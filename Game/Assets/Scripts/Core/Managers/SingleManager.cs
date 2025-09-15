using System.Collections.Generic;
using Core.Single;

namespace Core.Managers
{
    /// <summary>
    /// 单例管理器
    /// </summary>
    public class SingleManager
    {
        public List<ISingle> Singles = new List<ISingle>();

        public void AddSingle(ISingle single)
        {
            Singles.Add(single);
        }

        public void Release()
        {
            for (int i = 0; i < Singles.Count; i++)
            {
                Singles[i].Release();
            }
            Singles.Clear();
        }
    }
}