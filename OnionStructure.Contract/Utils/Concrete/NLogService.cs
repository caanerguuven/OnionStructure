using OnionStructure.Contract.Utils.Abstract;
using NLog;

namespace OnionStructure.Contract.Utils.Concrete
{
    public class NLogService : ILogService
    {
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();

        public NLogService()
        {
        }

        public void Information(string message)
        {
            logger.Info(message);
        }

        public void Warning(string message)
        {
            logger.Warn(message);
        }

        public void Debug(string message)
        {
            logger.Debug(message);
        }

        public void Error(string message)
        {
            logger.Error(message);
        }  
    }
}
