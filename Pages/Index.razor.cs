using MetamaskDecryption.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
namespace MetamaskDecryption.Pages;

public partial class Index
{
	private string decryptedText = null!;
	private string encryptedText = null!;
	private string error = null!;
	private string input = null!;
	private bool loggedIn;
	private string metamaskId = null!;
	private bool metamaskIsInstalled;
	[Inject]
	private EthereumService Ethereum { get; set; } = null!;
	[Inject]
	private IJSRuntime Javascript { get; set; } = null!;

	private async Task Decrypt()
	{
		this.decryptedText = await this.Javascript.InvokeAsync<string>("JsLib.decryptAsync", this.encryptedText);
	}

	private async Task Encrypt()
	{
		string publicKey = await this.Ethereum.GetPublicKeyAsync(this.metamaskId);
		var    result    = await this.Javascript.InvokeAsync<EncryptionResult>("JsLib.encryptMessage", publicKey, this.input);
		this.encryptedText = result.ciphertext;
	}

	private async Task MetaMaskSignInAsync()
	{
		try
		{
			this.metamaskId = await this.Javascript.InvokeAsync<string>("JsLib.loginAsync");
			this.loggedIn = true;
		}
		catch (Exception)
		{
			this.error = "Could not log in with MetaMask. Is your MetaMask unlocked?";
		}
	}
	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		this.metamaskIsInstalled = await this.Javascript.InvokeAsync<bool>("JsLib.metamaskIsInstalled");
		this.metamaskIsInstalled = await this.Javascript.InvokeAsync<bool>("JsLib.metamaskIsInstalled");
		StateHasChanged();
	}

	// DTO returned by JS method
	public record EncryptionResult(string ciphertext,
	                               string ephemPublicKey,
	                               string nonce,
	                               string version);
}