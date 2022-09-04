using System.Threading.Tasks;
using Core.Models;

namespace Core.Interfaces
{
    public interface ISettingManager
    {
        Task LoadAsync();
        Task SaveAsync();

        string SubstituteValues(Setting setting, string text, string mapFile);
    }
}
