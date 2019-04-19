# Avoid-Eagle
- 개발인원 : 1명
- 역할
  - 전체
  
## Introduction
무작위로 날아오는 독수리들을 피해 하늘 높이 올라가야 하는 간단한 피하기 게임입니다. 

사용하면 화면상의 모든 독수리가 사라지는 폭탄이라는 특수 아이템을 구현하여 운적인 요소를 최소화 했습니다. 

올라간 높이로 순위가 매겨져 다른 유저와 경쟁할 수 있습니다.

## Development Environment
- IDE : Unity 2017.4.6f1
- Language : C#

## Code Preview
***플레이어를 y축 방향으로 움직이게 했을 때, 원하는 위치에 멈추도록 만들기 위한 로직***
```C#
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
```

## Screenshots
![noname01](https://user-images.githubusercontent.com/45503931/56436872-581a6300-6318-11e9-93fe-dc5faa74b1ff.png)

![noname02](https://user-images.githubusercontent.com/45503931/56436873-581a6300-6318-11e9-9cf7-38b635601f86.png)

![resize_noname03](https://user-images.githubusercontent.com/45503931/56436868-5781cc80-6318-11e9-9f4a-8b4b65c6c3ac.png)

![resize_noname04](https://user-images.githubusercontent.com/45503931/56436870-581a6300-6318-11e9-89f8-1d0591f501f2.png)
