    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Text;
    using System.Diagnostics;
    using System.Configuration;
    using System.IO;

    public class EventLogGestorTranWriter
    {
        public void LogError(Exception ex, string mens)
        {

            string message = string.Format("Time: {0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            string direcion = @"C:\LOG\" + "JWT" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
            if (!File.Exists(direcion))
            {
                FileStream fileStream = new FileStream(@direcion, FileMode.OpenOrCreate, FileAccess.Write);
                fileStream.Close();
            }
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            message += mens;
            message += Environment.NewLine;
            message += string.Format("Message: {0}", ex.Message);
            message += Environment.NewLine;
            message += string.Format("StackTrace: {0}", ex.StackTrace);
            message += Environment.NewLine;
            message += string.Format("Source: {0}", ex.Source);
            message += Environment.NewLine;
            message += string.Format("TargetSite: {0}", ex.TargetSite.ToString());
            message += Environment.NewLine;
            message += "-----------------------------------------------------------";
            string path = @direcion;
            using (StreamWriter writer = File.AppendText(path))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }

        public void Log(string metodo, string mens)
        {
            string message = string.Format("{0}", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"));
            string direcion = @"C:\LOG\" + "JWT" + DateTime.Now.ToString("dd-MM-yyyy") + ".txt";
            message = message + metodo + mens;
            if (!File.Exists(direcion))
            {
                FileStream fileStream = new FileStream(@direcion, FileMode.OpenOrCreate, FileAccess.Write);
                fileStream.Close();
            }
            string path = @direcion;
            using (StreamWriter writer = File.AppendText(path))
            {
                writer.WriteLine(message);
                writer.Close();
            }
        }

    }

    

