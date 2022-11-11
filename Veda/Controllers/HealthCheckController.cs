using Microsoft.AspNetCore.Mvc;
using MyTask.BusinessFlow;

namespace mytask.Controllers
{

    public class HealthCheckController : Controller
    {
        private readonly HealthCheckBusinessFlow _healthCheckBussinessFlow;

        public HealthCheckController(HealthCheckBusinessFlow healthCheckBussinessFlow)
        {
            this._healthCheckBussinessFlow = healthCheckBussinessFlow;
        }
        [HttpGet("healthcheck")]
        public string HealthCheck()
        {
            return _healthCheckBussinessFlow.HealthCheck();
        }
    }
}
