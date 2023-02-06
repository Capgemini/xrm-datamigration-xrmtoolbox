using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace Capgemini.Xrm.CdsDataMigratorLibrary.Tests.Unit.Extensions
{
    internal static class MockWorkerHostExtensions
    {
        public static void ExecuteWork(this Mock<IWorkerHost> mockWorkerHost, int invocationIndex)
        {
            var workInfo = mockWorkerHost.Invocations[invocationIndex].Arguments[0].As<WorkAsyncInfo>();

            var doWorkArgs = new DoWorkEventArgs(workInfo.AsyncArgument);
            Exception error = null;
            try
            {
                workInfo?.Work(null, doWorkArgs);
            }
            catch (Exception ex)
            {
                error = ex;
            }

            workInfo?.PostWorkCallBack(new RunWorkerCompletedEventArgs(doWorkArgs.Result, error, false));
        }
    }
}
