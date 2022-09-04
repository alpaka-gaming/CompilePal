using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface ICommand
    {
        ICompileContext Context { get; }
        System.Diagnostics.Process Process { get; }
        string Name { get; set; }
        Task StartAsync();
    }
}
