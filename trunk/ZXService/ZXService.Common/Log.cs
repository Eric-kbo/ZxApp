using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = "Log4Net.config", Watch = true)]
namespace ZXService.Common
{ 
    public class Log : ILoggerService
    {
        private ILog _logger;

        public Log()
        {
            _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public Log(object obj)
        {
            _logger = LogManager.GetLogger(obj.GetType());
        }

        public static Log GetLogService()
        {
            return new Log();
        }

        public static Log GetLogService(object obj)
        {
            return new Log(obj);
        }




        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(Exception ex)
        {
            _logger.Error(ex);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(Exception ex)
        {
            _logger.Fatal(ex);
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }
    }
}
