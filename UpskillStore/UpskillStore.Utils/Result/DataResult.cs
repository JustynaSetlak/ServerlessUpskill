namespace UpskillStore.Utils.Result
{
    public class DataResult<T> : Result where T : class
    {
        public DataResult(bool isSuccessful, T value) : base(isSuccessful)
        {
            this.Value = value;
        }

        public T Value { get; }
    }
}
