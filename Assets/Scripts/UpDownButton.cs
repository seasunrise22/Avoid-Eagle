using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDownButton : MonoBehaviour 
{
	GameObject player; 
	GameObject pauseButton;

	float startPosY; // 출발지점 y축
	float stopPosY; // 멈추는 지점 y축
	Vector3 playerPos;
	bool isMove; // 움직이고 있는가?

	void Start () 
	{
		player = GameObject.Find("Player");	
		pauseButton = GameObject.Find("PauseButton");
		isMove = false;
	}
	
	void Update () 
	{
		playerPos = player.transform.position;
		stopPosY = player.transform.position.y;	

		// 이동거리 제한
		if(isMove)
		{
			if(Mathf.Abs(stopPosY - startPosY) > 0.5f) // Mathf.Abs: 절댓값 반환
			{
				player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0); // 정지	
				isMove = false;
			}
		}						
	}

	public void UpPress()
	{
		// 일시정지 상태가 아니고 플레이어가 죽은게 아니라면
		if(!pauseButton.GetComponent<PauseButton>().isPause && !player.GetComponent<PlayerController>().isDead)
		{			
			player.GetComponent<Animator>().SetTrigger("MoveTrigger"); // 애니메이션 재생	
			player.GetComponent<AudioSource>().Play(); // 소리 재생
			startPosY = playerPos.y; // 출발지점 y축 저장
			player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10); // y축 방향으로 속도 증가		
			isMove = true;	
		}			
	}

	public void DownPress()
	{
		// 일시정지 상태가 아니고 플레이어가 죽은게 아니라면
		if(!pauseButton.GetComponent<PauseButton>().isPause && !player.GetComponent<PlayerController>().isDead)
		{			
			player.GetComponent<Animator>().SetTrigger("MoveTrigger"); // 애니메이션 재생	
			player.GetComponent<AudioSource>().Play(); // 소리 재생
			startPosY = playerPos.y; // 출발지점 y축 저장
			player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -10); // y축 방향으로 속도 감소	
			isMove = true;		
		}			
	}
}
