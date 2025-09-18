using System;
using Core.Managers;
using UnityEngine;

namespace Core.Utils
{
    /// <summary>
    /// 日志扩展
    /// </summary>
    public class D
    {
        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="format">文本</param>
        public static void Log(string format, params object[] parameters)
        {
            if (!SwitchManager.Instance.GetSwitch(ConstManager.StringConst.SWITCH_DEBUG))
            {
                return;
            }
            
            UtilLog(format, parameters);
        }

        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="format">文本</param>
        public static void Warning(string format, params object[] parameters)
        {
            if (!SwitchManager.Instance.GetSwitch(ConstManager.StringConst.SWITCH_DEBUG))
            {
                return;
            }
            
            UtilWarning(format, parameters);
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="format">文本</param>
        public static void Error(string format, params object[] parameters)
        {
            if (!SwitchManager.Instance.GetSwitch(ConstManager.StringConst.SWITCH_DEBUG))
            {
                return;
            }
            
            UtilError(format, parameters);
        }
        
        /// <summary>
        /// 开发日志
        /// </summary>
        /// <param name="format">文本</param>
        public static void DevLog(string format, params object[] parameters)
        {
            if (!SwitchManager.Instance.GetSwitch(ConstManager.StringConst.SWITCH_DEBUG))
            {
                return;
            }
            
            UtilLog(format, parameters);
        }

        /// <summary>
        /// 开发警告
        /// </summary>
        /// <param name="format">文本</param>
        public static void DevWarning(string format, params object[] parameters)
        {
            if (!SwitchManager.Instance.GetSwitch(ConstManager.StringConst.SWITCH_DEBUG))
            {
                return;
            }
            
            UtilWarning(format, parameters);
        }

        /// <summary>
        /// 开发错误
        /// </summary>
        /// <param name="format">文本</param>
        public static void DevError(string format, params object[] parameters)
        {
            if (!SwitchManager.Instance.GetSwitch(ConstManager.StringConst.SWITCH_DEBUG))
            {
                return;
            }
            
            UtilError(format, parameters);
        }

        /// <summary>
        /// 编辑器日志
        /// </summary>
        /// <param name="format">文本</param>
        public static void EditorLog(string format, params object[] parameters)
        {
#if UNITY_EDITOR
            UtilLog(format, parameters);
#endif
        }

        /// <summary>
        /// 编辑器警告
        /// </summary>
        /// <param name="format">文本</param>
        public static void EditorWarning(string format, params object[] parameters)
        {
#if UNITY_EDITOR
            UtilWarning(format, parameters);
#endif
        }

        /// <summary>
        /// 编辑器错误
        /// </summary>
        /// <param name="format">文本</param>
        public static void EditorError(string format, params object[] parameters)
        {
#if UNITY_EDITOR
            UtilError(format, parameters);
#endif
        }

        public static void BuildLog(string format, params object[] parameters)
        {
#if UNITY_EDITOR
            UtilLog(format, parameters);
#else
            UtilLog($"Build Log, Time:{DateTime.Now} \n" + format, parameters);
#endif
        }

        public static void BuildWarning(string format, params object[] parameters)
        {
#if UNITY_EDITOR
            UtilWarning(format, parameters);
#else
            UtilWarning($"Build Warning, Time:{DateTime.Now} \n" + format, parameters);
#endif
        }

        public static void BuildError(string format, params object[] parameters)
        {
#if UNITY_EDITOR
            UtilError(format, parameters);
#else
           UtilError($"Build Error, Time:{DateTime.Now} \n" + format, parameters);
#endif
        }

        private static void UtilLog(string format, params object[] parameters)
        {
            Debug.Log(string.Format(format, parameters));
        }
        
        private static void UtilWarning(string format, params object[] parameters)
        {
            Debug.LogWarning(string.Format(format, parameters));
        }

        private static void UtilError(string format, params object[] parameters)
        {
            Debug.LogError(string.Format(format, parameters));
        }
    }
}