
/***********************************************************************************************************
 * Produced by App Advisory - http://app-advisory.com													   *
 * Facebook: https://facebook.com/appadvisory															   *
 * Contact us: https://appadvisory.zendesk.com/hc/en-us/requests/new									   *
 * App Advisory Unity Asset Store catalog: http://u3d.as/9cs											   *
 * Developed by Gilbert Anthony Barouch - https://www.linkedin.com/in/ganbarouch                           *
 ***********************************************************************************************************/




using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

#if APPADVISORY_ADS
using AppAdvisory.Ads;
#endif

namespace AppAdvisory.VSRATE
{
	public class RateUsManager : MonoBehaviour 
	{

		public CommonAppSettings commonAppSettings;

		public GameObject buttonRateUs;
		public GameObject buttonWriteUs;
		public GameObject panel;

		static RateUsManager self;

		void OnDestroy()
		{
			StarsManager.onNewratingEvent -= onNewratingEvent;
		}

		void Start()
		{
			self = this;

			panel.SetActive(false);

			StarsManager.onNewratingEvent -= onNewratingEvent;
			StarsManager.onNewratingEvent += onNewratingEvent;

			buttonRateUs.SetActive(false);
			buttonWriteUs.SetActive(false);
		}

		void onNewratingEvent(int num)
		{
			panel.SetActive(true);

			float rating = (float)(num + 1) / 2f;

			buttonRateUs.SetActive (rating >= 3.5);//rateUsSettings.numberOfStarsToAcceptReview);
			buttonWriteUs.SetActive(rating < 3.5);//rateUsSettings.numberOfStarsToAcceptReview);
		}

		public static void ShowRateUs(bool show)
		{
			print("ShowRateUs ; " + show);
			self.panel.SetActive(show);
		}

		public static void ShowRateUsWindows()
		{
			ShowRateUs(true);
		}

		public static void HideRateUsWindows()
		{
			ShowRateUs(false);
		}

		public static bool RateUsIsVisible()
		{
			return self.panel.activeInHierarchy;
		}

		public static void OpenRateUsURL()
		{
			#if !VS_UI
			Application.OpenURL(self.commonAppSettings.URL_STORE);
			#else
			Application.OpenURL(FindObjectOfType<AppAdvisory.VSUI.UIController>().URL_STORE);
			#endif
		}

		public static void SendEmail()
		{
			string url = "mailto:" + MyEscapeURL("support@sourceoven.com") + "?subject=" + MyEscapeURL("I don't like PIVOTA") + "&body=" + MyEscapeURL("Sorry to hear that you are not enjoying the game. Please tell us how we can improve.");

			print("url to open : " + url);

			Application.OpenURL(url);
		}

		static string MyEscapeURL (string url)
		{
			return WWW.EscapeURL(url).Replace("+","%20");
		}
	}
}