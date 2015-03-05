using JsonConditionalSerialization.Code;
using Newtonsoft.Json;
using System;
using System.Web.Http;

namespace JsonConditionalSerialization.Controllers
{
    public class BlogController : ApiController
    {
        [HttpGet]
        public BlogPost GetPostsUnconditional()
        {
            var myData = new BlogPost();
            return myData;
        }

        [HttpGet]
        public BlogPost GetPostsConditional()
        {
            var myData = new BlogPost();
            myData.SerializedPropertyCollection = new SerializedPropertyCollection<BlogPost>(o => o.Title, o => o.Url);
            return myData;
        }       

        [JsonObject(MemberSerialization.OptIn)]
        public class BlogPost : IConditionallySerialized
        {
            [JsonProperty]
            public string Title { get; set; }

            [JsonProperty]
            public Uri Url { get; set; }

            [JsonProperty]
            public string Body { get; set; }

            public SerializedPropertyCollection<BlogPost> SerializedPropertyCollection { get; set; }

            public ISerializedPropertyNameCollection SerializedPropertyNameCollection
            {
                get { return SerializedPropertyCollection; }
            }
        }
    }
}
