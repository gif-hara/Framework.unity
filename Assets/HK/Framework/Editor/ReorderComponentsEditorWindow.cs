using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using UnityEditor.SceneManagement;
using UnityEngine.Assertions;
using System.Collections.Generic;

namespace HK.Framework
{
	/// <summary>
	/// コンポーネントのMove UpとMove Downを容易に行うEditorWindow.
	/// </summary>
	public class ReorderComponentsEditorWindow : EditorWindow
	{
		private ReorderableList componentList;

		private GameObject selectionObject;

		private Component selectionComponent;

		private int selectionIndex;

		[MenuItem("Window/ReorderComponents")]
		public static void ShowWindow()
		{
			EditorWindow.GetWindow<ReorderComponentsEditorWindow>();
		}

		void OnGUI()
		{
			if(this.selectionObject == null)
			{
				EditorGUILayout.LabelField("Please select a GameObject.");
			}
			else
			{
				EditorGUILayout.ObjectField(this.selectionObject, typeof(GameObject), true);
			}
			if(this.componentList != null)
			{
				this.componentList.DoLayoutList();
			}
		}

		void OnInspectorUpdate()
		{
			if(this.selectionObject != Selection.activeGameObject)
			{
				this.selectionObject = Selection.activeGameObject;
				this.CreateComponentList();
				this.Repaint();
			}
		}

		private void CreateComponentList()
		{
			if(this.selectionObject == null)
			{
				this.componentList = null;
				return;
			}
				
			var components = new List<Component>(this.selectionObject.GetComponents<Component>());
			components.RemoveAll(this.IsRemove);
			this.componentList = new ReorderableList(components, typeof(Component));
			this.componentList.drawElementCallback = ( Rect rect, int index, bool selected, bool focused ) =>
			{
				var property = this.componentList.list[index] as Component;
				EditorGUI.LabelField(rect, new GUIContent( property.GetType().ToString() ) );
			};
			this.componentList.drawHeaderCallback = ( Rect rect ) =>
			{
				EditorGUI.LabelField(rect, "Components");
			};
			this.componentList.drawFooterCallback = (Rect rect) =>
			{
			};
			this.componentList.onReorderCallback = (ReorderableList list) => 
			{
				float sign = Mathf.Sign(list.index - this.selectionIndex);
				int difference = Mathf.Abs(list.index - this.selectionIndex);
				for(int i=0; i<difference; i++)
				{
					if(sign < 0)
					{
						UnityEditorInternal.ComponentUtility.MoveComponentUp(this.selectionComponent);
					}
					else
					{
						UnityEditorInternal.ComponentUtility.MoveComponentDown(this.selectionComponent);
					}
				}
				if(difference != 0)
				{
					EditorUtility.SetDirty(this.selectionObject);
					EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
				}
			};
			this.componentList.onSelectCallback = (ReorderableList list) => 
			{
				this.selectionIndex = list.index;
				this.selectionComponent = list.list[list.index] as Component;
			};
		}

		private bool IsRemove(Component target)
		{
			return target.GetType() == typeof(UnityEngine.Transform);
		}
	}
}
