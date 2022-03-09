using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.RPC.TransactionTypes;
using Nethereum.Signer;
using Nethereum.Web3;
using TransactionType=Nethereum.RPC.TransactionTypes.TransactionType;
namespace MetamaskDecryption;


internal class ChainClient
{
	private readonly HttpClient _httpClient;
	public ChainClient(HttpClient httpClient)
	{
		this._httpClient = httpClient;
	}
	public static async Task<ISignedTransaction> GetTransactionByHashAsync(string transactionHash)
	{
		var web3 = new Web3("https://mainnet.infura.io/v3/0dca4e01235c46049adf4d867a7def23");

		//Getting the transaction from the chain
		Transaction? transactionRpc = await web3.Eth.Transactions.GetTransactionByHash.SendRequestAsync(transactionHash);
		// TODO transactionRpc.GetPubKey()

		ISignedTransaction? transaction = transactionRpc.Type.ToTransactionType() switch
		                                  {
			                                  TransactionType.Legacy => TransactionFactory.CreateLegacyTransaction(transactionRpc.To,
			                                                                                                       transactionRpc.Gas,
			                                                                                                       transactionRpc.GasPrice,
			                                                                                                       transactionRpc.Value,
			                                                                                                       transactionRpc.Input,
			                                                                                                       transactionRpc.Nonce,
			                                                                                                       transactionRpc.R,
			                                                                                                       transactionRpc.S,
			                                                                                                       transactionRpc.V),

			                                  TransactionType.LegacyTransaction => TransactionFactory.CreateLegacyTransaction(transactionRpc.To,
			                                                                                                                  transactionRpc.Gas,
			                                                                                                                  transactionRpc.GasPrice,
			                                                                                                                  transactionRpc.Value,
			                                                                                                                  transactionRpc.Input,
			                                                                                                                  transactionRpc.Nonce,
			                                                                                                                  transactionRpc.R,
			                                                                                                                  transactionRpc.S,
			                                                                                                                  transactionRpc.V),

			                                  TransactionType.LegacyChainTransaction => TransactionFactory.CreateLegacyTransaction(transactionRpc.To,
			                                                                                                                       transactionRpc.Gas,
			                                                                                                                       transactionRpc.GasPrice,
			                                                                                                                       transactionRpc.Value,
			                                                                                                                       transactionRpc.Input,
			                                                                                                                       transactionRpc.Nonce,
			                                                                                                                       transactionRpc.R,
			                                                                                                                       transactionRpc.S,
			                                                                                                                       transactionRpc.V),

			                                  TransactionType.EIP1559 => TransactionFactory.Create1559Transaction(1,
			                                                                                                      transactionRpc.Nonce,
			                                                                                                      transactionRpc.MaxPriorityFeePerGas,
			                                                                                                      transactionRpc.MaxFeePerGas,
			                                                                                                      transactionRpc.Gas,
			                                                                                                      transactionRpc.To,
			                                                                                                      transactionRpc.Value,
			                                                                                                      transactionRpc.Input,
			                                                                                                      transactionRpc.AccessList.Select(list => new AccessListItem(list.Address, list.StorageKeys.Select(s => s.HexToByteArray()).ToList())).ToList(),
			                                                                                                      transactionRpc.R,
			                                                                                                      transactionRpc.S,
			                                                                                                      transactionRpc.V),
			                                  _ => throw new ArgumentOutOfRangeException()
		                                  };

		return transaction;
	}
	public async Task<string> GetTransactionHashByWalletAddressAsync(string address)
	{
		address = address.Replace("0x", "");

		var url = $"https://api.etherscan.io/api?module=account&action=txlist&address=0x{address}&startblock=0&endblock=99999999&page=1&offset=10&sort=asc&apikey=9MU7X99KB63TYVQI1XK9HTU1KREYQ4CMAW";

		var etherscanResponse = await this._httpClient.GetFromJsonAsync<EtherscanResponse>(url);

		return etherscanResponse.result.First(t => t.from == $"0x"+address)
		                        .hash;

	}

	class EtherscanResponse
	{
		public string                             message { get; set; }
		public List<EtherscanTransactionResponse> result  { get; set; }
		public string                             status  { get; set; }
	}

	class EtherscanTransactionResponse
	{
		public string blockHash         { get; set; }
		public string blockNumber       { get; set; }
		public string confirmations     { get; set; }
		public string contractAddress   { get; set; }
		public string cumulativeGasUsed { get; set; }
		public string from              { get; set; }
		public string gas               { get; set; }
		public string gasPrice          { get; set; }
		public string gasUsed           { get; set; }
		public string hash              { get; set; }
		public string input             { get; set; }
		public string isError           { get; set; }
		public string nonce             { get; set; }
		public string timeStamp         { get; set; }
		public string to                { get; set; }
		public string transactionIndex  { get; set; }
		public string txreceipt_status  { get; set; }
		public string value             { get; set; }
	}
}