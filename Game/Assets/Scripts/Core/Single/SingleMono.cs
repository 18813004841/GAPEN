using UnityEngine;

namespace Core.Single
{
    /// <summary>
    /// 单例Mono基类
    /// </summary>
    /// <typeparam name="T"> MonoBehaviour 子类 </typeparam>
    public class SingleMono<T> : MonoBehaviour, ISingle where T : MonoBehaviour
    {
        private static T _instance;

        /// <summary>
        /// 单例
        /// </summary>
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    // 查找场景中的现有实例
                    _instance = FindObjectOfType<T>();

                    if (_instance == null)
                    {
                        // 如果没有，创建一个新的实例
                        GameObject singletonObject = new GameObject(typeof(T).Name);
                        _instance = singletonObject.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                DontDestroyOnLoad(gameObject); // 不销毁该对象
            }
            else if (_instance != this)
            {
                Destroy(gameObject); // 如果已经存在实例，销毁当前对象
            }
        }

        protected virtual void OnApplicationQuit()
        {
            _instance = null; // 在应用程序退出时重置实例
        }

        public virtual void Release()
        {
            
        }
    }
}