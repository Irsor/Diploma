namespace Diploma.Models
{
    public class SerializeModel
    {
        public SerializeModel(string name, object value)
        {
            Name = name;
            Value = value;
        }

        public string Name { get; set; }
        public Object Value { get; set; }
    }
}
