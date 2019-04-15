using System;
using System.Linq;
using System.Threading.Tasks;
using StatBot.UserIO;

namespace StatBot.Service.Core
{
    public class Service : IService
    {
        private readonly IExecutingRequestsCollection[] executingRequestsCollections;
        private readonly IUserMessagePrinter serviceUserMessagePrinter;
        private readonly IUserConsentProvider userConsentProvider;
        private readonly IUserRawInputProvider userRawInputProvider;

        public Service(IExecutingRequestsCollection[] executingRequestsCollections,
                       IUserRawInputProvider userRawInputProvider,
                       UserMessagePrintersCache printersCache,
                       IUserConsentProvider userConsentProvider)
        {
            this.executingRequestsCollections = executingRequestsCollections;
            this.userRawInputProvider = userRawInputProvider;
            this.userConsentProvider = userConsentProvider;

            serviceUserMessagePrinter = printersCache.GetOrAdd("Service");
        }

        public async Task StartAndWaitAsync()
        {
            await Task.Run(StartAndWaitAsyncInternal);
        }

        private async Task StartAndWaitAsyncInternal()
        {
            try
            {
                var rawInput = WaitNextRawInput();

                while (!NeedTerminate(rawInput))
                {
                    foreach (var requestsCollection in executingRequestsCollections)
                    {
                        requestsCollection.TryStartExecuteNew(rawInput);
                    }

                    rawInput = WaitNextRawInput();
                }

                var notEmptyRequestsCollections = executingRequestsCollections.Where(c => c.IsNotEmpty()).ToArray();
                if (notEmptyRequestsCollections.Any() && UserWantsWaitPendingRequests())
                {
                    await Task.WhenAll(notEmptyRequestsCollections.Select(r => r.WaitEmptyAsync()));
                }
            }
            catch (Exception e)
            {
                serviceUserMessagePrinter.UnderlyingPrinter.Print(e.ToString());
                throw;
            }
        }

        private string WaitNextRawInput()
        {
            serviceUserMessagePrinter.PrintNextRequestParameters();
            return userRawInputProvider.WaitUserRawInput();
        }

        private bool NeedTerminate(string rawInput)
        {
            return string.IsNullOrWhiteSpace(rawInput);
        }

        private bool UserWantsWaitPendingRequests()
        {
            serviceUserMessagePrinter.DoYouWantToWaitPendingRequests();
            serviceUserMessagePrinter.YesOrNo();

            var rawInput = userRawInputProvider.WaitUserRawInput();

            while (!userConsentProvider.ConsentFormatIsValid(rawInput))
            {
                serviceUserMessagePrinter.InputNotExpected(UserMessagesConstants.UserConsentOptions);
                rawInput = userRawInputProvider.WaitUserRawInput();
            }

            return userConsentProvider.UserConsents(rawInput);
        }
    }
}