using System.Collections.Generic;
using UnityEditor;

namespace Core.Bundle
{
    public class BundleBuildSetting
    {
        public BuildTarget BuildTarget;
        
        public string OutputFolder;
        
        public bool UseCache;

        public List<AssetBundleBuild> AllAssetBundleBuild = new List<AssetBundleBuild>();
    }
}