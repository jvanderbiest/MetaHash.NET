using MHC.Domain;
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
    }
}