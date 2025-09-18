using System;
using System.Diagnostics;

namespace Core.Utils
{
    public class Utility
    {
        public static bool ExecuteExternalCmd(string externalAppPath, string parameters)
        {
            try
            {
                D.BuildLog("ExecuteExternalCmd:", externalAppPath, parameters);
                
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = externalAppPath;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.Arguments = parameters;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.Start();
                    
                    var output = process.StandardOutput.ReadToEnd();
                    
                    D.BuildLog(output);
                    process.WaitForExit();
                    return process.ExitCode == 0;
                }
                
            }
            catch (Exception exception)
            {
                throw new SystemException(string.Format("ExecuteExternalCmd({0} {1}\n{2}) Exception", 
                    externalAppPath, parameters, exception.Message));
            }
            
            return false;
        }
    }
}