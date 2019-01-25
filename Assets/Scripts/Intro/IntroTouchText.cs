using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroTouchText : MonoBehaviour 
{
	void Awake() 
	{

	}
	
	void Start() 
	{
		gameObject.transform.localScale = new Vector3(0, 0, 0);
		StartCoroutine(TouchTextEffect());	
	}

	IEnumerator TouchTextEffect()
	{
		while(true)
		{
			gameObject.transform.localScale = new Vector3(1, 1, 1);
			yield return new WaitForSeconds(0.4f);
			gameObject.transform.localScale = new Vector3(0, 0, 0);
			yield return new WaitForSeconds(0.4f);
		}
	}
}
