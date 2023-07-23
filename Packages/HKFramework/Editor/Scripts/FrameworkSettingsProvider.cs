using System.Collections.Generic;
using HK.Framework.AudioSystems;
using HK.Framework.BootSystems;
using HK.Framework.UISystems;
using UnityEditor;
using UnityEditor.Animations;
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
                    CreateSetupData();
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
            if (serializedObject.hasModifiedProperties)
            {
                serializedObject.ApplyModifiedProperties();
            }
            
            if (GUILayout.Button("Recreate SetupData"))
            {
                CreateSetupData();
            }
        }

        private static void CreateSetupData()
        {
            var path = "Assets/HKFramework/Resources/SetupData.asset";
            var setupData = AssetDatabase.LoadAssetAtPath<SetupData>(path);
            if (setupData == null)
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
            }

            setupData.SetUIManagerPrefabEditor(CreateDefaultUIManager());
            setupData.SetAnimatorControllerEditor(CreateDefaultAnimationData());
            setupData.SetAudioManagerPrefabEditor(CreateDefaultAudioManager());
            EditorUtility.SetDirty(setupData);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static UIManager CreateDefaultUIManager()
        {
            const string path = "Assets/HKFramework/UIManager.prefab";

            // すでにプロジェクトにUIManagerがある場合は作成しない
            var result = AssetDatabase.LoadAssetAtPath<UIManager>(path);
            if (result != null)
            {
                return result;
            }
            
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

            result = PrefabUtility.SaveAsPrefabAsset(uiManager.gameObject, "Assets/HKFramework/UIManager.prefab").GetComponent<UIManager>();

            // 作成したゲームオブジェクトを削除する
            Object.DestroyImmediate(uiManager.gameObject);

            return result;
        }

        private static AnimatorController CreateDefaultAnimationData()
        {
            const string path = "Assets/HKFramework/Runtime.controller";
            
            // すでにプロジェクトにAnimatorControllerがある場合は作成しない
            var result = AssetDatabase.LoadAssetAtPath<AnimatorController>(path);
            if (result != null)
            {
                return result;
            }
            
            // BaseとClipAとClipBの3つのAnimationClipを作成する
            var baseClip = new AnimationClip();
            AssetDatabase.CreateAsset(baseClip, "Assets/HKFramework/Base.anim");
            var clipA = new AnimationClip();
            AssetDatabase.CreateAsset(clipA, "Assets/HKFramework/ClipA.anim");
            var clipB = new AnimationClip();
            AssetDatabase.CreateAsset(clipB, "Assets/HKFramework/ClipB.anim");
            
            // Runtimeという名前のAnimatorControllerを作成する
            result = new AnimatorController();
            AssetDatabase.CreateAsset(result, path);
            // Base LayerとOverride Layer AとOverride Layer Bいう名前のレイヤーを作成する
            result.AddLayer("Base Layer");
            result.AddLayer("Override Layer A");
            result.AddLayer("Override Layer B");
            
            // Base LayerにBaseという名前のステートを作成する
            var baseState = result.layers[0].stateMachine.AddState("Base");
            // BaseステートにbaseClipを設定する
            result.SetStateEffectiveMotion(baseState, baseClip);
            
            // Override Layer AにOverride State Aという名前のステートを作成する
            var clipAState = result.layers[1].stateMachine.AddState("Override State A");
            // Override State AステートにclipAを設定する
            result.SetStateEffectiveMotion(clipAState, clipA);
            
            // Override Layer BにOverride State Bという名前のステートを作成する
            var clipBState = result.layers[2].stateMachine.AddState("Override State B");
            // Override State BステートにclipBを設定する
            result.SetStateEffectiveMotion(clipBState, clipB);
            
            return result;
        }

        private static AudioManager CreateDefaultAudioManager()
        {
            const string path = "Assets/HKFramework/AudioManager.prefab";
            
            // すでにプロジェクトにAudioManagerがある場合は作成しない
            var result = AssetDatabase.LoadAssetAtPath<AudioManager>(path);
            if (result != null)
            {
                return result;
            }
            
            // ゲームオブジェクトを新規作成
            var audioManager = new GameObject(
                "AudioManager",
                typeof(AudioManager)
                )
                .GetComponent<AudioManager>();
            var bgmSource = new GameObject(
                "BGMSource",
                typeof(AudioSource)
                )
                .GetComponent<AudioSource>();
            bgmSource.transform.SetParent(audioManager.transform);
            audioManager.SetBGMSource(bgmSource);
            audioManager.SetSoundEffectElementPrefab(CreateDefaultSoundEffectElement());
            
            result = PrefabUtility.SaveAsPrefabAsset(audioManager.gameObject, path).GetComponent<AudioManager>();
            // audioManager.gameObjectを削除する
            Object.DestroyImmediate(audioManager.gameObject);

            return result;
        }
        
        private static SoundEffectElement CreateDefaultSoundEffectElement()
        {
            const string path = "Assets/HKFramework/SoundEffectElement.prefab";
            
            // すでにプロジェクトにSoundEffectElementがある場合は作成しない
            var result = AssetDatabase.LoadAssetAtPath<SoundEffectElement>(path);
            if (result != null)
            {
                return result;
            }
            
            // ゲームオブジェクトを新規作成
            var soundEffectElement = new GameObject(
                "SoundEffectElement",
                typeof(SoundEffectElement)
                )
                .GetComponent<SoundEffectElement>();
            var audioSource = new GameObject(
                "AudioSource",
                typeof(AudioSource)
                )
                .GetComponent<AudioSource>();
            audioSource.transform.SetParent(soundEffectElement.transform);
            soundEffectElement.SetAudioSource(audioSource);
            
            result = PrefabUtility.SaveAsPrefabAsset(soundEffectElement.gameObject, path).GetComponent<SoundEffectElement>();
            // soundEffectElement.gameObjectを削除する
            Object.DestroyImmediate(soundEffectElement.gameObject);

            return result;
        }
    }
}
