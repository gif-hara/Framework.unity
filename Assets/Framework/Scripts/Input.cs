using UnityEngine;
using UnityEngine.Assertions;
using System.Collections.Generic;
using System;

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
				if(isConsumerPlatform)
				{
					return UnityEngine.Input.GetMouseButton(0);
				}
				else if(isMobilePlatform)
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

		/// <summary>
        /// 画面をタッチダウンしたか返す.
        /// </summary>
		/// <remarks>
		/// コンシューマまたはWebPlayerかUnityの時はマウスの一度だけ左クリックされたか.
		/// モバイルの場合は一度だけタップされたかで判断しています.
		/// </remarks>
        /// <returns>画面をタッチダウンしたか返す.</returns>
		public static bool IsTouchDown
		{
			get
			{
				if(isConsumerPlatform)
				{
					return UnityEngine.Input.GetMouseButtonDown(0);
				}
				else if(isMobilePlatform)
				{
					return Array.FindIndex(UnityEngine.Input.touches, t => t.phase == TouchPhase.Began) != -1;
				}
				else
				{
					Assert.IsTrue(false, "未対応のプラットフォームです. platform = " + Application.platform.ToString());
					return false;
				}
			}
		}

		/// <summary>
        /// 画面をタッチアップしたか返す.
        /// </summary>
		/// <remarks>
		/// コンシューマまたはWebPlayerかUnityの時はマウスの左クリックを終了したか.
		/// モバイルの場合はタップが終了したかで判断しています.
		/// </remarks>
        /// <returns>画面をタッチアップしたか返す.</returns>
		public static bool IsTouchUp
		{
			get
			{
				if(isConsumerPlatform)
				{
					return UnityEngine.Input.GetMouseButtonUp(0);
				}
				else if(isMobilePlatform)
				{
					return Array.FindIndex(UnityEngine.Input.touches, t => t.phase == TouchPhase.Ended) != -1;
				}
				else
				{
					Assert.IsTrue(false, "未対応のプラットフォームです. platform = " + Application.platform.ToString());
					return false;
				}
			}
		}

		/// <summary>
        /// コンシューマ環境で動作しているか返す.
        /// </summary>
		/// <remarks>
		/// コンソールかWebPlayerかUnityで動作している環境のことを指します.
		/// </remarks>
        /// <returns>コンシューマ環境で動作しているか返す.</returns>
		private static bool isConsumerPlatform
		{
			get
			{
				return Application.isConsolePlatform || Application.isWebPlayer || Application.isEditor;
			}
		}

		/// <summary>
        /// モバイル環境で動作しているか返す.
        /// </summary>
        /// <returns>モバイル環境で動作しているか返す.</returns>
		private static bool isMobilePlatform
		{
			get
			{
				return Application.isMobilePlatform;
			}
		}
	}
}
