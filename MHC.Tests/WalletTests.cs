using System.Threading.Tasks;
using MHC.Domain;
using MHC.Domain.Requests;
using NUnit.Framework;

namespace MHC.Tests
{
    [TestFixture]
    public class WalletTests
    {
        [Test]
        public void CreateWalletAddress_ShouldReturnAddress()
        {
            var wallet = new Wallet();
            Assert.NotNull(wallet);
            Assert.IsNotEmpty(wallet.Address);
            Assert.IsNotEmpty(wallet.PrivateKey);
            Assert.IsNotEmpty(wallet.PublicKey);
            Assert.IsTrue(wallet.PrivateKeyRaw.Length == 32);
            Assert.IsTrue(wallet.PublicKeyRaw.Length == 65);
        }

        [Test]
        public async Task SendTx()
        {
            var client = new Client();
            var privateKey = "";
            var wallet = new Wallet(privateKey);
            string data = "some data";
            var sign = wallet.Sign(data);

            var request = new SendTransactionRequest
            {
                PublicKey = "",
                To = "",
                Fee = "0",
                Nonce = "1",
                Data = data,
                Sign = sign,
                Value = "0.000001"
            };

            var response = await client.SendTransaction(request);
        }
    }
}
