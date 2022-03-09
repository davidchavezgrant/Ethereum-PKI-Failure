namespace MetamaskDecryption;

class EthereumService
{
	private readonly HttpClient _http;
	public EthereumService(HttpClient http) => this._http = http;

	public async Task<string> RecoverPublicKey(string address)
	{
		var publicKey = "CePgQSgsYPzcaFN179CqDX9G7hdJXS40RvVbB58f+eNRasGKwDVq54vOIcHs50C0LPGUmXkbyzF4lSf8uJKO1Q==";
		return publicKey;
	}
}