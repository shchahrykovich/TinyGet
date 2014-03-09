﻿using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using TinyGet.Config;
using TinyGet.Requests;
using TinyGet.Tests.Controllers;
using TinyGet.Tests.Helpers;

namespace TinyGet.Tests.Requests
{
    [TestFixture]
    public class RequestSenderTests
    {
        private RequestSender _sender;
        private Mock<IAppArguments> _arguments;
        private CancellationTokenSource _tokenSource;
        private ApiServer _server;

        [SetUp]
        public void Setup()
        {
            _arguments = new Mock<IAppArguments>();
            _arguments.Setup(a => a.Method).Returns(HttpMethod.Get);

            _tokenSource = new CancellationTokenSource();
            Context context = new Context(_arguments.Object, _tokenSource.Token, null);
           
            _sender = new RequestSender(context);
            _server = new ApiServer();
        }

        [TearDown]
        public void TearDown()
        {
            if (null != _server)
            {
                _server.Dispose();
            }
        }

        [Test]
        public void Should_Send_Http_Request()
        {           
            // Arrange            
            _arguments.Setup(a => a.GetUrl()).Returns(_server.HostUrl + "api/Home");

            // Act
            Task result = _sender.Run();
            result.Wait();

            // Assert
            AssertSingleRequestMethod("HomeController.Get", 1);
        }

        private static void AssertSingleRequestMethod(string name, int count)
        {
            Assert.That(RequestRecorder.GetTotal(), Is.EqualTo(count));
            Assert.That(RequestRecorder.Get(name), Is.EqualTo(count));
        }

        [Test]
        [Timeout(2000)]
        public void Should_Cancel_Requests()
        {
            // Arrange
            _arguments.Setup(a => a.GetUrl()).Returns(_server.HostUrl + "api/Home/long-running");

            // Act
            _tokenSource.Cancel();
            Task result = _sender.Run();
            
            // Assert
            Assert.Throws<AggregateException>(result.Wait);
            Assert.That(result.IsCanceled, Is.True);
        }
    }
}
