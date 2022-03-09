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

export async function encryptAsync(publicKey, message)
{

    const encryptedMessage =  encrypt({
        data: message,
        publicKey: publicKey,
        version: 'x25519-xsalsa20-poly1305',
    });
    return encryptedMessage;
}

export async function Decrypt(payload)
{
    let address = await Login();
    const decrypted = await ethereum.request({method: 'eth_decrypt', params: [payload, address]});
    return decrypted;
}