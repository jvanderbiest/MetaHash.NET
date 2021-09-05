[![NuGet Badge](https://buildstats.info/nuget/metahash.net)](https://www.nuget.org/packages/metahash.net/)

# MetaHash.NET
MetaHash crypto library wrapper for .NET implementations.

## MetaHash wallet
### New wallet
```sh
using MHC.Domain;
var wallet = new Wallet();
```

- Generate wallet on the fly
- Load existing wallet from private key and calculate address and public key
- Load existing wallet from address, private and public key

## MetaHash client
### Initialize default
```sh
using MHC;
var client = new Client();
```

- Transfer MHC
- Get transaction
- Fetch history
- Fetch balance
- Verify transaction

## Internals
To access the base cryptography, use MHC.Internals

# Credits
Base crypto implementation from https://github.com/maincoon/MHCCrypto

# Questions
Join us on Telegram: https://t.me/metahash_eng