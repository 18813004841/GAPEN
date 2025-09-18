using System.Collections.Generic;
using System.IO;
using System.Linq;
using Core.Utils;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Build.Pipeline.Interfaces;
using UnityEngine;
using UnityEngine.Build.Pipeline;

namespace Core.Bundle
{
    public class AutoBundleAgent : BundleBuildWrapper
    {
        private static List<string> _allAssetFolders = new List<string>
        {
            "Assets/__Art", //直接应用资源
            "Assets/__Res", //加载资源
        };
        
        private static List<string> _excludeAssetFolders = new List<string>
        {
            "Assets/__Art/Editor",
            "Assets/__Res/Editor",
        };
        
        private static List<string> _excludeAssetExt = new List<string>
        {
            ".cs",
            ".cginc",
        };
        
        private Dictionary<string, HashSet<string>> allPackedAssetPaths = new Dictionary<string, HashSet<string>>();

        protected override BundleBuildSetting CreateBuildParameters(BuildTarget buildTarget, string outputFolder)
        {
            var allAssetGuid = new HashSet<string>();
            allAssetGuid.AddRange(AssetDatabase.FindAssets("", _allAssetFolders.ToArray()));

            var counter = 0;
            var count = allAssetGuid.Count;

            foreach (var assetGuid in allAssetGuid)
            {
                if (counter % 100 == 0)
                {
                    EditorUtility.DisplayProgressBar("构建资源", $"分析资源中:{counter}/{count}", counter / (float)count);
                }

                counter++;

                var assetPath = AssetDatabase.GUIDToAssetPath(assetGuid);

                if (!NeedPack(assetPath))
                {
                    continue;
                }

                var bundleName = CalcAssetBundleName(assetPath);
                if (string.IsNullOrEmpty(bundleName))
                {
                    D.BuildError("无法计算资源的BundleName: " + assetPath);
                    continue;
                }

                if (TryAddPackedAsset(bundleName, assetPath))
                {
                    var allDependAssetPath = AssetDatabase.GetDependencies(assetPath);

                    foreach (var dependAssetPath in allDependAssetPath)
                    {
                        if (!NeedPack(dependAssetPath))
                        {
                            continue;
                        }
                    }
                }
            }
            
            var allAssetBundleBuild = new List<AssetBundleBuild>(allPackedAssetPaths.Count);

            foreach (var keyValue in allPackedAssetPaths)
            {
                var bundleName = keyValue.Key;
                var allAssetPath = keyValue.Value;
                var allAssetName = allAssetPath.ToArray();

                var assetBundleBuild = new AssetBundleBuild()
                {
                    assetBundleName = bundleName,
                    assetNames = allAssetName,
                    addressableNames = allAssetName.Select(Path.GetFileNameWithoutExtension).ToArray()
                };
            }

            BundleBuildSetting setting = new BundleBuildSetting();
            setting.BuildTarget = buildTarget;
            setting.OutputFolder = outputFolder;
            setting.AllAssetBundleBuild = allAssetBundleBuild;
            
            EditorUtility.ClearProgressBar();
            return setting;
        }

        protected override void CollectManifest(IBundleBuildResults results, string outputPath)
        {
            var manifest = ScriptableObject.CreateInstance<CompatibilityAssetBundleManifest>();
            manifest.SetResults(results.BundleInfos);
            AssetDatabase.CreateAsset(manifest, outputPath);
            Debug.Log($"Generating AssetBundleManifest: {outputPath}");
        }
        
        private string CalcAssetBundleName(string assetPath)
        {
            // 所有shader打包到一块
            if (assetPath.EndsWith(".shader") || 
                assetPath.EndsWith(".compute") ||
                assetPath.EndsWith(".shadervariants"))
            {
                return "shaders.ab";
            }

            var parentFolder = Path.GetDirectoryName(assetPath);

            if (null == parentFolder)
            {
                D.BuildError("Could not get parent folder of asset: " + assetPath);
                return string.Empty;
            }
            
            if (parentFolder.Contains("@"))
            {
                parentFolder = parentFolder.Split('@')[0];
                return parentFolder.ToLower() + ".ab";
            }
            
            return parentFolder.ToLower() + ".ab";
        }
        
        bool TryAddPackedAsset(string bundleName,string assetPath)
        {
            if (!allPackedAssetPaths.TryGetValue(bundleName, out var allAssetPath))
            {
                allAssetPath = new HashSet<string>();
                allPackedAssetPaths.Add(bundleName, allAssetPath);
            }
            
            return allAssetPath.Add(assetPath);
        }
        
        private bool NeedPack(string assetPath)
        {
            if (assetPath.Contains("/Editor/"))
            {
                return false;
            }
            
            // 扩展名为空 或 文件夹
            var extension = Path.GetExtension(assetPath);
            if (string.IsNullOrEmpty(extension))
            {
                return false;
            }

            foreach (var folder in _excludeAssetFolders)
            {
                if (assetPath.StartsWith(folder))
                {
                    return false;
                }
            }
            
            foreach (var ext in _excludeAssetExt)
            {
                if (assetPath.EndsWith(ext))
                {
                    return false;
                }
            }

            return true;
        }
    }
}