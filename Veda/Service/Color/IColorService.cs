using MyTask.Models.Entity;

namespace MyTask.Service.Color
{
    public interface IColorService
    {
        ColorsEntity GetColorByCoverColorId(int coverColorId);
    }
}
