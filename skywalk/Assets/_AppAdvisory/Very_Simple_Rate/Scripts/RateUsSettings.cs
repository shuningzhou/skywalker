
/***********************************************************************************************************
 * Produced by App Advisory - http://app-advisory.com
 * Facebook: https://facebook.com/appadvisory
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch
 ***********************************************************************************************************/




using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace AppAdvisory.VSRATE
{
	public class RateUsSettings : ScriptableObject 
	{
		[Range(1f, 5f)]
		public float numberOfStarsToAcceptReview = 3.5f;
		public string email = "me@example.com";
		public string subject = "My Subject";
		public string body = "My Body\r\nFull of non-escaped chars";



		#region EDITOR

		public bool isRateUsFoldoutOpened = false;

		public static readonly string PATH = "Assets/_AppAdvisory/Very_Simple_Rate/";
		public static readonly string NAME = "RateUsSettings";

		private static string PathToAsset 
		{
			get 
			{
				return PATH + NAME + ".asset";
			}
		}

		#if UNITY_EDITOR

		[MenuItem("Assets/Create/RateUsSettings")]
		public static void CreateRateUsSettings()
		{
			RateUsSettings asset = ScriptableObject.CreateInstance<RateUsSettings>();

			AssetDatabase.CreateAsset(asset, PathToAsset);
			AssetDatabase.SaveAssets();

			EditorUtility.FocusProjectWindow();

			Selection.activeObject = asset;
		}

		#endif

		#endregion
	}

}