using System.Text;
using MHC.Internals;

namespace MHC.Domain
{
    public class Wallet
    {
        /// <summary>
        /// Creates a new wallet address with public and private key
        /// </summary>
        public Wallet()
        {
            PrivateKey = Crypto.CreatePrivateKey();
            PublicKey = Crypto.GetPublicKey(PrivateKey);
            Address = Crypto.GetAddress(Address);
        }

        /// <summary>
        /// Uses an existing address with provided private key and public key
        /// </summary>
        /// <param name="address"></param>
        /// <param name="privateKey"></param>
        /// <param name="publicKey"></param>
        public Wallet(string address, string privateKey, string publicKey)
        {
            Address = address;
            PrivateKey = privateKey;
            PublicKey = publicKey;
        }

        /// <summary>
        /// Uses an existing private key to extract the address and public key
        /// </summary>
        /// <param name="privateKey"></param>
        public Wallet(string privateKey)
        {
            PrivateKey = privateKey;
            PublicKey = Crypto.GetPublicKey(privateKey);
            Address = Crypto.GetAddress(PublicKey);
        }

        public string Address { get; }

        private byte[] _privateKeyRaw;
        public byte[] PrivateKeyRaw
        {
            get
            {
                if (_privateKeyRaw != null)
                {
                    return _privateKeyRaw;

                }
                return _privateKeyRaw = Crypto.ParsePrivateKey(PrivateKey);
            }
        }

        public string PrivateKey { get; }
        public string PublicKey { get; }

        private byte[] _publicKeyRaw;
        public byte[] PublicKeyRaw
        {
            get
            {
                if (_publicKeyRaw != null)
                {
                    return _publicKeyRaw;

                }
                return _publicKeyRaw = Crypto.ParsePublicKey(PublicKey);
            }
        }
    }
}