namespace MetamaskDecryption.Data.Etherscan;

internal class EtherscanClient
{
	private readonly HttpClient _httpClient;
	public EtherscanClient(HttpClient httpClient) => this._httpClient = httpClient;

	public async Task<string> GetTransactionHashByWalletAddressAsync(string address)
	{
		address = address.Replace("0x", "");

		var url = $"https://api.etherscan.io/api?module=account&action=txlist&address=0x{address}&startblock=0&endblock=99999999&page=1&offset=10&sort=asc&apikey=9MU7X99KB63TYVQI1XK9HTU1KREYQ4CMAW";

		var                             transactions           = await this._httpClient.GetFromJsonAsync<EtherscanResponseDTO>(url);
		EtherscanResponseDTO.ResultItem transactionFromAddress = transactions!.result.First(t => t.from == ("0x" + address));

		return transactionFromAddress.hash;
	}
}