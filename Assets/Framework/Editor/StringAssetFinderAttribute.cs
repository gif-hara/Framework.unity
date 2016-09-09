#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using UnityEditor;

namespace HK.Framework
{
	[CustomPropertyDrawer(typeof(StringAsset.Finder))]
	public class StringAssetFinderDrawer : PropertyDrawer
	{
	    public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
	    {
	        position = EditorGUI.PrefixLabel( position, GUIUtility.GetControlID( FocusType.Passive ), label );

	        var indentLevel = EditorGUI.indentLevel;
	        EditorGUI.indentLevel = 0;

	        var targetProperty = property.FindPropertyRelative("target");

	        var targetRect = new Rect( position.x, position.y, position.width / 2, position.height );
	        EditorGUI.PropertyField( targetRect, targetProperty, GUIContent.none );

	        var stringAsset = targetProperty.objectReferenceValue as StringAsset;
	        var keyRect = new Rect( targetRect.x + targetRect.width, position.y, position.width / 2, position.height );
	        var finderValue = property.FindPropertyRelative( "value" );
	        var finderGuid = property.FindPropertyRelative( "guid" );
	        EditorGUI.BeginChangeCheck();
	        var selectedIndex = EditorGUI.Popup( keyRect, GetCurrentSelectKeyIndex( stringAsset, finderGuid.stringValue ), GetKeyAndDescriptionList( stringAsset ) );
	        if( EditorGUI.EndChangeCheck() && stringAsset != null )
	        {
	            finderValue.stringValue = stringAsset.database[selectedIndex].value.Default;
	            finderGuid.stringValue = stringAsset.database[selectedIndex].guid;
	        }

	        EditorGUI.indentLevel = indentLevel;
	    }

	    private string[] GetKeyAndDescriptionList( StringAsset stringAsset )
	    {
	        if( stringAsset == null )
	        {
	            return new string[0];
	        }

	        string[] list = new string[stringAsset.database.Count];
	        for( var i = 0; i < list.Length; i++ )
	        {
	            list[i] = stringAsset.database[i].value.Default;
	        }

	        return list;
	    }

	    private int GetCurrentSelectKeyIndex(StringAsset stringAsset, string finderGuid)
	    {
	        if(stringAsset == null)
	        {
	            return 0;
	        }
	        return stringAsset.database.FindIndex( d => d.guid.CompareTo( finderGuid ) == 0 );
	    }
	}
}
#endif
