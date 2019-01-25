using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleGenerator : MonoBehaviour 
{
	public GameObject eaglePrefab; // 독수리 프리팹 꽂을 구멍
	public GameObject cloneEagle; // 독수리 클론 담을 변수

	GameObject mainCamera;
	GameObject player;
	
	float delta; // 시간을 담기 위한 변수
	float span; // 독수리 생성 시간 간격
	int genPosY; // 독수리 생성시킬 y축 위치

	ArrayList cloneArray; // 독수리 클론 담을 배열

	void Start () 
	{
		mainCamera = GameObject.Find("Main Camera"); // 메인 카메라 오브젝트 Find
		player = GameObject.Find("Player");

		delta = 0;
		span = 0.3f;

		cloneArray = new ArrayList();	
	}
	
	void Update () 
	{
		delta += Time.deltaTime; // 프레임에 종속되지 않도록 하기 위해

		// 1초마다 한 번 씩 실행
		if((delta > span) && !player.GetComponent<PlayerController>().isDead)
		{
			delta = 0; // 시간변수 초기화
			cloneEagle = Instantiate(eaglePrefab) as GameObject; // 독수리 클론 생성
			cloneArray.Add(cloneEagle); // 생성된 클론을 배열에 담음
			genPosY  = Random.Range(0, 10); // 독수리가 생성될 높이. (이상, 미만)사이의 숫자 중 하나 랜덤	

			cloneEagle.transform.position = new Vector3(4, (mainCamera.transform.position.y + genPosY), 0); // 화면 오른쪽에서 생성
		}		
	}

	// 화면상의 모든 독수리 클론들 삭제시키는 함수
	public void DestroyEagles()
	{
		// 이미 만들어진 독수리 클론들 삭제
		foreach(GameObject cloneEagle in cloneArray)
		{
			if(cloneEagle != null)
			{
				cloneEagle.GetComponent<Animator>().SetTrigger("EagleDeadTrigger"); // 독수리 죽는 애니메이션 재생				
				StartCoroutine(DestroyEaglesTimer(cloneEagle));
			}			
		}	
	}

	// 일정시간 후 독수리 오브젝트를 없애는 코루틴 함수
	IEnumerator DestroyEaglesTimer(GameObject clone)
	{		
		yield return new WaitForSeconds(0.1f);
		Destroy(clone.gameObject);
	}
}
