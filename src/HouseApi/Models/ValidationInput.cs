using System;

namespace HouseApi.Models
{
    public class ModelValidationInput<T> where T : class
    {
        private readonly Func<ModelBuilderResult<T>> _modelGetter;

        public ModelValidationInput(Func<ModelBuilderResult<T>> modelGetter)
        {
            _modelGetter = modelGetter;
        }

        public ModelValidationInput(T value)
        {
            _modelGetter = () => new ModelBuilderResult<T>(value);
        }

        public ModelBuilderResult<T> TryGetModel() => _modelGetter();

        /// <summary>
        /// For convenience values can implicitly convert to validation inputs.
        /// </summary>
        public static implicit operator ModelValidationInput<T>(T value) =>
            new ModelValidationInput<T>(value);
    }

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
}
