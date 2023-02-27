using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace StateMachine.UnitTests.workflow
{
    public class WorkflowManagerUnitTests
    {
        private ILogger<WorkflowManagerUnitTests> logger;
        
        public WorkflowManagerUnitTests()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();
            //get logger
            logger = serviceProvider.GetService<ILoggerFactory>()
                .CreateLogger<WorkflowManagerUnitTests>();
        }
    }
}
