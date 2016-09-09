using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;

namespace HK.Framework
{
	[CustomEditor( typeof( StringAsset ) )]
	public class StringAssetEditor : EditorScriptableObject<StringAsset>
	{
	    private ReorderableList reorderableList;

	    private string culture;

	    void OnEnable()
	    {
	        this.culture = "ja";
	        reorderableList = new ReorderableList( serializedObject, serializedObject.FindProperty( "database" ) );
	        reorderableList.elementHeight = EditorGUIUtility.singleLineHeight;
	        reorderableList.drawHeaderCallback = ( Rect rect ) =>
	        {
	            rect = EditorGUI.PrefixLabel( rect, new GUIContent( "Current Culture = " + culture ) );
	            this.CultureButton( rect, 0, "ja" );
	            this.CultureButton( rect, 1, "en" );
	        };
	        reorderableList.drawElementCallback = ( Rect rect, int index, bool selected, bool focused ) =>
	        {
	            var property = reorderableList.serializedProperty.GetArrayElementAtIndex( index );

	            var indentLevel = EditorGUI.indentLevel;
	            EditorGUI.indentLevel += 1;

	            var valueRect = new Rect( rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight );
	            var valueProperty = property.FindPropertyRelative( "value" );

	            if(culture == "ja")
	            {
	                EditorGUI.PropertyField( valueRect, valueProperty.FindPropertyRelative( culture ), GUIContent.none );
	            }
	            else
	            {
	                EditorGUI.PropertyField( valueRect, valueProperty.FindPropertyRelative( culture ), new GUIContent("ja = " + valueProperty.FindPropertyRelative("ja").stringValue) );
	            }

	            EditorGUI.indentLevel = indentLevel;
	        };
	        reorderableList.onAddCallback = ( ReorderableList list ) =>
	        {
	            list.serializedProperty.InsertArrayElementAtIndex( list.serializedProperty.arraySize );
	            var property = reorderableList.serializedProperty.GetArrayElementAtIndex( list.serializedProperty.arraySize - 1 );
	            var valueProperty = property.FindPropertyRelative( "value" );
	            valueProperty.FindPropertyRelative( "ja" ).stringValue = "";
	            valueProperty.FindPropertyRelative( "en" ).stringValue = "";
	            property.FindPropertyRelative( "guid" ).stringValue = System.Guid.NewGuid().ToString();
	        };

	    }
		
		public override void OnInspectorGUI ()
		{
	        reorderableList.DoLayoutList();
	        serializedObject.ApplyModifiedProperties();
		}

	    private void CultureButton(Rect origin, int index, string cultureIdentity)
	    {
	        const float width = 30;
	        var rect = new Rect(origin.x + index * width, origin.y, width, origin.height);
	        if(GUI.Button(rect, cultureIdentity))
	        {
	            this.culture = cultureIdentity;
	        }
	    }
	}
}
