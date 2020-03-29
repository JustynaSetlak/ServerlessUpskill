namespace UpskillStore.Utils.Result
{
    public class SuccessfulDataResult<T> : DataResult<T> where T : class
    {
        public SuccessfulDataResult(T data) : base(true, data)
        {
        }
    }
}
