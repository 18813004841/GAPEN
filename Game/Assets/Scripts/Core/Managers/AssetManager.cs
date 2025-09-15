using System.Collections.Generic;
using Core.Single;
using UnityEngine;

namespace Core.Managers
{
    /// <summary>
    /// 资源管理器
    /// </summary>
    public class AssetManager : Singleton<AssetManager>
    {
        /// <summary>
        /// 资源加载方式
        /// </summary>
        public enum AssetModel
        {
            /// <summary>
            /// bundle 加载
            /// </summary>
            Bundle,

            /// <summary>
            /// 直接资源加载
            /// </summary>
            Resource
        }

        // /// <summary>
        // /// 加载资源
        // /// </summary>
        // /// <param name="fullName">资源地址</param>
        // /// <typeparam name="T">资源类型</typeparam>
        // public T LoadAsset<T>(string fullName) where T : Object
        // {
        //     
        // }
    }
}