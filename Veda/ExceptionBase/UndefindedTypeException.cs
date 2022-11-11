namespace mytask.ExceptionBase
{
    public class UndefindedTypeException : BaseException
    {
        public UndefindedTypeException(string message) : base("000003", message)
        {
        }
    }
}
