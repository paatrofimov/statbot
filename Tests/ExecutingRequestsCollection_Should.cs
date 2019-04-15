using System;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using NUnit.Framework;
using StatBot.RequestParametersParser;
using StatBot.Service.Core;
using StatBot.Service.Core.Models;
using StatBot.Tracing;
using StatBot.UserIO;

namespace Tests
{
    [TestFixture]
    public class ExecutingRequestsCollection_Should
    {
        [SetUp]
        public void SetUp()
        {
            requestsExecutor = A.Fake<IRequestExecutor<object>>();
            var parser = A.Fake<IRequestsParametersParser<object>>();
            var traceIdProvider = A.Fake<ITraceIdProvider>();
            var printersFactory = A.Fake<UserMessagePrintersCache>();
            requestsCollection =
                new ExecutingRequestsCollection<object>(parser, requestsExecutor, traceIdProvider, printersFactory);
        }

        private IExecutingRequestsCollection requestsCollection;
        private IRequestExecutor<object> requestsExecutor;

        [Test]
        public void NotThrow_When_SingleRequestFails()
        {
            A.CallTo(() => requestsExecutor.Execute(A<RequestProcessorProcessArgs<object>>.Ignored))
             .Throws(_ => new Exception());

            new Action(() => requestsCollection.TryStartExecuteNew("stub"))
                .Should()
                .NotThrow();
        }

        [Test]
        public void RemoveRequest_When_RequestFails()
        {
            A.CallTo(() => requestsExecutor.Execute(A<RequestProcessorProcessArgs<object>>.Ignored))
             .Throws(_ => new Exception());

            requestsCollection.TryStartExecuteNew("stub");

            Task.Delay(100).Wait();

            requestsCollection.IsNotEmpty().Should().BeFalse();
        }
    }
}