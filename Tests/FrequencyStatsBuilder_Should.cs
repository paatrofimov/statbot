using ApprovalTests;
using ApprovalTests.Reporters;
using NUnit.Framework;
using StatBot.Vk.Models;
using StatBot.Vk.Requests;
using StatBot.VkApiClient.Models.Internal;

namespace Tests
{
    [TestFixture]
    public class FrequencyStatsBuilder_Should
    {
        private FrequencyStatsBuilder builder;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            builder = new FrequencyStatsBuilder();
        }

        [Test]
        [UseReporter(typeof(DiffReporter))]
        public void BuildFrequency()
        {
            var result = builder.Build(new StatsBuilderBuildArgs
                                       {
                                           Username = "Username",
                                           WallGetObject = new InternalVkApiWallGetObject
                                                           {
                                                               WallApiPosts = new[]
                                                                              {
                                                                                  new InternalVkApiPost
                                                                                  {
                                                                                      Text =
                                                                                          "ПоcтрОение таблицы чАстоТ символов"
                                                                                  }
                                                                              }
                                                           }
                                       });

            Approvals.Verify(result);
        }
    }
}