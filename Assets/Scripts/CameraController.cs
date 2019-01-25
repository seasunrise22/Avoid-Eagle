using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour 
{
	GameObject player;

	// 배경 오브젝트들
	GameObject background;
	GameObject mountain01;
	GameObject mountain02;
	GameObject bottomCloud02;
	GameObject bottomCloud03;

	Vector3 playerPos; // 플레이어의 위치값

	void Start () 
	{
		player = GameObject.Find("Player");

		// 배경 오브젝트들	
		background = GameObject.Find("BackgroundImg");
		mountain01 = GameObject.Find("Mountain01");
		mountain02 = GameObject.Find("Mountain02");
		bottomCloud02 = GameObject.Find("BottomCloud02");
		bottomCloud03 = GameObject.Find("BottomCloud03");
	}
	
	void Update () 
	{
		if(!player.GetComponent<PlayerController>().isDead)
		{
			playerPos = player.transform.position; // 플레이어 포지션 값(기준)

			transform.position = new Vector3((playerPos.x+1.5f), playerPos.y, transform.position.z); // 카메라 위치 캐릭터를 중심으로 재조정
			background.transform.position = new Vector3(0, (playerPos.y-9.0f), 0); // 배경 이미지 재조정
			mountain01.transform.position = new Vector3(1, (playerPos.y-3.5f), 0); // 뒤쪽 산 이미지 재조정
			mountain02.transform.position = new Vector3(-0.5f, (playerPos.y-4.5f), 0); // 앞쪽 산 이미지 재조정
			bottomCloud02.transform.position = new Vector3(-1.6f, (playerPos.y-3.3f), 0); // 뒤쪽 구름 이미지 재조정
			bottomCloud03.transform.position = new Vector3(1.2f, (playerPos.y-3.8f), 0); // 앞쪽 구름 이미지 재조정
		}	
	}
}
