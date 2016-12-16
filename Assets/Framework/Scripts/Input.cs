using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;

namespace HK.Framework
{
	/// <summary>
	/// 入力制御.
	/// </summary>
	public static class Input
	{
		/// <summary>
        /// 画面をタッチしているか返す.
        /// </summary>
		/// <remarks>
		/// コンシューマまたはWebPlayerかUnityの時はマウスの左クリックされているか.
		/// モバイルの場合はタップされているかで判断しています.
		/// </remarks>
        /// <returns>画面をタッチしているか返す.</returns>
		public static bool IsTouch
		{
			get
			{
				if(Application.isConsolePlatform || Application.isWebPlayer || Application.isEditor)
				{
					return UnityEngine.Input.GetMouseButton(0);
				}
				else if(Application.isMobilePlatform)
				{
					return UnityEngine.Input.touchCount > 0;
				}
				else
				{
					Assert.IsTrue(false, "未対応のプラットフォームです. platform = " + Application.platform.ToString());
					return false;
				}
			}
		}
	}
}