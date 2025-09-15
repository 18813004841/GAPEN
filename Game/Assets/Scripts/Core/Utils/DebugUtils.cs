using Core.Managers;

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
        /// <param name="str">文本</param>
        public static void Log(string str)
        {
            if (!SwitchManager.Instance.GetSwitch(ConstManager.StringConst.SWITCH_DEBUG))
            {
                return;
            }
        }

        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="str">文本</param>
        public static void Warning(string str)
        {
            if (!SwitchManager.Instance.GetSwitch(ConstManager.StringConst.SWITCH_DEBUG))
            {
                return;
            }
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="str">文本</param>
        public static void Error(string str)
        {
            if (!SwitchManager.Instance.GetSwitch(ConstManager.StringConst.SWITCH_DEBUG))
            {
                return;
            }
        }
        
        /// <summary>
        /// 开发日志
        /// </summary>
        /// <param name="str">文本</param>
        public static void DevLog(string str)
        {
            if (!SwitchManager.Instance.GetSwitch(ConstManager.StringConst.SWITCH_DEBUG))
            {
                return;
            }
        }

        /// <summary>
        /// 开发警告
        /// </summary>
        /// <param name="str">文本</param>
        public static void DevWarning(string str)
        {
            if (!SwitchManager.Instance.GetSwitch(ConstManager.StringConst.SWITCH_DEBUG))
            {
                return;
            }
        }

        /// <summary>
        /// 开发错误
        /// </summary>
        /// <param name="str">文本</param>
        public static void DevError(string str)
        {
            if (!SwitchManager.Instance.GetSwitch(ConstManager.StringConst.SWITCH_DEBUG))
            {
                return;
            }
        }

        /// <summary>
        /// 编辑器日志
        /// </summary>
        /// <param name="str">文本</param>
        public static void EditorLog(string str)
        {
#if UNITY_EDITOR
            
#endif
        }

        /// <summary>
        /// 编辑器警告
        /// </summary>
        /// <param name="str">文本</param>
        public static void EditorWarning(string str)
        {
#if UNITY_EDITOR
            
#endif
        }

        /// <summary>
        /// 编辑器错误
        /// </summary>
        /// <param name="str">文本</param>
        public static void EditorError(string str)
        {
#if UNITY_EDITOR
            
#endif
        }
    }
}