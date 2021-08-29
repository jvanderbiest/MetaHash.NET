using MHC.Domain;

namespace MHC
{
    public static class Utilities
    {
        public static Wallet CreateWalletAddress()
        {
            var privateKey = Crypto.CreatePrivateKey();
            var publicKey = Crypto.GetPublicKey(privateKey);
            var address = Crypto.GetAddress(publicKey);

            var wallet = new Wallet(address, privateKey, publicKey);
            return wallet;
        }
    }
}