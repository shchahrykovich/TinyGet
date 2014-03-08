using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TinyGet.Config;
using TinyGet.Requests;

namespace TinyGet.Tests.Requests
{
    [TestFixture]
    public class RequestSenderTests
    {
        private RequestSender _sender;
        private Mock<IAppArguments> _arguments;
        private CancellationTokenSource _tokenSource;

        [SetUp]
        public void Setup()
        {
            _arguments = new Mock<IAppArguments>();
            _tokenSource = new CancellationTokenSource();
            Context context = new Context(_arguments.Object, _tokenSource.Token, null);
            _sender = new RequestSender(context);
        }

        [Test]
        public void Should_Return_Cancelled_Task_When_Cancel_Called()
        {
            // Act
            _tokenSource.Cancel();
            Task result = _sender.Run();
            
            // Assert
            Assert.Throws<AggregateException>(result.Wait);
            Assert.That(result.IsCanceled, Is.True);
        }
    }
}
