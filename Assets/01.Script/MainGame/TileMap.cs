using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour	// : MonoBehaviour -> 상속, 이게 있어야 게임에 포함될 수 있음
{
	// public GameObject _tileObjectPrefabs; // 일반적인 방법!
	[SerializeField] GameObject _tileObjectPrefabs;	// 프라이빗, 에디터에서만 setting 가능, inspector view에 출력됨

	// Unity Methods

	// Awake() : 한번만 실행됨
	void Start ()		// 활성화 될 때 마다 한번 실행
	{
		// Create(); // => GameScene에서 통제
	}
	
	void Update ()	// 매번 실행
	{
		// Debug.Log("HelloWorld");
	}

	MapTile[,] _maptileArray;	// c# 2차원 배열

	public void Create()
	{
		// 맵 타일 출력 test
		Sprite[] spriteList = Resources.LoadAll<Sprite>("roguelikeSheet_transparent");

		/*
		for (int i = 0; i < spriteList.Length; i++)
		{
			GameObject tileObject = GameObject.Instantiate(_tileObjectPrefabs); // 게임 오브젝트 1개 생성
			tileObject.transform.SetParent(transform);  // 부모에 따라서 정해짐
			tileObject.transform.localScale = Vector3.one;  // 1x1x1 초기화
			tileObject.transform.localPosition = Vector3.zero;  // 0x0x0 초기화
																// transform : 위치나 크기 관련
																// wordlposition, localposition : 월드 위치 표시, 지역내 위치 표시(특정 지역어디에 있는가) => 쉬운 계산을 위해서
			float x = -8 + (i % 16);
			float y = 8 - (i / 16);
			tileObject.GetComponent<SpriteRenderer>().sprite = spriteList[i];   // sprite 출력
			tileObject.transform.localPosition = new Vector2(x, y);
		}
		// 모든 맵 리스트 출력
		*/

		int width = 32;
		int height = 32;
		// 맵 생성 변수

		_maptileArray = new MapTile[height, width];

		// 1층 - 바닥
		for(int y=0;y<height;y++)
		{
			for(int x=0;x<width;x++)
			{
				GameObject tileObject = GameObject.Instantiate(_tileObjectPrefabs); // 게임 오브젝트 1개 생성
				tileObject.transform.SetParent(transform);  // 부모에 따라서 정해짐
				tileObject.transform.localScale = Vector3.one;  // 1x1x1 초기화
				tileObject.transform.localPosition = Vector3.zero;  // 0x0x0 초기화
																	// transform : 위치나 크기 관련
																	// wordlposition, localposition : 월드 위치 표시, 지역내 위치 표시(특정 지역어디에 있는가) => 쉬운 계산을 위해서
				int spriteIndex = 5;   // int spriteIndex = x + (y * height);
				if (spriteIndex<spriteList.Length)
				{
					/*
					tileObject.GetComponent<SpriteRenderer>().sprite = spriteList[spriteIndex];   // sprite 출력
					tileObject.transform.localPosition = new Vector2(x, y);
					*/

					MapTile mapTile = tileObject.GetComponent<MapTile>();
					mapTile.Init(spriteList[spriteIndex], x-8, y-8);    // 출력위치 조정용 -8
					_maptileArray[y, x] = mapTile;
					// 캡슐화
				}
			}	// for문 - x
		}   // for문 - y

		// 2층 - 아이템
		for (int y = 0; y < height; y++)
		{
			for (int x = 0; x < width; x++)
			{
				int creatValue = Random.Range(0, 100);
				if (creatValue < 30)    // 간단한 PCG -> 수치화된건 PCG 가능, 길 막힐 가능성 있음
				{
					GameObject tileObject = GameObject.Instantiate(_tileObjectPrefabs); // 게임 오브젝트 1개 생성
					tileObject.transform.SetParent(transform);  // 부모에 따라서 정해짐
					tileObject.transform.localScale = Vector3.one;  // 1x1x1 초기화
					tileObject.transform.localPosition = Vector3.zero;  // 0x0x0 초기화

					int spriteIndex = 291;
					if (spriteIndex < spriteList.Length)
					{
						MapTile mapTile = tileObject.GetComponent<MapTile>();
						mapTile.Init(spriteList[spriteIndex], x - 8, y - 8);
						// 캡슐화
					}
				}

			}   // for문 - x
		}   // for문 - y

	}   // Cearte()

	MapTile GetMapTile(int tileX, int tileY)
	{
		return _maptileArray[tileY, tileX];	// c# 2차원 배열
	}

	public void SetCharcter(int tileX, int tileY, Player character)
	{
		MapTile mapTile = GetMapTile(tileX, tileY);
		character.transform.position = mapTile.transform.position;
	}	// SetCharcter(int tileX, int tileY, Player player)

}	// TileMap Class 
