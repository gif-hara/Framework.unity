using System.Collections.Generic;
using HK.Framework.BootSystems;
using HK.Framework.UISystems;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

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
                    setupData = ScriptableObject.CreateInstance<SetupData>();
                    // フォルダが無い場合は作成する
                    if (!AssetDatabase.IsValidFolder("Assets/HKFramework"))
                    {
                        AssetDatabase.CreateFolder("Assets", "HKFramework");
                    }
                    if (!AssetDatabase.IsValidFolder("Assets/HKFramework/Resources"))
                    {
                        AssetDatabase.CreateFolder("Assets/HKFramework", "Resources");
                    }
                    AssetDatabase.CreateAsset(setupData, "Assets/HKFramework/Resources/SetupData.asset");

                    setupData.SetUIManagerPrefabEditor(CreateDefaultUIManager());
                    EditorUtility.SetDirty(setupData);
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

        private static UIManager CreateDefaultUIManager()
        {
            // ゲームオブジェクトを新規作成
            var uiManager = new GameObject(
                "UIManager",
                typeof(UIManager)
                )
                .GetComponent<UIManager>();

            // カメラを作成して設定する
            var cameraObject = new GameObject(
                "UICamera",
                typeof(Camera)
                )
                .GetComponent<Camera>();
            var cameraTransform = cameraObject.transform;
            cameraTransform.SetParent(uiManager.transform);
            cameraTransform.localPosition = Vector3.zero;
            cameraObject.clearFlags = CameraClearFlags.Depth;
            cameraObject.orthographic = true;
            cameraObject.orthographicSize = 100.0f;
            cameraObject.nearClipPlane = 0.0f;
            cameraObject.farClipPlane = 20.0f;

            // Canvasを作成して設定する
            var canvasObject = new GameObject(
                "Canvas",
                typeof(RectTransform)
                );
            var canvasRectTransform = canvasObject.GetComponent<RectTransform>();
            canvasRectTransform.SetParent(uiManager.transform);
            canvasRectTransform.localScale = Vector3.one;
            canvasRectTransform.localPosition = Vector3.zero;
            canvasRectTransform.anchorMin = Vector2.zero;
            canvasRectTransform.anchorMax = Vector2.one;

            var canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceCamera;
            canvas.worldCamera = cameraObject;
            canvas.planeDistance = 10.0f;

            var canvasScaler = canvasObject.AddComponent<CanvasScaler>();
            canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            canvasScaler.referenceResolution = new Vector2(1920, 1080);
            canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
            canvasScaler.matchWidthOrHeight = 0.0f;

            canvasObject.AddComponent<GraphicRaycaster>();

            // EventSystemを作成して設定する
            var eventSystemObject = new GameObject(
                "EventSystem",
                typeof(EventSystem),
                typeof(InputSystemUIInputModule)
                );
            eventSystemObject.transform.SetParent(uiManager.transform);
            
            uiManager.SetUICameraEditor(cameraObject);
            uiManager.SetUIParentEditor(canvasRectTransform);

            var result = PrefabUtility.SaveAsPrefabAsset(uiManager.gameObject, "Assets/HKFramework/UIManager.prefab").GetComponent<UIManager>();

            // 作成したゲームオブジェクトを削除する
            Object.DestroyImmediate(uiManager.gameObject);

            return result;
        }
    }
}
