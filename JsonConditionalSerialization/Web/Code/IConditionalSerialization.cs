
namespace JsonConditionalSerialization.Code
{
    public interface ISerializedPropertyNameCollection
    {
        bool Contains(string propertyName);
    }

    public interface IConditionallySerialized
    {
        ISerializedPropertyNameCollection SerializedPropertyNameCollection { get; }
    }
}
