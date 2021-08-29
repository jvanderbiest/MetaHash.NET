using System.Text;

namespace MHC.Domain
{
    public class Wallet
    {
        public Wallet(string address, string privateKey, string publicKey)
        {
            Address = address;
            PrivateKey = privateKey;
            PublicKey = publicKey;
        }

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
        
        /// <summary>
        /// Signs the data with the private key
        /// </summary>
        /// <param name="data">The data field to sign</param>
        /// <returns></returns>
        public string Sign(string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            var signature = Crypto.Sign(bytes, PrivateKey);
            return signature;
        }
    }
}