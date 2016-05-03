#if UNITY_EDITOR
using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using UnityEditor;

namespace HK.Framwork
{
	/// <summary>
	/// .
	/// </summary>
	[CustomPropertyDrawer(typeof(ColorProperty.Finder))]
	public class ColorPropertyFinderDrawer : PropertyDrawer
	{
	    public override void OnGUI( Rect position, SerializedProperty property, GUIContent label )
	    {
	        position = EditorGUI.PrefixLabel( position, GUIUtility.GetControlID( FocusType.Passive ), label );

	        var indentLevel = EditorGUI.indentLevel;
	        EditorGUI.indentLevel = 0;

	        var targetProperty = property.FindPropertyRelative( "target" );

	        // targetの描画.
	        var targetRect = new Rect( position.x, position.y, position.width / 2, position.height );
	        EditorGUI.PropertyField( targetRect, targetProperty, GUIContent.none );

	        // keyのポップアップ描画.
	        var floatProperty = targetProperty.objectReferenceValue as ColorProperty;
	        var keyRect = new Rect( targetRect.x + targetRect.width, targetRect.y, position.width / 2, position.height );
	        var finderKey = property.FindPropertyRelative( "key" );
	        var finderGuid = property.FindPropertyRelative( "guid" );
	        EditorGUI.BeginChangeCheck();
	        var selectedIndex = EditorGUI.Popup( keyRect, GetCurrentSelectKeyIndex( floatProperty, finderGuid.stringValue ), GetKeyAndDescriptionList(floatProperty) );
	        if( EditorGUI.EndChangeCheck() )
	        {
	            finderKey.stringValue = floatProperty.Database[selectedIndex].key;
	            finderGuid.stringValue = floatProperty.Database[selectedIndex].guid;
	        }

	        EditorGUI.indentLevel = indentLevel;
	    }

	    private string[] GetKeyList( ColorProperty floatProperty )
	    {
	        if( floatProperty == null )
	        {
	            return new string[0];
	        }

	        var database = floatProperty.Database;
	        string[] list = new string[database.Count];
	        for( var i = 0; i < list.Length; i++ )
	        {
	            list[i] = database[i].key;
	        }

	        return list;
	    }
	    private string[] GetKeyAndDescriptionList( ColorProperty floatProperty )
	    {
	        if( floatProperty == null )
	        {
	            return new string[0];
	        }

	        var database = floatProperty.Database;
	        string[] list = new string[database.Count];
	        for( var i = 0; i < list.Length; i++ )
	        {
	            list[i] = database[i].key + " [Value = " + database[i].value + "f]";
	        }

	        return list;
	    }

	    private int GetCurrentSelectKeyIndex(ColorProperty property, string finderGuid)
	    {
	        if(property == null)
	        {
	            return 0;
	        }
	        return property.Database.FindIndex( d => d.guid.CompareTo( finderGuid ) == 0 );
	    }
	}
}
#endif
