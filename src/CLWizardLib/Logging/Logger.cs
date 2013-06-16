using System;
using System.IO;
using System.Reflection;
using log4net;

namespace CLWizardLib.Logging
{
    /// <summary>
    /// ログメッセージを出力するクラス
    /// </summary>
    public static class Logger
    {
        /// <summary>
        /// 致命的エラーのログを出力する
        /// </summary>
        public static void Fatal(Type type, string message, Exception exception = null)
        {
            ILog logger = LogManager.GetLogger(type);

            if (!logger.IsFatalEnabled) return;

            if (exception == null)
            {
                logger.Fatal(message);
            }
            else
            {
                logger.Fatal(message, exception);
            }
        }

        /// <summary>
        /// エラーのログを出力する
        /// </summary>
        public static void Error(Type type, string message, Exception exception = null)
        {
            ILog logger = LogManager.GetLogger(type);

            if (!logger.IsErrorEnabled) return;

            if (exception == null)
            {
                logger.Error(message);
            }
            else
            {
                logger.Error(message, exception);
            }
        }

        /// <summary>
        /// 警告のログを出力する
        /// </summary>
        public static void Warn(Type type, string message, Exception exception = null)
        {
            ILog logger = LogManager.GetLogger(type);

            if (!logger.IsWarnEnabled) return;

            if (exception == null)
            {
                logger.Warn(message);
            }
            else
            {
                logger.Warn(message, exception);
            }
        }

        /// <summary>
        /// 情報のログを出力する
        /// </summary>
        public static void Info(Type type, string message, Exception exception = null)
        {
            ILog logger = LogManager.GetLogger(type);

            if (!logger.IsInfoEnabled) return;

            if (exception == null)
            {
                logger.Info(message);
            }
            else
            {
                logger.Info(message, exception);
            }
        }

        /// <summary>
        /// デバッグのログを出力する
        /// </summary>
        public static void Debug(Type type, string message, Exception exception = null)
        {
            ILog logger = LogManager.GetLogger(type);

            if (!logger.IsDebugEnabled) return;

            if (exception == null)
            {
                logger.Debug(message);
            }
            else
            {
                logger.Debug(message, exception);
            }
        }
    }
}
