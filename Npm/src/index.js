import {ethers, Signer} from "ethers";
import { encrypt } from "@metamask/eth-sig-util";
import {bufferToHex} from "ethereumjs-util";


export function metamaskIsInstalled()
{
    return (typeof window.ethereum !== 'undefined');
}

export async function loginAsync()
{
    const accounts = await ethereum.request({ method: 'eth_requestAccounts' });
    return accounts[0];
}

export function encryptMessage(publicKey, message)
{
    const encryptedMessage =  encrypt({
        data: message,
        publicKey: publicKey,
        version: 'x25519-xsalsa20-poly1305',
    });
    return encryptedMessage;
}

export async function decryptAsync(payload)
{
    const accounts = await ethereum.request({ method: 'eth_requestAccounts' });
    const address = accounts[0];
    const decrypted = await ethereum.request({method: 'eth_decrypt', params: [payload, address]});
    return decrypted;
}