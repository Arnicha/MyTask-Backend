using Microsoft.EntityFrameworkCore;
using MyTask.Models.Entity;

namespace mytask.Data
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions options) : base(options)
        {
        }
        // เวลาสร้าง model Entity ใหม่ มาใส่ในนี้ด้วย
        //แบบนี้
        public DbSet<HealthCheckEntity> healthCheckEntity { get; set; }
        public DbSet<TaskEntity> taskEntity { get; set; }
        public DbSet<TodolistEntity> todoEntity { get; set; }
        public DbSet<ColorsEntity> colorsEntity { get; set; }
    }
}
