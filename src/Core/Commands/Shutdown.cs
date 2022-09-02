using System.Threading;
using Core.Commands;
using Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Core.Process
{
    public class Shutdown : Generic
    {

        public Shutdown(ILoggerFactory loggerFactory, ICompileContext compileContext) : base(loggerFactory, compileContext) { }
        public override void Run(CancellationToken cancellationToken)
        {
            // if (!CanRun())
            //     return;
            //
            // if (cancellationToken.IsCancellationRequested)
            //     return;
            //
            // _logger.LogInformation("Shutdown");
            //
            // // don't run unless it's the last map of the queue
            // if (_compileContext.CompilingManager.MapFiles.Last().File == _compileContext.MapFile)
            // {
            //     _logger.LogInformation("The system will shutdown soon. You can cancel this shutdown by using the command \"shutdown -a\"");
            //
            //     var startInfo = new ProcessStartInfo("shutdown", GetParameterString());
            //     startInfo.UseShellExecute = false;
            //     startInfo.CreateNoWindow = true;
            //
            //     Core.Process = new System.Diagnostics.Process { StartInfo = startInfo };
            //     Core.Process.Start();
            // }
        }
        public override void Cancel()
        {
            throw new System.NotImplementedException();
        }
    }
}
