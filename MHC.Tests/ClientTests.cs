using System.Threading.Tasks;
using MHC.Domain;
using MHC.Domain.Requests;
using NUnit.Framework;

namespace MHC.Tests
{
    [TestFixture]
    public class ClientTests : BaseTests
    {
       
        [Test, Category("Integration")]
        public async Task TransferAndVerify()
        {
            var client = new Client();
            var privateKey = Configuration["privateKey"];
            var to = Configuration["address"];

            var wallet = new Wallet(privateKey);

            var transferResponse = await client.Transfer(new TransferRequest { ToAddress = to, PrivateKey = privateKey, Data = "message", Fee = 0, MhcAmount = 5 });

            Assert.AreEqual("ok", transferResponse.Result);
            Assert.IsNotEmpty(transferResponse.Result);
            Assert.IsFalse(transferResponse.Result.Contains("error"));


            var verifyResponse = await client.VerifyTransactionOnWallet(wallet.Address, transferResponse.Params);

            Assert.IsTrue(verifyResponse);
        }

        [Test, Category("Integration")]
        public async Task FetchBalance()
        {
            var client = new Client();
            var privateKey = Configuration["privateKey"];

            var wallet = new Wallet(privateKey);

            var fetchBalanceResponse = await client.FetchBalance(new FetchBalanceRequest
                {Parameters = new FetchBalanceRequestParams {Address = wallet.Address}});

            Assert.NotNull(fetchBalanceResponse);
            Assert.Greater(fetchBalanceResponse.Result.Spent, 0);
        }
    }
}