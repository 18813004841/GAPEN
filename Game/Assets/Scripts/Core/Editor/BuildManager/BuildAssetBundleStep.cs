using System.IO;
using Core.Bundle;
using Core.Managers;
using Core.Utils;
using UnityEditor;

namespace Core.BuildManager
{
    public class BuildAssetBundleStep : BuildStep
    {
        private BundleBuildWrapper _bundleBuilder;
        
        public BuildAssetBundleStep(BuildManager buildManager) : base(buildManager)
        {
            _bundleBuilder = new AutoBundleAgent();
        }

        protected override bool DoBuildImplement()
        {
            D.BuildLog("Build asset bundle start.");
            if (!Directory.Exists(EditorConstManager.StringConst.Bundle_BundleOutFolder))
            {
                Directory.CreateDirectory(EditorConstManager.StringConst.Bundle_BundleOutFolder);
            }
            if (!Directory.Exists(EditorConstManager.StringConst.Bundle_ManifestOutFolder))
            {
                Directory.CreateDirectory(EditorConstManager.StringConst.Bundle_ManifestOutFolder);
            }

            string manifestPath = Path.Combine(EditorConstManager.StringConst.Bundle_ManifestOutFolder,
                "AssetBundlesManifest.asset");
            manifestPath = manifestPath.Replace("\\", "/");
            
            _bundleBuilder.Build(BuildTarget.Android, EditorConstManager.StringConst.Bundle_BundleOutFolder, manifestPath);
            return true;
        }
        
        [MenuItem("Tools/Build/BuildAssetBundle")]
        public static void BuildAssetBundleTool()
        {
            var step = new BuildAssetBundleStep(null);
            step.DoBuild();
        }
    }
}