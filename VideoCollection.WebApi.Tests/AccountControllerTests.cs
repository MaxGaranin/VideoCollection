using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using VideoCollection.WebApi.Controllers;

namespace VideoCollection.WebApi.Tests
{
    [TestFixture]
    public class AccountControllerTests
    {
        [Test]
        public void Test()
        {
            var ac = new AccountController();
            var amount = ac.Get();
            var initAmount = 1000m;
            Assert.AreEqual(initAmount, amount);

            var task1 = Task.Run(() =>
            {
                var deposites = 0m;
                var random = RandomProvider.GetThreadRandom();
                for (var i = 0; i < 1000; i++)
                {
                    decimal value = random.Next(100);
                    var operation = new AccoutOperation
                    {
                        Date = DateTime.Now,
                        OperationType = OperationType.Deposit,
                        Value = value
                    };
                    ac.Change(operation);
                    deposites += value;
                    Thread.Sleep(1);
                }

                return deposites;
            });

            var task2 = Task.Run(() =>
            {
                var withdrowals = 0m;
                var random = RandomProvider.GetThreadRandom();
                for (var i = 0; i < 1000; i++)
                {
                    decimal value = random.Next(100);
                    var operation = new AccoutOperation
                    {
                        Date = DateTime.Now,
                        OperationType = OperationType.Withdraw,
                        Value = value
                    };
                    ac.Change(operation);
                    withdrowals += value;
                    Thread.Sleep(2);
                }

                return withdrowals;
            });

            Task.WaitAll(task1, task2);

            var d = task1.Result;
            var w = task2.Result;
            Assert.AreEqual(initAmount + d - w, ac.Get());
        }
    }

    public static class RandomProvider
    {
        private static int _seed = Environment.TickCount;

        private static readonly ThreadLocal<Random> RandomWrapper = new ThreadLocal<Random>(() =>
            new Random(Interlocked.Increment(ref _seed))
        );

        public static Random GetThreadRandom()
        {
            return RandomWrapper.Value;
        }
    }
}