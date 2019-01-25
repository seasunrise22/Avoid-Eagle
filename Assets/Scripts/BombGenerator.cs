using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombGenerator : MonoBehaviour 
{
	public GameObject bombPrefab; // 폭탄 프리팹 꽂을 구멍
	public GameObject cloneBomb; // 폭탄 클론 담을 변수

	GameObject player;
	GameObject mainCamera;

	float delta; // 시간을 담기 위한 변수
	float span; // 시간 간격
	int genBombPosY; // 폭탄 생성시킬 y축 위치

	ArrayList cloneBombArray; // 폭탄 클론 담을 배열

	void Awake() 
	{
		player = GameObject.Find("Player");
		mainCamera = GameObject.Find("Main Camera");

		delta = 0;
		span = 1;

		cloneBombArray = new ArrayList();	
	}
	
	void Update () 
	{
		delta += Time.deltaTime; // 프레임에 종속되지 않도록 하기 위해

		if((delta > span) && !player.GetComponent<PlayerController>().isDead) // 설정된 간격마다 폭탄 아이템을 생성할지 판단
		{
			delta = 0; // 시간변수 초기화		

			int rand = Random.Range(0, 100); // 폭탄 아이템이 나올 확률 
			if(rand < 20) // 설정된 확률로 폭탄 아이템 생성
			{
				genBombPosY = Random.Range(0, 10); // 폭탄이 생성될 높이. (이상, 미만)사이의 숫자 중 하나 랜덤
				cloneBomb = Instantiate(bombPrefab) as GameObject; // 폭탄 클론 생성					
				cloneBomb.tag = "BombItem";
				cloneBombArray.Add(cloneBomb); // 생성된 클론을 배열에 담음
				cloneBomb.transform.position = new Vector3(4, (mainCamera.transform.position.y + genBombPosY), 0); // 화면 오른쪽에서 생성
			}			
		}				
	}

	// 화면상의 모든 폭탄 클론들 삭제시키는 함수
	public void DestroyBombs()
	{
		// 이미 만들어진 폭탄 클론들 삭제
		foreach(GameObject cloneBomb in cloneBombArray)
		{
			if(cloneBomb != null)
			{		
				Destroy(cloneBomb.gameObject);
			}			
		}	
	}
}
