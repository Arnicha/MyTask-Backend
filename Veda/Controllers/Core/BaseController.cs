using Microsoft.AspNetCore.Mvc;
using mytask.ExceptionBase;

namespace mytask.Controllers
{
    public class BaseController : Controller
    {
        protected long GetUserId()
        {
            try
            {
                string user = Request.Headers["userId"];
                long userId = long.Parse(user);
                return userId;
            }
            catch
            {
                throw new ValidationException("กรุณาใส่ Headers userId");
            }
        }
    }
}
