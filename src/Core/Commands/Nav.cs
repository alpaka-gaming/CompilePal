using System.Threading;
using Core.Commands;
using Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Core.Process
{
    public class Nav : Generic
    {

        public Nav(ILoggerFactory loggerFactory, ICompileContext compileContext) : base(loggerFactory, compileContext) { }
        public override void Run(CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
        public override void Cancel()
        {
            throw new System.NotImplementedException();
        }
    }
}
