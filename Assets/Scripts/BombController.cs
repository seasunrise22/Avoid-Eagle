using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour 
{	
	float translation;	

	void Awake()
	{
		
	}

	void Update() 
	{
		// 매 초당 등속으로 왼쪽 이동(프레임당 이동으로 잡아버리면 기기마다 속도가 달라지므로 Time.deltaTime 사용)
		this.translation = Time.deltaTime * -2; // 독수리랑 겹쳐서 못 먹는 사태 막기 위해 속도를 독수리와 조금 다르게 설정
		transform.Translate(translation, 0, 0); 

		// 화면 벗어나면 오브젝트 삭제
		if(transform.position.x < -4)
		{
			Destroy(gameObject);
		}
	}
}
