namespace UpskillStore.Utils.Result
{
    public class ErrorDataResult<T> : DataResult<T> where T : class
    {
        public ErrorDataResult(T errorData) : base(false, errorData)
        {
        }
    }
}
