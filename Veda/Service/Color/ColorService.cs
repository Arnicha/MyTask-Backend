using mytask.Repository;
using MyTask.Models.Entity;
using System.Linq;

namespace MyTask.Service.Color
{
    public class ColorService : IColorService
    {
        private readonly IBaseRepository baseRepository;
        public ColorService(IBaseRepository baseRepository)
        {
            this.baseRepository = baseRepository;
        }
        public ColorsEntity GetColorByCoverColorId(int coverColorId)
        {
            ColorsEntity coverColor = baseRepository.Gets<ColorsEntity>().FirstOrDefault(a => a.id == coverColorId);
            return coverColor;
        }
    }
}
