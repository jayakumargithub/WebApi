using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http.ExceptionHandling;
using log4net;

namespace WebApi
{

    public class Log4NetExceptionLogger : ExceptionLogger
    {
        private static readonly ILog _log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public override void Log(ExceptionLoggerContext context)
        {
            var msg = string.Format("Unhandled exception processing {0} for {1}: {2}", context.Request.Method,
                context.Request.RequestUri,
                context.Exception);
            _log.Info(msg, context.Exception);
        }
    }
    public class TraceSourceExceptionLogger : ExceptionLogger
    {
        private readonly TraceSource _traceSource;

        public TraceSourceExceptionLogger(TraceSource traceSource)
        {
            _traceSource = traceSource;
        }

        public override void Log(ExceptionLoggerContext context)
        {
            _traceSource.TraceEvent(TraceEventType.Error, 1,
                "Unhandled exception processing {0} for {1}: {2}",
                context.Request.Method,
                context.Request.RequestUri,
                context.Exception);
        }
    }

}