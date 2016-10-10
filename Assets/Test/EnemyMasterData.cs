using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace HK.Framework
{
	/// <summary>
	/// .
	/// </summary>
	public class EnemyMasterData : MonoBehaviour
	{
		[SerializeField]
		private TextAsset textAsset;

		[SerializeField]
		private StringAsset.Finder a, b, c, d, e, f, g, h, i, j, k, l, m, n;

		[ContextMenu("Test")]
		private void Test()
		{
			var database = CsvParser<Enemy>.Parse(this.textAsset, s => new Enemy(s));
			database.ForEach(d => Debug.LogFormat("name = {0} hp = {1} armor = {2}", d.name, d.hp, d.armor));
		}

		public class Enemy
		{
			public Enemy(List<string> data)
			{
				this.name = data[1];
				this.hp = int.Parse(data[2]);
				this.armor = int.Parse(data[3]);
			}

			public string name;

			public int hp;

			public int armor;
		}
	}
}