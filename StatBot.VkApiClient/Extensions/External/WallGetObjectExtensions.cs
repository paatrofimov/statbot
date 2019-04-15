using System.Linq;
using StatBot.VkApiClient.Models.Internal;
using VkNet.Model;
using VkNet.Model.Attachments;

namespace StatBot.VkApiClient.Extensions.External
{
    public static class WallGetObjectExtensions
    {
        public static InternalVkApiWallGetObject ToInternal(this WallGetObject wallGetObject)
        {
            return new InternalVkApiWallGetObject
                   {
                       WallApiPosts = wallGetObject.WallPosts.Select(ToInternal).ToArray()
                   };
        }

        private static InternalVkApiPost ToInternal(Post post)
        {
            return new InternalVkApiPost
                   {
                       Text = post.Text
                   };
        }
    }
}