using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; //UI 클릭시 터치 이벤트 발생 방지.

public class PlayerController : MonoBehaviour 
{
	GameObject EagleGenerator;
	GameObject gameOverPopup;
	GameObject BombButton;
	GameObject BombGenerator;
	//GameObject pauseButton;

	//Animator animator;
	//Rigidbody2D rigid2D;

	//float startPosY; // 출발지점 y축
	//float stopPosY; // 멈추는 지점 y축	
	public bool isDead; // 플레이어 생존여부
	bool coroutineRunning; // 독수리 두 번 충돌판정되지 않게 하기 위해서

	void Start()
	{
		EagleGenerator = GameObject.Find("EagleGenerator");
		//pauseButton = GameObject.Find("PauseButton");
		//animator = GetComponent<Animator>();
		//rigid2D = GetComponent<Rigidbody2D>();
		gameOverPopup = GameObject.Find("GameOverPopup");
		BombButton = GameObject.Find("BombButton");
		BombGenerator = GameObject.Find("BombGenerator");
		isDead = false;
		coroutineRunning = false;	
	}

	void Update () 
	{
		/*
		stopPosY = transform.position.y;
		
		// 플레이어 이동
		if((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && !isDead)
		{					
			// 터치한 부분이 UI가 아니고(raycast target에 체크된 부분이 아니라면) 일시정지 상태가 아니라면
			if(!EventSystem.current.IsPointerOverGameObject() && !pauseButton.GetComponent<PauseButton>().isPause)
			{
				animator.SetTrigger("MoveTrigger"); // 애니메이션 재생	
				startPosY = transform.position.y; // 출발지점 y축 저장
				rigid2D.velocity = new Vector2(0, 2); // y축 방향으로 속도 증가
			}			
		}
		
		// 이동거리 제한
		if(stopPosY - startPosY > 1)
		{
			rigid2D.velocity = new Vector2(0, 0); // 정지	
		}
		*/
	}

	// 독수리 및 폭탄과 충돌판정
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.tag == "BombItem" && other.gameObject != null) // 부딪힌 대상이 폭탄 아이템이라면
		{
			BombGenerator.GetComponent<AudioSource>().Play(); // 아이템 획득 소리 재생			
			BombButton.GetComponent<Image>().fillAmount += 0.5f; // 아이템 먹으면 일정 비율 게이지가 채워짐
			Destroy(other.gameObject);
		}
		else if(!coroutineRunning && other.gameObject != null) // 부딪힌게 독수리라면(+충돌판정을 한 번만 일으키기 위해)
		{
			isDead = true; // 플레이어가 죽었음을 나타냄
			GameDirector.saveCurPosY = Mathf.Round(transform.position.y); // 광고보고 이어하기용 죽었을 당시 이동거리 저장(반올림)
			gameOverPopup.GetComponent<AudioSource>().Play(); // 게임오버 소리 재생
			gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic; // 죽으면 캐릭터 추락함

			coroutineRunning = true;
			StartCoroutine(DestroyTimer(other)); // 일정 시간 후 독수리 없애는 코루틴 호출			
			EagleGenerator.GetComponent<EagleGenerator>().DestroyEagles(); // 다른 독수리들 모두 삭제하는 함수 호출		
			BombGenerator.GetComponent<BombGenerator>().DestroyBombs(); // 필드상의 모든 폭탄 제거	
		}		
	}

	// 일정시간 후 독수리 오브젝트를 없애는 코루틴 함수
	IEnumerator DestroyTimer(Collider2D other)
	{				
		other.GetComponent<Animator>().SetTrigger("EagleDeadTrigger"); // 독수리 죽는 애니메이션 재생
		GetComponent<Animator>().SetTrigger("DeadTrigger"); // 독수리 죽는 애니메이션 재생			
		yield return new WaitForSeconds(0.1f);
		Destroy(other.gameObject);
	}
}
