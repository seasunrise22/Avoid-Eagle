using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GoogleMobileAds.Api;

public class AdMobManager : MonoBehaviour 
{
	private BannerView bannerView;

	public void Start()
	{		 
		DontDestroyOnLoad(gameObject);
		MobileAds.Initialize("ca-app-pub-6199509731492528~2799736587"); // AvoidEagle 앱 ID   
		RequestBannerAd();
		ShowBannerAd();
	}

	public void RequestBannerAd()
	{		
		//string adUnitId = "ca-app-pub-3940256099942544/6300978111"; // 테스트 배너 광고 단위 ID
		string adUnitId = "ca-app-pub-6199509731492528/2894448816"; // 실제 내 배너 광고 단위 ID

		bannerView = new BannerView(adUnitId, AdSize.SmartBanner, AdPosition.Top);

		AdRequest request = new AdRequest.Builder().Build();

		bannerView.LoadAd(request);
	}

	public void ShowBannerAd()
	{
		bannerView.Show();

		//Debug.Log("광고 실행!");
		//Debug.Log(SystemInfo.deviceUniqueIdentifier); // 테스트용 내 디바이스 ID
	}
}
