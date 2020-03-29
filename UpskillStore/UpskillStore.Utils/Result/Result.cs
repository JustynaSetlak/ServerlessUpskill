namespace UpskillStore.Utils.Result
{
    public class Result
    {
        public Result(bool isSuccessful)
        {
            this.IsSuccessful = isSuccessful;
        }

        public bool IsSuccessful { get; }
    }
}
