using FakeItEasy;
using NUnit.Framework;
using StatBot.Vk.Cache;
using StatBot.Vk.Helpers;
using StatBot.VkApiClient;
using StatBot.VkApiClient.Models.Internal;

namespace Tests
{
    [TestFixture]
    public class UsernameProvider_Should
    {
        private IUsernameProvider usernameProvider;
        private IVkClient vkClient;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            vkClient = A.Fake<IVkClient>();
            usernameProvider = new UsernameProvider(new IdsToUsersCache(vkClient), new IdsToGroupsCache(vkClient));
        }

        [Test]
        public void Cache_User()
        {
            var apiObject = new InternalVkApiObject() {Id = 123, Type = InternalVkObjectType.User};

            A.CallTo(() => vkClient.GetUsers(A<InternalVkApiUserGetRequestParams>.Ignored))
             .Returns(new[] {A.Dummy<InternalVkApiUser>()});

            usernameProvider.GetUsernameForObject(apiObject);
            usernameProvider.GetUsernameForObject(apiObject);

            A.CallTo(() => vkClient.GetUsers(A<InternalVkApiUserGetRequestParams>.Ignored))
             .MustHaveHappenedOnceExactly();
        }

        [Test]
        public void Cache_Group()
        {
            var apiObject = new InternalVkApiObject() {Id = 123, Type = InternalVkObjectType.Group};

            A.CallTo(() => vkClient.GetGroupById(A<InternalVkApiGetGroupByIdRequestParams>.Ignored))
             .Returns(A.Dummy<InternalVkApiGroup>());

            usernameProvider.GetUsernameForObject(apiObject);
            usernameProvider.GetUsernameForObject(apiObject);

            A.CallTo(() => vkClient.GetGroupById(A<InternalVkApiGetGroupByIdRequestParams>.Ignored))
             .MustHaveHappenedOnceExactly();
        }
    }
}