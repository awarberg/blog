namespace JsonConditionalSerialization.Code
{
    public interface IConditionallySerialized
    {
        ISerializedPropertyNameCollection SerializedPropertyNameCollection { get; }
    }

    public interface ISerializedPropertyNameCollection
    {
        bool Contains(string propertyName);
    }
}
