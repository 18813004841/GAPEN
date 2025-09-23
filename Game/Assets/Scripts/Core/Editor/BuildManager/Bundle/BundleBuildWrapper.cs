using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build.Pipeline;
using UnityEditor.Build.Pipeline.Interfaces;
using UnityEditor.SceneManagement;
using UnityEngine;
using BuildCompression = UnityEngine.BuildCompression;

namespace Core.Bundle
{
    public abstract class BundleBuildWrapper
    {
        public virtual bool Build(BuildTarget buildTarget, string bundleFolder, string manifestPath)
        {
            EditorSceneManager.SaveOpenScenes();
            
            var setting = CreateBuildParameters(buildTarget, bundleFolder);
            if (setting == null)
            {
                Debug.LogError("Bundle没有找到打包设置");
                return false;
            }

            if (setting.AllAssetBundleBuild.Count == 0)
            {
                Debug.LogError("Bundle没有设置任何打包内容");
                return false;
            }

            if (string.IsNullOrEmpty(setting.OutputFolder))
            {
                Debug.LogError("Bundle没有设置输出目录");
                return false;
            }

            if (!System.IO.Directory.Exists(setting.OutputFolder))
            {
                System.IO.Directory.CreateDirectory(setting.OutputFolder);
            }

            if (!Build(setting, out var results))
            {
                Debug.LogError("Bundle打包失败");
                return false;
            }
            
            CollectManifest(results, manifestPath);
            
            Debug.Log("Bundle打包成功");
            return true;    
        }
        
        protected virtual bool Build(BundleBuildSetting setting, out IBundleBuildResults results)
        {
            Debug.Log("开始构建bundle");
            
            var buildTargetGroupMapper = new Dictionary<BuildTarget, BuildTargetGroup>()
            {
                {BuildTarget.Android, BuildTargetGroup.Android},
                {BuildTarget.iOS, BuildTargetGroup.iOS},
                {BuildTarget.StandaloneWindows, BuildTargetGroup.Standalone},
                {BuildTarget.StandaloneWindows64, BuildTargetGroup.Standalone},
                {BuildTarget.WebGL, BuildTargetGroup.WebGL},
            };

            if (!buildTargetGroupMapper.TryGetValue(setting.BuildTarget, out var buildGroup))
            {
                throw new System.Exception("buildTargetGroupMapper Error!");
            }
            
            var buildParams = new BundleBuildParameters(setting.BuildTarget, buildGroup, setting.OutputFolder);
            buildParams.BundleCompression = BuildCompression.LZ4;
            buildParams.UseCache = setting.UseCache;
            buildParams.AppendHash = false;
            
            var tasks = DefaultBuildTasks.Create(DefaultBuildTasks.Preset.AssetBundleBuiltInShaderExtraction);
            
            var buildContent = new BundleBuildContent(setting.AllAssetBundleBuild);
            
            ReturnCode exitCode = ContentPipeline.BuildAssetBundles(buildParams, buildContent, out results, tasks);
            if (exitCode != ReturnCode.Success)
            {
                Debug.LogError($"打包失败:{exitCode}");
                return false;
            }

            Debug.Log("构建bundle完成");
            return true;
        }
        
        protected abstract BundleBuildSetting CreateBuildParameters(BuildTarget buildTarget, string outputFolder);

        protected abstract void CollectManifest(IBundleBuildResults results, string outputPath);
    }
}