namespace Diploma.Models
{
    public class Elem
    {
        public Elem(string name, string value, string type)
        {
            Name = name;
            Value = value;
            Type = type;
        }

        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }
}