using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace address_optimizer
{
    public static partial class Log
    {
        [LoggerMessage(20, LogLevel.Information, "Requested at {date}.")]
        public static partial void Requested(this ILogger logger, DateTime date);
    }
}