namespace HouseApi.Models
{
    public class ValidationInput<T>
    {
        public string Key { get; }
        public T? Value { get; }

        public ValidationInput(string key, T value)
        {
            Key = key;
            Value = value;
        }

        /// <summary>
        /// For convenience values can implicitly convert to validation inputs.
        /// </summary>
        public static implicit operator ValidationInput<T>(T value) =>
            new ValidationInput<T>("input", value);
    }

    public class ValidationInputFactory
    {
        public static ValidationInput<T> Create<T>(string key, T value) =>
            new ValidationInput<T>(key, value);
    }
}
