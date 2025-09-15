using System.Collections.Generic;
using Core.Single;

namespace Core.Managers
{
    /// <summary>
    /// 开关管理器
    /// </summary>
    public class SwitchManager : Singleton<SwitchManager>
    {
        /// <summary>
        /// 开关列表
        /// </summary>
        private Dictionary<string, bool> _switchList = new Dictionary<string, bool>();

        /// <summary>
        /// 设置开关
        /// </summary>
        /// <param name="key">开关名称</param>
        /// <param name="active">开启状态</param>
        public void SetSwitch(string key, bool active)
        {
            _switchList[key] = active;
        }

        /// <summary>
        /// 获取开关
        /// </summary>
        /// <param name="key">开关名称</param>
        /// <returns>开启状态</returns>
        public bool GetSwitch(string key)
        {
            if (!_switchList.ContainsKey(key))
            {
                return false;
            }

            return _switchList[key];
        }
    }
}