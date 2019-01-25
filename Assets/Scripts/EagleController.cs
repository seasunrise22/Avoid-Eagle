using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleController : MonoBehaviour 
{	
	float translation; // 독수리 이동 속도	

	void Update () 
	{
		// 매 초당 등속으로 왼쪽 이동(프레임당 이동으로 잡아버리면 기기마다 속도가 달라지므로 Time.deltaTime 사용)
		this.translation = Time.deltaTime * -3; // 프레임 간격만큼 곱해주면 프레임에 종속되지 않는 속도 구현 가능
		transform.Translate(translation, 0, 0); // 독수리 등속 이동

		// 화면 벗어나면 오브젝트 삭제
		if(transform.position.x < -4)
		{
			Destroy(gameObject);
		}
	}
}
