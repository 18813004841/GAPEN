using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEditor.Build.Pipeline;
using UnityEditor.Build.Pipeline.Interfaces;
using UnityEngine;
using UnityEngine.Build.Pipeline;

namespace Core.Bundle
{
    public class DefaultBundleBuildAgent : BundleBuildWrapper
    {
        protected override BundleBuildSetting CreateBuildParameters(BuildTarget buildTarget, string outputFolder)
        {
            var allBundles = ContentBuildInterface.GenerateAssetBundleBuilds();
            
            BundleBuildSetting setting = new BundleBuildSetting();
            setting.BuildTarget = buildTarget;
            setting.AllAssetBundleBuild = allBundles.ToList();
            setting.OutputFolder = outputFolder;

            return setting;
        }

        protected override void CollectManifest(IBundleBuildResults results, string outputPath)
        {
            var manifest = ScriptableObject.CreateInstance<CompatibilityAssetBundleManifest>();
            manifest.SetResults(results.BundleInfos);
            AssetDatabase.CreateAsset(manifest, outputPath);
            Debug.Log($"Generating AssetBundleManifest: {outputPath}");
        }
    }
}