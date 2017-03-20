
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

namespace AppAdvisory.VSRATE
{
	public class StarsManager : MonoBehaviour 
	{
		public delegate void OnNewratingEventHandler(int index);
		public static event OnNewratingEventHandler onNewratingEvent;

		public GameObject[] stars;

		void Awake()
		{
			for(int i = 0; i < stars.Length; i++)
			{
				SetColorAlpha(stars[i], 0f);
			}
		}

		public void OnClicked(int num)
		{
			for(int i = 0; i <= num; i++)
			{
				SetColorAlpha(stars[i], 1f);

			}

			for(int i = num + 1; i < stars.Length; i++)
			{
				SetColorAlpha(stars[i], 0f);
			}

			if(onNewratingEvent != null)
				onNewratingEvent(num);
		}

		void SetColorAlpha(GameObject obj, float alpha)
		{
			Color c = obj.GetComponent<Image>().color;
			c.a = alpha;
			obj.GetComponent<Image>().color = c;
		}

	}
}