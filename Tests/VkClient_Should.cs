using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using StatBot.VkApiClient;
using StatBot.VkApiClient.Models.Internal;

namespace Tests
{
    [TestFixture]
    public class VkClient_Should
    {
        private VkClient client;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            client = new VkClient();
            client.Authorize(new InternalVkApiAuthorizationRequestParams
                             {
                                 ApplicationId = 6933681,
                                 AccessToken =
                                     "b30e6b4575c30264bd26c5a6449d4e9ca635357d7758dc240f4ca7390d520aa35946b9ecad3e01138623f"
                             });
        }

        [Test]
        public void GetGroup()
        {
            var response = client.GetGroupById(new InternalVkApiGetGroupByIdRequestParams {GroupId = "180815810"});

            response.Should().BeEquivalentTo(new InternalVkApiGroup
                                             {
                                                 Id = 180815810,
                                                 Name = "test_api"
                                             });
        }

        [Test]
        public void GetUser()
        {
            var response = client.GetUserOrDie(147232170);

            response.Should().BeEquivalentTo(new InternalVkApiUser
                                             {
                                                 Id = 147232170,
                                                 FirstName = "Павел",
                                                 LastName = "Трофимов"
                                             });
        }

        [Test]
        public void ResolveScreenName_Of_Group()
        {
            var response = client.ResolveScreenName(new InternalVkApiResolveScreenNameRequestParams
                                                    {
                                                        ScreenName = "club180815810"
                                                    });

            response.Should().BeEquivalentTo(new InternalVkApiObject
                                             {
                                                 Id = 180815810,
                                                 Type = InternalVkObjectType.Group
                                             });
        }

        [Test]
        public void ResolveScreenName_Of_User()
        {
            var response = client.ResolveScreenName(new InternalVkApiResolveScreenNameRequestParams
                                                    {
                                                        ScreenName = "patrofimov"
                                                    });

            response.Should().BeEquivalentTo(new InternalVkApiObject
                                             {
                                                 Id = 147232170,
                                                 Type = InternalVkObjectType.User
                                             });
        }

        [Test]
        public void WallGet()
        {
            var response = client.WallGet(new InternalVkApiWallGetRequestParams
                                          {
                                              OwnerId = -180815810
                                          });

            response.WallApiPosts.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void WallPost()
        {
            var response = client.WallPost(new InternalVkApiWallPostRequestParams
                                           {
                                               OwnerId = -180815810,
                                               Message = "client remote post test"
                                           });

            response.PostId.Should().NotBe(0);
        }
    }
}