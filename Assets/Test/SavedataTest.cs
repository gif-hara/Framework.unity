using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using UnityEditor;
using HK.Framework;
using HK.Framework.Systems;

namespace HK.Framework
{
	/// <summary>
	/// .
	/// </summary>
	public class SavedataTest : MonoBehaviour
	{
		public class Player
		{
			public string name;

			public int strength;
		}

		[MenuItem("HK/Savedata Save")]
		private static void Save()
		{
			var player = new Player();
			player.name = "P";
			player.strength = 10;
			SaveData.SetClass<Player>("p0", player);
			SaveData.Save();
		}

		[MenuItem("HK/Savedata Load")]
		private static void Load()
		{
			var player = SaveData.GetClass<Player>("p0", null);
			Debug.Log("player name = " + player.name);
			Debug.Log("player strength = " + player.strength);
		}
	}
}