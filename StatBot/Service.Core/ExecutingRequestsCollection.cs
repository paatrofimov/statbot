using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using StatBot.RequestParametersParser;
using StatBot.Service.Core.Models;
using StatBot.Tracing;
using StatBot.UserIO;

namespace StatBot.Service.Core
{
    public class ExecutingRequestsCollection<TRequestParams> : IExecutingRequestsCollection<TRequestParams>
    {
        private readonly ConcurrentDictionary<string, Task>
            executingTasksByIds = new ConcurrentDictionary<string, Task>();

        private readonly string scope;
        private readonly IRequestExecutor<TRequestParams> requestExecutor;
        private readonly IRequestsParametersParser<TRequestParams> requestsParametersParser;
        private readonly ITraceIdProvider traceIdProvider;
        private readonly UserMessagePrintersCache printersCache;

        public ExecutingRequestsCollection(IRequestsParametersParser<TRequestParams> requestsParametersParser,
                                           IRequestExecutor<TRequestParams> requestExecutor,
                                           ITraceIdProvider traceIdProvider,
                                           UserMessagePrintersCache printersCache)
        {
            this.requestsParametersParser = requestsParametersParser;
            this.traceIdProvider = traceIdProvider;
            this.printersCache = printersCache;
            this.requestExecutor = requestExecutor;

            scope = typeof(TRequestParams).Name.Replace("RequestParameters", string.Empty);
        }

        public void TryStartExecuteNew(string rawInput)
        {
            var parameters = requestsParametersParser.ParseParametersOrNull(rawInput);

            if (parameters == null)
            {
                return;
            }

            var traceId = traceIdProvider.GetNext();

            var requestUserMessagePrinter = printersCache.GetOrAdd($"{scope}_{traceId}");

            requestUserMessagePrinter.RequestExecutionStarted();

            executingTasksByIds[traceId] =
                Task.Run(() =>
                         {
                             try
                             {
                                 Execute(parameters, requestUserMessagePrinter);
                             }
                             catch (Exception e)
                             {
                                 requestUserMessagePrinter.UnderlyingPrinter.Print(e.ToString());
                                 throw;
                             }
                             finally
                             {
                                 executingTasksByIds.TryRemove(traceId, out _);
                                 requestUserMessagePrinter.RequestExecutionEnded();
                             }
                         });
        }

        public bool IsNotEmpty()
        {
            return executingTasksByIds.Any();
        }

        public async Task WaitEmptyAsync()
        {
            while (!IsNotEmpty())
            {
                await Task.Delay(1000);
            }
        }

        private void Execute(TRequestParams parameters, IUserMessagePrinter requestUserMessagePrinter)
        {
            var processArgs = new RequestProcessorProcessArgs<TRequestParams>
                              {
                                  RequestParams = parameters,
                                  UserMessagePrinter = requestUserMessagePrinter
                              };
            requestExecutor.Execute(processArgs);
        }
    }
}