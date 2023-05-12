namespace Diploma.Models
{
    public class SerializeModel
    {
        public SerializeModel(object value)
        {
            Value = value;
        }

        public Object Value { get; set; }
    }
}