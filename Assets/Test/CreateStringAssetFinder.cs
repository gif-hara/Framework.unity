using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using HK.Framework;

namespace HK.Framework.Test
{
	/// <summary>
	/// .
	/// </summary>
	public class CreateStringAssetFinder : MonoBehaviour
	{
		[SerializeField]
		private string defaultString;

		[SerializeField]
		private StringAsset stringAsset;

		[SerializeField]
		private StringAsset.Finder finder;

		void Start()
		{
			this.finder = this.stringAsset.CreateFinder(this.defaultString);
			Debug.Log(this.finder);
		}
	}
}