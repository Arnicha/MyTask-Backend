namespace mytask.ExceptionBase
{
    public class NotFoundException : BaseException
    {
        public NotFoundException() : base("000001", "ไม่พบข้อมูล")
        {
        }

        public NotFoundException(string message) : base("000001", message)
        {
        }
    }
}
