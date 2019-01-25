using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;

public class RankingButton : MonoBehaviour 
{
	string leaderBoardId = "CgkInNqqyIgEEAIQAQ"; 

	public void OnRankingButton()
	{
		//구글 플레이게임 서비스를 초기화합니다.
		//Activate the Google Play gaems platform
		PlayGamesPlatform.Activate();

		//구글 서비스에 로그인합니다.
		Social.localUser.Authenticate(AuthenticateHandler);
	}

	void AuthenticateHandler(bool isSuccess)
	{
		if (isSuccess)
		{
			float highScore = PlayerPrefs.GetFloat("highscore", 0);
			// 리더보드에 점수를 게시
            Social.ReportScore((long)highScore, leaderBoardId, (bool success) =>
            	{
            		if (success)
            		{
            			// 리더보드 UI 보여줌
            			PlayGamesPlatform.Instance.ShowLeaderboardUI(leaderBoardId);
            		}
            		else
            		{
            		// upload highscore failed
            		}
            	});
        }
        else
        {
        // login failed
        }
    }
}

	
