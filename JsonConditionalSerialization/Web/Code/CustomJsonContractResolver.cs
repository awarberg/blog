using System;
using System.Collections.Generic;
using System.Net.Http.Formatting;

namespace JsonConditionalSerialization.Code
{
    public class CustomJsonContractResolver : System.Net.Http.Formatting.JsonContractResolver
    {
        public CustomJsonContractResolver(MediaTypeFormatter formatter)
            : base(formatter)
        {
        }
        protected override IList<Newtonsoft.Json.Serialization.JsonProperty> CreateProperties(Type type, Newtonsoft.Json.MemberSerialization memberSerialization)
        {
            var jsonProperties = base.CreateProperties(type, memberSerialization);
            if (typeof(IConditionallySerialized).IsAssignableFrom(type))
            {
                foreach (var p in jsonProperties)
                {
                    var jsonProperty = p;
                    jsonProperty.ShouldSerialize = o => ShouldSerializeJsonProperty((IConditionallySerialized)o, jsonProperty);
                }
            }
            return jsonProperties;
        }
        private static bool ShouldSerializeJsonProperty(IConditionallySerialized obj, Newtonsoft.Json.Serialization.JsonProperty jsonProperty)
        {
            var serializedPropertyNameCollection = obj.SerializedPropertyNameCollection;
            return serializedPropertyNameCollection == null || serializedPropertyNameCollection.Contains(jsonProperty.PropertyName);
        }
    }
}