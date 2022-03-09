namespace MetamaskDecryption.Data.Etherscan;

internal class EtherscanResponseDTO
{
	public string           message { get; set; }
	public List<ResultItem> result  { get; set; }
	public string           status  { get; set; }
	internal class ResultItem
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