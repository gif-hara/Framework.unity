using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace HK.Framework.Editor
{
    public class FrameworkSettingsProvider : SettingsProvider
    {
        public FrameworkSettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null)
            : base(path, scopes, keywords)
        {
        }

        [SettingsProvider]
        public static SettingsProvider Create()
        {
            var provider = new FrameworkSettingsProvider("Project/HKFramework", SettingsScope.Project)
            {
                label = "HKFramework",
                keywords = new HashSet<string>(new[] { "HKFramework" })
            };
            return provider;
        }

        public override void OnGUI(string searchContext)
        {
            var setupData = Resources.Load<SetupData>("SetupData");
            if (setupData == null)
            {
                if (GUILayout.Button("Create SetupData"))
                {
                    // セットアップデータのScriptableObjectを生成してResourcesに配置する
                    var setupDataInstance = ScriptableObject.CreateInstance<SetupData>();
                    // フォルダが無い場合は作成する
                    if (!AssetDatabase.IsValidFolder("Assets/HKFramework"))
                    {
                        AssetDatabase.CreateFolder("Assets", "HKFramework");
                    }
                    if (!AssetDatabase.IsValidFolder("Assets/HKFramework/Resources"))
                    {
                        AssetDatabase.CreateFolder("Assets/HKFramework", "Resources");
                    }
                    AssetDatabase.CreateAsset(setupDataInstance, "Assets/HKFramework/Resources/SetupData.asset");

                    // PlayerSettingsのPreloadedAssetsにセットアップデータを追加する
                    var preloadAssets = new List<Object>(PlayerSettings.GetPreloadedAssets());
                    preloadAssets.Add(setupDataInstance);
                    PlayerSettings.SetPreloadedAssets(preloadAssets.ToArray());

                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
                return;
            }

            // セットアップデータの中身をGUIで表示する
            var serializedObject = new SerializedObject(setupData);
            var iterator = serializedObject.GetIterator();
            while (iterator.NextVisible(true))
            {
                if(iterator.name == "m_Script")
                {
                    continue;
                }
                EditorGUILayout.PropertyField(iterator, true);
            }
        }
    }
}
