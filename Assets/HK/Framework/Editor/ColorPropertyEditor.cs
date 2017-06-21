using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;
using System;

namespace HK.Framework
{
	[CustomEditor( typeof( ColorProperty ) )]
	public class ColorPropertyEditor : EditorScriptableObject<ColorProperty>
	{
	    private ReorderableList reorderableList;

	    void OnEnable()
	    {
	        reorderableList = new ReorderableList( serializedObject, serializedObject.FindProperty( "database" ) );
	        reorderableList.elementHeight = EditorGUIUtility.singleLineHeight * 3;
	        reorderableList.drawHeaderCallback = ( Rect rect ) =>
	            {
	                //EditorGUI.LabelField(rect, "Property Field");
	            };
	        reorderableList.drawElementCallback += ( Rect rect, int index, bool selected, bool focused ) =>
	        {
	            var property = reorderableList.serializedProperty.GetArrayElementAtIndex( index );

	            EditorGUI.PrefixLabel( rect, GUIUtility.GetControlID( FocusType.Passive ), new GUIContent( "Element " + index ) );
	            var indentLevel = EditorGUI.indentLevel;
	            EditorGUI.indentLevel += 1;

	            var keyRect = new Rect( rect.x, rect.y + EditorGUIUtility.singleLineHeight, rect.width, EditorGUIUtility.singleLineHeight );
	            var valueRect = new Rect( rect.x, rect.y + EditorGUIUtility.singleLineHeight * 2, rect.width, EditorGUIUtility.singleLineHeight );
	            EditorGUI.PropertyField( keyRect, property.FindPropertyRelative( "key" ), GUIContent.none );
	            EditorGUI.PropertyField( valueRect, property.FindPropertyRelative( "value" ), new GUIContent( "Value", "プロパティの値" ) );

	            EditorGUI.indentLevel = indentLevel;
	        };
	        reorderableList.onAddCallback = ( ReorderableList list ) =>
	            {
	                list.serializedProperty.InsertArrayElementAtIndex( list.serializedProperty.arraySize );
	                var property = reorderableList.serializedProperty.GetArrayElementAtIndex( list.serializedProperty.arraySize - 1 );
	                property.FindPropertyRelative( "key" ).stringValue = "";
	                property.FindPropertyRelative( "value" ).colorValue = new Color();
	                property.FindPropertyRelative( "guid" ).stringValue = System.Guid.NewGuid().ToString();

	            };
	    }
		
		public override void OnInspectorGUI ()
		{
	        reorderableList.DoLayoutList();
	        serializedObject.ApplyModifiedProperties();
		}
	}
}

