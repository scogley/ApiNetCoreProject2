using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.IO;

namespace HelloWorldService
{
    public class LoggingActionFilter : IActionFilter
    {
        private System.Diagnostics.Stopwatch stopwatch;

        private IHostingEnvironment env;

        public LoggingActionFilter(IHostingEnvironment env)
        {
            this.env = env;
        }

        public void OnActionExecuting(ActionExecutingContext actionContext)
        {
            stopwatch = System.Diagnostics.Stopwatch.StartNew();
        }

        public void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            stopwatch.Stop();

            var webroot = env.WebRootPath;
            var filepath = Path.Combine(webroot, "logger.txt");
            var controller = (ControllerBase)actionExecutedContext.Controller;
            var controllername = controller.ToString();
            var actionName = controller.Request.Method;
            var result = actionExecutedContext.Result;
            var logline = string.Format("{0} : {1} {2} Elapsed={3}\n",
                controllername,
                actionName,
                System.DateTime.Now, stopwatch.Elapsed);

            File.AppendAllText(filepath, logline);
        }
    }
}