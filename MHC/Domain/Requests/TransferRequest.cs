using System.Text;
using Org.BouncyCastle.Utilities.Encoders;

namespace MHC.Domain.Requests
{
    public class TransferRequest
    {
        /// <summary>
        /// The target wallet address to transfer the amount of MHC
        /// </summary>
        public string ToAddress { get; set; }

        /// <summary>
        /// The private key from the source wallet
        /// </summary>
        public string PrivateKey { get; set; }

        /// <summary>
        /// The amount of MHC to transfer, will be converted to HASH
        /// </summary>
        public long MhcAmount { get; set; }

        /// <summary>
        /// An optional transaction message that needs to adhere to 0[xX][0-9a-fA-F]+?
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// An optional fee to include in the transaction
        /// </summary>
        public int Fee { get; set; }

        public long MhcHashAmount => MhcAmount * 1000000;

        public string DataHex
        {
            get
            {
                byte[] bytes = Encoding.UTF8.GetBytes(Data);
                var dataHex = Hex.ToHexString(bytes);
                return dataHex;
            }
        }
    }
}