using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using GoogleMobileAds.Api; // 광고관련

public class PopupButton : MonoBehaviour 
{
	private RewardBasedVideoAd rewardBasedVideo;

	public void Start()
	{		
		string appId = "ca-app-pub-6199509731492528~2799736587"; // AvoidEagle 앱 애드몹 ID

        // Initialize the Google Mobile Ads SDK.
		MobileAds.Initialize(appId);

        // Get singleton reward based video ad reference.
		this.rewardBasedVideo = RewardBasedVideoAd.Instance;

		// Called when the user should be rewarded for watching a video.
        rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        // Called when the ad is closed.
        rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;       

		this.RequestRewardBasedVideo(); // 광고 Load
	}

    // 광고 Load
	private void RequestRewardBasedVideo()
	{
    	//string adUnitId = "ca-app-pub-3940256099942544/5224354917"; // 보상형 광고 테스트 ID
    	string adUnitId = "ca-app-pub-6199509731492528/2372554692"; // 내 보상형 광고 실제 ID

    	// Create an empty ad request.
    	AdRequest request = new AdRequest.Builder().Build();
        // Load the rewarded video ad with the request.
    	this.rewardBasedVideo.LoadAd(request, adUnitId);
    }    

    // 광고 보상으로 isAdRetry 값 true 부여
    public void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
    	GameDirector.isAdRetry = true;  
    	rewardBasedVideo.OnAdRewarded -= HandleRewardBasedVideoRewarded;  	
    }

    // 광고 닫으면 씬 재시작
    public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
    {
        SceneManager.LoadScene("GameScene");
        rewardBasedVideo.OnAdClosed -= HandleRewardBasedVideoClosed; 
    }	

    public void AdRetry()
    {		
    	if (rewardBasedVideo.IsLoaded()) // 광고가 로드되어 있다면 광고 재생
    	{
    		rewardBasedVideo.Show();
    	}
    }

    public void Retry()
    {
    	GameDirector.isAdRetry = false;
    	SceneManager.LoadScene("GameScene");	
    }	    
}
