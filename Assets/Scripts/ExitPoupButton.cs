using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitPoupButton : MonoBehaviour
 {
 	GameObject exitPopup; // 게임종료 팝업

	void Start () 
	{
		this.exitPopup = GameObject.Find("ExitPopup");	
	}

	void Update () 
	{
		
	}

	public void Quit()
	{
		Application.Quit();
	}

	public void Resume()
	{
		Time.timeScale = 1;	
		this.exitPopup.transform.localPosition = new Vector3(0, -2000, 0);
	}
}
