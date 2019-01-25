using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroDirector : MonoBehaviour 
{
	void Awake()
	{
		Screen.SetResolution(720, 1280, true); // 해상도 설정(고정?)
	}

	void Start () 
	{
		gameObject.GetComponent<AudioSource>().Play();		
	}
	
	// 터치하면 게임씬으로 이동
	void Update () 
	{
		if(Input.GetMouseButtonDown(0))
		{
			gameObject.GetComponent<AudioSource>().Stop();	
			SceneManager.LoadScene("GameScene");
		}		
	}
}
