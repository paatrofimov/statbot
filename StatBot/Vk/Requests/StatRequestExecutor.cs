using System;
using System.Linq;
using StatBot.Service.Core;
using StatBot.Service.Core.Models;
using StatBot.Settings;
using StatBot.Vk.Cache;
using StatBot.Vk.Helpers;
using StatBot.Vk.Models;
using StatBot.VkApiClient;
using StatBot.VkApiClient.Models.Internal;

namespace StatBot.Vk.Requests
{
    public class StatRequestExecutor : IRequestExecutor<StatRequestParameters>
    {
        private readonly ScreenNamesToVkApiObjectsCache screenNamesToVkApiObjectsCache;
        private readonly ApplicationSettings settings;
        private readonly IStatsBuilder<StatsBuilderBuildArgs>[] statsBuilders;
        private readonly IUsernameProvider usernameProvider;
        private readonly IVkClient vkClient;

        public StatRequestExecutor(ApplicationSettings settings,
                                   IVkClient vkClient,
                                   ScreenNamesToVkApiObjectsCache screenNamesToVkApiObjectsCache,
                                   IStatsBuilder<StatsBuilderBuildArgs>[] statsBuilders,
                                   IUsernameProvider usernameProvider)
        {
            this.settings = settings;
            this.vkClient = vkClient;
            this.screenNamesToVkApiObjectsCache = screenNamesToVkApiObjectsCache;
            this.statsBuilders = statsBuilders;
            this.usernameProvider = usernameProvider;
        }

        public void Execute(RequestProcessorProcessArgs<StatRequestParameters> args)
        {
            var screenName = args.RequestParams.ScreenName;
            var printer = args.UserMessagePrinter;

            var vkApiObject = screenNamesToVkApiObjectsCache.GetOrAdd(screenName);
            if (vkApiObject?.Id == null)
            {
                printer.NoUserOrGroup(screenName);
                return;
            }

            var username = usernameProvider.GetUsernameForObject(vkApiObject);

            var wallGetObject = vkClient.WallGet(new InternalVkApiWallGetRequestParams
                                                 {
                                                     OwnerId = vkApiObject.Id.Value,
                                                     Count = settings.VkWallGetPostsCountParameter
                                                 });

            var stats = statsBuilders.Select(builder => builder.Build(new StatsBuilderBuildArgs
                                                                      {
                                                                          WallGetObject = wallGetObject,
                                                                          Username = username
                                                                      })
            );
            var statsMessage = string.Join(Environment.NewLine, stats);
            printer.UnderlyingPrinter.Print(statsMessage);

            printer.PostingStatsMessageToGroupWithId(settings.VkGroupOwnerIdForWallPost.ToString());
            vkClient.WallPost(new InternalVkApiWallPostRequestParams
                              {
                                  OwnerId = settings.VkGroupOwnerIdForWallPost,
                                  Message = statsMessage
                              });
        }
    }
}