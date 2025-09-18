using System.Collections.Generic;
using System.Text;
using Core.Utils;

namespace Core.BuildManager
{
    public abstract class BuildStep
    {
        public enum Status
        {
            Wait,
            Skipped,
            Success,
            Failed,
        }
        
        protected BuildManager BuildManager;
        protected Status _status;
        
        protected List<BuildStep> _allDependence = new List<BuildStep>();
        protected StringBuilder _finalReport = new StringBuilder();
        
        protected BuildStep(BuildManager buildManager)
        {
            BuildManager = buildManager;
            _status = Status.Wait;
        }
        
        public void AddDependence(BuildStep buildStep)
        {
            _allDependence.Add(buildStep);    
        }
        
        public bool IsAllDependenceReady
        {
            get
            {
                foreach(var dependence in _allDependence)
                {
                    if (dependence._status == Status.Wait)
                    {
                        return false;
                    }
                }

                return true;
            }
        }
        
        public virtual string Name
        {
            get
            {
                return this.GetType().Name.ToString();
            }
        }
        
        public bool DoBuild()
        {
            bool result = DoBuildImplement();
            _status = result ? Status.Success : Status.Failed;
            return result;
        }
        
        public string FinalReport
        {
            get { return _finalReport.ToString(); }
        }
        
        /// <summary>
        /// 日志会追加到buildlog最后,并通体现在钉钉通知上.
        /// </summary>
        /// <param name="report"></param>
        protected void AddFinalReport(string report)
        {
            lock (_finalReport)
            {
                _finalReport.AppendLine(report);
            }
        }
        
        protected void AddFinalReportKeyValue(string key, string value)
        {
            AddFinalReport($"**{key}**:{value}");
        }
        
        protected void SendDingTalkMessage(string content)
        {
            // BuildManager.SendDingTalkMessage(content); 
        }
        
        protected bool ExecuteExternalCmd(string externalAppPath, string parameters)
        {
            D.BuildLog("ExecuteExternalCmd:{0} {1}", externalAppPath, parameters);
            return Utility.ExecuteExternalCmd(externalAppPath, parameters);
        }
        
        protected abstract bool DoBuildImplement();
    }
}