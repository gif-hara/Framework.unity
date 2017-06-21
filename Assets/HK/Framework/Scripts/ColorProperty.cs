using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

namespace HK.Framework
{
	/// <summary>
	/// float型のデータを格納するクラス.
	/// </summary>
	[CreateAssetMenu()]
	public class ColorProperty : ScriptableObject
	{
	    [System.Serializable]
	    public class Finder
	    {
	        public ColorProperty target;

	        public string key;

	        public string guid;

#if !UNITY_EDITOR
	        private Color cachedValue;

	        private bool isInitialize = false;
#endif

	        public Color Get
	        {
	            get
	            {
#if UNITY_EDITOR
	                // 毎回取得することでゲーム中でも値を変更出来るように.
	                return this.target.Get( this );
#else
	                // 最適化のためキャッシュさせる.
	                if(!this.isInitialize)
	                {
	                    this.cachedValue = this.target.Get( this );
	                    this.isInitialize = true;
	                }

	                return this.cachedValue;
#endif
	            }
	        }
	    }

	    [System.Serializable]
	    public class Data
	    {
	        public string key;

	        public Color value;

	        public string guid;
	    }

	    [SerializeField]
	    private List<Data> database = new List<Data>();

#if !UNITY_EDITOR
	    private Dictionary<string, Color> dictionary = null;
#endif

	    public Color Get( Finder finder )
	    {
#if UNITY_EDITOR
	        var data = this.database.Find( d => d.guid == finder.guid );
	        if(data == null)
	        {
	            Debug.LogError( "\"" + finder.key + "\"に対応する値がありませんでした." );
	            return new Color();
	        }

	        return data.value;
#else
	        if(this.dictionary == null)
	        {
	            this.dictionary = new Dictionary<string, Color>();
	            this.database.ForEach( d => this.dictionary.Add( d.guid, d.value ) );
	        }

	        Color value = new Color();
	        if(!this.dictionary.TryGetValue(finder.guid, out value))
	        {
	            Debug.LogError( "\"" + finder.key + "\"に対応する値がありませんでした." );
	        }

	        return value;
#endif
	    }

#if UNITY_EDITOR
	    public List<Data> Database { get { return this.database; } }
#endif

	}
}
