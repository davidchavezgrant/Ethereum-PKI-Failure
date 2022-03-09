using Nethereum.Hex.HexConvertors.Extensions;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Signer;
namespace MetamaskDecryption.Data.Infura;

internal static class TransactionExtensions
{
	static List<AccessListItem> ToAccessListItems(this IEnumerable<AccessList> accessLists) => accessLists.Select(list => list.ToListItem()).ToList();
	static AccessListItem ToListItem(this AccessList list) => new(list.Address, list.StorageKeys.ToByteArrayList());
	static List<byte[]> ToByteArrayList(this IEnumerable<string> keys) => keys.Select(s => s.HexToByteArray()).ToList();
	public static ISignedTransaction ToLegacyTransaction(this Transaction t) => TransactionFactory.CreateLegacyTransaction(t.To,
	                                                                                                                       t.Gas,
	                                                                                                                       t.GasPrice,
	                                                                                                                       t.Value,
	                                                                                                                       t.Input,
	                                                                                                                       t.Nonce,
	                                                                                                                       t.R,
	                                                                                                                       t.S,
	                                                                                                                       t.V);

	public static ISignedTransaction To1559Transaction(this Transaction t) => TransactionFactory.Create1559Transaction(1,
	                                                                                                                   t.Nonce,
	                                                                                                                   t.MaxPriorityFeePerGas,
	                                                                                                                   t.MaxFeePerGas,
	                                                                                                                   t.Gas,
	                                                                                                                   t.To,
	                                                                                                                   t.Value,
	                                                                                                                   t.Input,
	                                                                                                                   t.AccessList.ToAccessListItems(),
	                                                                                                                   t.R,
	                                                                                                                   t.S,
	                                                                                                                   t.V);

}
