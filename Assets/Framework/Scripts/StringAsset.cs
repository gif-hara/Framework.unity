using UnityEngine;
using System.Collections.Generic;
using System;
using UnityEngine.Assertions;

namespace HK.Framework
{
	/// <summary>
	/// 文字列を管理するアセット.
	/// </summary>
	[System.Serializable][CreateAssetMenu()]
	public class StringAsset : ScriptableObject
	{
		[System.Serializable]
		public class Finder
		{
			public StringAsset target;

			public string value;

			public string guid;

#if !UNITY_EDITOR
	        private string cachedValue;

	        private bool isInitialize = false;
#endif
			public override string ToString()
			{
#if UNITY_EDITOR
				// 毎回取得することでゲーム中でも値を変更出来るように.
				return this.target.Get(this);
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

			public string Get
			{
				get
				{
					return this.ToString();
				}
			}

			public string Format(params object[] args)
			{
				return this.target.Format(this, args);
			}
		}

		[System.Serializable]
		public class Data
		{
			public Data()
			{
				this.value = new Value();
				this.guid = System.Guid.NewGuid().ToString();
			}

			public Value value;

			public string guid;
		}

		[System.Serializable]
		public class Value
		{
			public string ja = "";

			public string en = "";

			public string Default
			{
				get
				{
					return this.ja;
				}
			}

			public void Set(string value, string culture)
			{
				switch(culture)
				{
				case "ja":
					this.ja = value;
				break;
				case "en":
					this.en = value;
				break;
				default:
					Debug.AssertFormat(false, "不正な値です. culture = {0}", culture);
				break;
				}
			}

			public string Get(string culture)
			{
				switch(culture)
				{
				case "ja":
					return this.ja;
				case "en":
					return this.en;
				default:
					Debug.AssertFormat(false, "不正な値です. culture = {0}", culture);
					return "";
				}
			}
		}

		public List<Data> database = new List<Data>();

#if !UNITY_EDITOR
		/// <summary>
		/// 検索用のディクショナリ.
		/// </summary>
		private Dictionary<string, Value> findDictionary = null;
#endif

		/// <summary>
		/// 要素を取得する.
		/// </summary>
		/// <param name="finder"></param>
		/// <returns></returns>
		public string Get(Finder finder)
		{
#if UNITY_EDITOR
			var data = this.database.Find(d => d.guid == finder.guid);
			if(data == null)
			{
				Debug.LogError("\"" + finder.value + "\"に対応する値がありませんでした.");
				return "";
			}

			// ここでローカライズ.
			return data.value.ja;
#else
	        if( this.findDictionary == null )
	        {
	            this.findDictionary = new Dictionary<string, Value>();
	            for( int i = 0, imax = this.database.Count; i < imax; i++ )
	            {
	                this.findDictionary.Add( this.database[i].guid, this.database[i].value );
	            }
	        }
			
			Value result;
	        if( !this.findDictionary.TryGetValue( finder.guid, out result ) )
			{
				// 要素がない場合はエラー.
				Debug.LogError( "\"" + finder.value + "\"に対応する文字列がありませんでした." );
	            result.ja = finder.value;
			}
			
	        // ここでローカライズ処理を入れる.
			return result.ja;
#endif
		}

		/// <summary>
		/// string.Formatのラッピング.
		/// </summary>
		/// <param name="finder"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		public string Format(Finder finder, params object[] args)
		{
			return string.Format(Get(finder), args);
		}

		public void Add()
		{
			this.database.Add(new Data());
		}
	}
}
