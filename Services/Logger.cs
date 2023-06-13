using System;
using System.Diagnostics;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.VisualBasic.Logging;

namespace Windows.Service.Logging
{
    public class Logger
    {
        private static FileLogTraceListener twtl;

        private static void InitializeLogger()
        {
            twtl = new FileLogTraceListener("Logger");
            twtl.AutoFlush = true;
            // log file name will be current directory + assembly name + timestamp
            twtl.BaseFileName = string.Format("{0}\\{1}", Directory.GetCurrentDirectory(), Assembly.GetEntryAssembly().GetName().Name);
            twtl.DiskSpaceExhaustedBehavior = DiskSpaceExhaustedOption.ThrowException;
            twtl.LogFileCreationSchedule = LogFileCreationScheduleOption.Daily;
            twtl.MaxFileSize = 10485760; // 10 MB

            LogInfo("Logger successfully initialized");
        }


        private Logger()
        {
        }

        public static void LogInfo(String s)
        {
            if (twtl == null) 
                InitializeLogger();

            twtl.WriteLine(string.Format("{0}  I  {1}  {2}",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Assembly.GetCallingAssembly().GetName().Name, s));
        }

        public static void LogWarning(String s)
        {
            if (twtl == null)
                InitializeLogger();

            twtl.WriteLine(string.Format("{0}  W  {1}  {2}",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Assembly.GetCallingAssembly().GetName().Name, s));
        }

        public static void LogError(String s)
        {
            if (twtl == null)
                InitializeLogger();

            twtl.WriteLine(string.Format("{0}  E  {1}  {2}",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Assembly.GetCallingAssembly().GetName().Name, s));
        }
    }
}
