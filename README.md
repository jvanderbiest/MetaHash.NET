[![NuGet Badge](https://buildstats.info/nuget/metahash.net)](https://www.nuget.org/packages/metahash.net/)

# MetaHash.NET
MetaHash crypto library wrapper for .NET implementations.

## MetaHash wallet
### New wallet
```sh
using MHC.Domain;
var wallet = new Wallet();
```

Load wallet from private key and calculate address and public key
Load wallet from address, private and public key

## MetaHash client
### Initialize default
```sh
using MHC;
var client = new Client();
```

- Send transactions
- Get transaction
- Fetch history

## Internals
To access the base cryptography, use MHC.Internals

# Credits
Base crypto implementation from https://github.com/maincoon/MHCCrypto

# Questions
Join us on Telegram: https://t.me/metahash_eng