using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour 
{
	GameObject pauseText;
	GameObject gameDirector;
	public bool isPause; // 일시정지 상태인가 아닌가

	void Start() 
	{
		this.pauseText = GameObject.Find("PauseText");
		this.gameDirector = GameObject.Find("GameDirector");
		this.isPause = false;		
	}

	// 일시정지 버튼 메서드
	public void OnPause()
	{
		// 일시정지 상태가 아닐경우
		if(!this.isPause)
		{
			this.isPause = true;
			Time.timeScale = 0;		
			this.gameDirector.GetComponent<AudioSource>().Pause(); // 배경음악 일시정지
			// 일시정지 텍스트 활성화
			this.pauseText.transform.localPosition = new Vector3(0, 0, 0);
			GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/UI/Play") as Sprite; // 일시정지 버튼 이미지 변경(Assets/Resources 하위 폴더 탐색)                       
		}
		else if(this.isPause) // 일시정지 상태일경우
		{
			this.isPause = false;
			Time.timeScale = 1;	
			this.gameDirector.GetComponent<AudioSource>().Play(); // 배경음악 다시 재생
			// 일시정지 텍스트 숨김
			this.pauseText.transform.localPosition = new Vector3(0, 1000, 0);
			GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/UI/Pause") as Sprite; // 일시정지 버튼 이미지 변경(Assets/Resources 하위 폴더 탐색)    
		}		
	}
}
