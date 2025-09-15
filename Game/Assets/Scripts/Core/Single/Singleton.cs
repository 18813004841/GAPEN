namespace Core.Single
{
    /// <summary>
    /// 单例基类
    /// </summary>
    /// <typeparam name="T"> 子类 </typeparam>
    public class Singleton<T> : ISingle where T:new()
    {
        private static T _instance;
        
        /// <summary>
        /// 单例
        /// </summary>
        public static T Instance
        {
            get
            {
                if (null == _instance)
                {
                    _instance = new T();
                }

                return _instance;
            }
        }
        
        protected virtual void OnApplicationQuit()
        {
            _instance = default(T); // 在应用程序退出时重置实例
        }

        public virtual void Release()
        {
            
        }
    }
}