using System;
using System.Linq;
using mytask.Repository;
using MyTask.Models.Entity;

namespace MyTask.BusinessFlow
{
    public class HealthCheckBusinessFlow
    {
        private readonly IBaseRepository baseRepository;
        public HealthCheckBusinessFlow(IBaseRepository baseRepository)
        {
            this.baseRepository = baseRepository;
        }
        public string HealthCheck()
        {
            return this.baseRepository.Gets<HealthCheckEntity>().FirstOrDefault().statusMessage;
        }
    }
}
