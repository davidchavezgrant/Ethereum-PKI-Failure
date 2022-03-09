using MetamaskDecryption.Data.Etherscan;
using MetamaskDecryption.Data.Infura;
using Nethereum.Signer;

namespace MetamaskDecryption.Data;

class EthereumService
{
	private readonly EtherscanClient etherscanClient;
	public EthereumService(EtherscanClient etherscanClient) => this.etherscanClient = etherscanClient;

	public async Task<string> GetPublicKeyAsync(string address)
	{
		// This works fine
		ISignedTransaction transaction = await GetSignedTransactionAsync(address);

		// Metamask doesn't like this return value
		return RecoverPublicKey(transaction);
	}

	private async Task <ISignedTransaction> GetSignedTransactionAsync(string address)
	{
		// Infura (ethRPC) doesn’t have a mapping of address: transactions by address so we pull that from the etherscan explorer
		string hash = await this.etherscanClient.GetTransactionHashByWalletAddressAsync(address);

		// v,r,and s aren’t available in Etherscan return which we need to recover the public key so we pull that from infura
		ISignedTransaction transaction = await InfuraClient.GetTransactionByHashAsync(hash);
		return transaction;
	}

	private static string RecoverPublicKey(ISignedTransaction transaction)
	{
		EthECKey recoveredKey = transaction is LegacyTransactionChainId legacy?
			               EthECKey.RecoverFromSignature(transaction.Signature, transaction.RawHash, legacy.GetChainIdAsBigInteger()) :
			               EthECKey.RecoverFromParityYSignature(transaction.Signature, transaction.RawHash);

		var publicKey = recoveredKey.GetPubKeyNoPrefix(true);
		return Convert.ToBase64String(publicKey);
	}
}