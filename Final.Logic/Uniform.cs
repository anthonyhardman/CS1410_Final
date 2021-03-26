namespace Final.Logic
{
    public class Uniform<T>
    {
        public Uniform(string name, T value)
        {
            Name = name;
            Value = value;
        }

       
        public string Name { get; set; }
        public T Value { get; set; }
    }
}