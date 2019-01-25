using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombButton : MonoBehaviour 
{
	GameObject EagleGenerator;
	GameObject Player;
	GameObject UsableImg; // 폭탄 사용가능 이미지
	GameObject CoolTimeImg; // 폭탄 쿨타임 이미지
	GameObject BombReadySound; // 폭탄 사용가능 사운드를 내기 위한 빈 오브젝트
	GameObject BombEmptyText;

	bool isUseBomb; // 폭탄을 썼는가
	float delta; // 시간을 담을 그릇
	float span; // 폭탄 게이지가 채워지는 간격

	void Awake() 
	{
		EagleGenerator = GameObject.Find("EagleGenerator");
		Player = GameObject.Find("Player");
		UsableImg = GameObject.Find("UsableImg");
		CoolTimeImg = GameObject.Find("CoolTimeImg");
		BombReadySound = GameObject.Find("BombReadySound");
		BombEmptyText = GameObject.Find("BombEmptyText");
		isUseBomb = false;
		delta = 0;
		span = 1; // 초

		UsableImg.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f); // 사용가능하다는 아이콘 표시
		//CoolTimeImg.transform.localScale = new Vector3(0, 0, 0); // 쿨타임 중이라는 표시 지움
		BombEmptyText.transform.localScale = new Vector3(0, 0, 0); // 쿨타임 중이라는 표시
	}
	
	void Update() 
	{
		//Debug.Log(gameObject.GetComponent<Image>().fillAmount);

		if(isUseBomb) // 폭탄을 쓴 상태라면 쿨타임을 돌린다
		{
			UsableImg.transform.localScale = new Vector3(0, 0, 0); // 사용가능하다는 아이콘 지움
			//CoolTimeImg.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f); // 쿨타임 중이라는 표시
			BombEmptyText.transform.localScale = new Vector3(1.2f, 1.2f, 1f); // 폭탄 없음 텍스트 표시

			/*
			delta += Time.deltaTime; // 프레임에 종속되지 않고 시간을 계산하기 하기 위해
			
			if((delta > span) && !Player.GetComponent<PlayerController>().isDead) // 간격마다 폭탄 이미지 채워짐
			{
				gameObject.GetComponent<Image>().fillAmount += 0.066f; // 여기서 쿨타임 조정함. 현재 대략 15초.
				delta = 0; // 시간변수 초기화				

				if(gameObject.GetComponent<Image>().fillAmount >= 1) // 쿨이 다 찼다면
				{
					BombReadySound.GetComponent<AudioSource>().Play(); // 폭탄 쿨 다 찼다는 소리 재생
					UsableImg.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f); // 사용가능하다는 아이콘 표시
					CoolTimeImg.transform.localScale = new Vector3(0, 0, 0); // 쿨타임 중이라는 표시 지움
					isUseBomb = false;
				}
			}	
			*/

			if(gameObject.GetComponent<Image>().fillAmount != 0 && gameObject.GetComponent<Image>().fillAmount < 1) // 폭탄 게이지가 조금이라도 차있다면
			{
				BombEmptyText.transform.localScale = new Vector3(0, 0, 0); // 폭탄 없음 텍스트 지움
			}

			if(gameObject.GetComponent<Image>().fillAmount >= 1) // 쿨이 다 찼다면
			{
				BombReadySound.GetComponent<AudioSource>().Play(); // 폭탄 쿨 다 찼다는 소리 재생
				UsableImg.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f); // 사용가능하다는 아이콘 표시
				//CoolTimeImg.transform.localScale = new Vector3(0, 0, 0); // 쿨타임 중이라는 표시 지움
				BombEmptyText.transform.localScale = new Vector3(0, 0, 0); // 폭탄 없음 텍스트 지움
				isUseBomb = false;
			}
		}	
	}

	public void UseBomb()
	{		
		if(gameObject.GetComponent<Image>().fillAmount >= 1)
		{
			gameObject.GetComponent<AudioSource>().Play();
			gameObject.GetComponent<Image>().fillAmount = 0;
			EagleGenerator.GetComponent<EagleGenerator>().DestroyEagles();
			isUseBomb = true;
		}		
	}
}
