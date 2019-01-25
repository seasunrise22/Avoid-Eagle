using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameDirector : MonoBehaviour 
{
	GameObject player;
	GameObject distanceText; // 거리 나타내는 Text UI
	GameObject gameOverPopup; // 게임오버 팝업 UI
	GameObject newRecordText; // 최고기록 Text UI
	GameObject exitPopup; // 게임종료 팝업	
	GameObject StartText;
	GameObject EagleGenerator;
	GameObject BombGenerator;

	float distance; // 플레이어 이동 거리	
	static float maxDistance; // 최고기록 저장용 변수
	public static float saveCurPosY; // 광고보고 이어하기용 y축 저장 변수
	public static bool isAdRetry;
	bool isStart; // 유저가 최초에 터치를 했는가? 해야 게임이 본격적으로 시작

	IEnumerator StartTextCoroutine;

	void Awake()
	{
		GetComponent<AudioSource>().Play(); // 배경음악 재생
		Screen.SetResolution(720, 1280, true); // 해상도 고정(적용)하는 코드라는데... 이게 먹힐런지...

		player = GameObject.Find("Player");
		distanceText = GameObject.Find("DistanceText");	
		gameOverPopup = GameObject.Find("GameOverPopup");
		newRecordText = GameObject.Find("NewRecordText");
		exitPopup = GameObject.Find("ExitPopup");	
		StartText = GameObject.Find("StartText");	
		EagleGenerator = GameObject.Find("EagleGenerator");	
		BombGenerator = GameObject.Find("BombGenerator");	

		gameOverPopup.transform.localPosition = new Vector3(0, 2000, 0); 
		exitPopup.transform.localPosition = new Vector3(0, -2000, 0); 

		maxDistance = PlayerPrefs.GetFloat("highscore", 0);	// 게임꺼도 최고점은 저장되도록(값이 없으면 0)	
		isStart = false;

		StartTextCoroutine = ReadyTextAnimator(0.01f); // 코루틴 생성
	}

	void Start () 
	{
		// 게임 시작 전 Touch를 해야 게임이 시작되도록 구현
		EagleGenerator.SetActive(false);
		BombGenerator.SetActive(false);
		StartCoroutine(StartTextCoroutine); // Touch! 텍스트 움직이는 코루틴 재생

		/* 광고보고 이어하기 인가 아닌가 판별 구간 */
		if(isAdRetry) // 광고보고 다시하기 상태라면
		{
			player.transform.position = new Vector3(-1.5f, saveCurPosY, 0); // 이전에 저장된 위치에 플레이어를 둔다.	

		}
		else if(!isAdRetry) // 광고보고 다시하기 상태가 아니라면
		{
			player.transform.position = new Vector3(-1.5f, 0, 0); // 초기 위치에 플레이어를 둔다.			
		}		
		/* 광고보고 이어하기 인가 아닌가 판별 구간(끝) */			
	}	
	
	void Update () 
	{			
		// 최초 유저가 터치를 했는가 안 했는가
		if(!isStart) // 유저가 터치하지 않는 상태에서
		{	
			if(Input.GetMouseButton(0))	// 터치를 했다면
			{
				StopCoroutine(StartTextCoroutine); // 코루틴 중지
				StartText.transform.localScale = new Vector3(0, 0, 0); // Touch 텍스트 숨김
				EagleGenerator.SetActive(true);
				BombGenerator.SetActive(true);
				isStart = true;
			}	
		}
		
		distance =  Mathf.Round(player.GetComponent<Rigidbody2D>().position.y); // 플레이어의 y축 좌표(이동거리). 반올림.

		if(distance < 0)
		{
			distance = 0;
		}		

		distanceText.GetComponent<Text>().text = distance.ToString("F0") + "km"; // 이전 이동거리를 더한 이동거리 표시							

		// 플레이어가 죽으면
		if(player.GetComponent<PlayerController>().isDead)
		{					
			GetComponent<AudioSource>().Stop(); // 배경음악 종료
			gameOverPopup.transform.localPosition = new Vector3(0, 0, 0); // 게임오버 팝업UI 호출				

			// 최고 기록 갱신
			if(maxDistance < distance)
			{
				maxDistance = distance;	
				PlayerPrefs.SetFloat("highscore", maxDistance); // 게임꺼도 최고점은 저장되도록			
			}

			newRecordText.GetComponent<Text>().text = "최고 기록 : " + maxDistance.ToString("F0") + "km"; // 최고기록 표현(이전기록이 있다면)					
		}

		// 뒤로가기 버튼 눌렀을 경우(게임종료 팝업 호출)
		if(Input.GetKeyDown(KeyCode.Escape)) 
		{
			Time.timeScale = 0;	
			exitPopup.transform.localPosition = new Vector3(0, 0, 0);
		}
	}		

	// 텍스트 커졌다 작아졌다 하는 코루틴
	IEnumerator ReadyTextAnimator(float waitTime)
	{
		StartText.transform.localScale = new Vector3(1, 1, 1); // Touch! 텍스트 표출
		WaitForSeconds waitSec = new WaitForSeconds(waitTime);

		int i = 150; // Best fit Max Size
		bool minus = true;

		// 글자가 커졌다 작아졌다 하는 애니메이션 효과 주기 위한 반복문
		while(true)
		{
			yield return waitSec;
			StartText.GetComponent<Text>().resizeTextMaxSize = i; 

			if(minus) 
			{
				i -= 1;
				if(i == 120) minus = false;
			}
			else if(!minus)
			{
				i += 1;
				if(i == 150) minus = true;	
			}
		}
	}
}
