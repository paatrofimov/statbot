using System;
using StatBot.Configuration;
using StatBot.Service.Core;
using StatBot.UserIO;
using StatBot.Vk.Authentication;

namespace StatBot
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var container = ContainerConfigurator.ConfigureContainer();

            try
            {
                container.Resolve<IVkAuthenticator>().Authenticate();

                new Service.Core.Service(container.Resolve<IExecutingRequestsCollection[]>(),
                                         container.Resolve<IUserRawInputProvider>(),
                                         container.Resolve<UserMessagePrintersCache>(),
                                         container.Resolve<IUserConsentProvider>())
                    .StartAndWaitAsync()
                    .GetAwaiter().GetResult();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Console.ReadLine();
            }
            finally
            {
                container.Dispose();
            }
        }
    }
}