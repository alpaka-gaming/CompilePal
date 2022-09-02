namespace Core.Interfaces
{
    public interface ICommand
    {
        ICompileContext Context { get; }
        System.Diagnostics.Process Process { get; }
    }
}
