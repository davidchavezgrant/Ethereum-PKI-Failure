namespace MetamaskDecryption.Data.Etherscan;

internal class EtherscanResponseDTO
{
	public string           message { get; set; } = null!;
	public List<ResultItem> result  { get; set; } = null!;
	public string           status  { get; set; } = null!;
	internal class ResultItem
	{
		public string blockHash         { get; set; } = null!;
		public string blockNumber       { get; set; } = null!;
		public string confirmations     { get; set; } = null!;
		public string contractAddress   { get; set; } = null!;
		public string cumulativeGasUsed { get; set; } = null!;
		public string from              { get; set; } = null!;
		public string gas               { get; set; } = null!;
		public string gasPrice          { get; set; } = null!;
		public string gasUsed           { get; set; } = null!;
		public string hash              { get; set; } = null!;
		public string input             { get; set; } = null!;
		public string isError           { get; set; } = null!;
		public string nonce             { get; set; } = null!;
		public string timeStamp         { get; set; } = null!;
		public string to                { get; set; } = null!;
		public string transactionIndex  { get; set; } = null!;
		public string txreceipt_status  { get; set; } = null!;
		public string value             { get; set; } = null!;
	}
}