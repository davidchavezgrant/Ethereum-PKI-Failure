using Nethereum.RPC.Eth.DTOs;
using Nethereum.RPC.TransactionTypes;
using Nethereum.Signer;
using Nethereum.Web3;
using TransactionType=Nethereum.RPC.TransactionTypes.TransactionType;
namespace MetamaskDecryption.Data.Infura;

internal class InfuraClient
{
	private const string infuraUrl = "https://mainnet.infura.io/v3/0dca4e01235c46049adf4d867a7def23";
	public static async Task<ISignedTransaction> GetTransactionByHashAsync(string transactionHash)
	{
		var web3 = new Web3(infuraUrl);

		//Getting the transaction from the chain
		Transaction? rpcTransaction = await web3.Eth.Transactions.GetTransactionByHash.SendRequestAsync(transactionHash);

		ISignedTransaction? transaction = rpcTransaction.Type.ToTransactionType() switch
		                                  {
			                                  TransactionType.Legacy                 => rpcTransaction.ToLegacyTransaction(),
			                                  TransactionType.LegacyTransaction      => rpcTransaction.ToLegacyTransaction(),
			                                  TransactionType.LegacyChainTransaction => rpcTransaction.ToLegacyTransaction(),
			                                  TransactionType.EIP1559                => rpcTransaction.To1559Transaction(),
			                                  _                                      => throw new ArgumentOutOfRangeException()
		                                  };

		return transaction;
	}
}
