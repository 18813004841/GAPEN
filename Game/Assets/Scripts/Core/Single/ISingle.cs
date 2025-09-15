namespace Core.Single
{
    /// <summary>
    /// 单例必备接口
    /// </summary>
    public interface ISingle
    {
        /// <summary>
        /// 释放
        /// </summary>
        public void Release();
    }
}