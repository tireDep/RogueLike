using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour {
	// player, map 상위 object

	[SerializeField] List<GameObject> _itemPrefabList;	// List 형태
	[SerializeField] GameObject _enemyPrefab;
	[SerializeField] TileMap _tileMap;
	[SerializeField] Player _player;
	// [SerializeField] Enemy _enemy;
	void Start ()
	{
		_tileMap.Create();
		GameManager.Instance.SetMap(_tileMap);

		_player.Init(); // Null error 해결!
		GameManager.Instance.SetPlayer(_player);
		// _enemy.Init();
		int enemyCnt = 10;
		for(int i=0;i<enemyCnt;i++)
		{
			// prefabs으로부터 게임 obj 생성(원본이 있고, 원본을 복사해서 생성)
			GameObject enemyObj = GameObject.Instantiate(_enemyPrefab);

			enemyObj.transform.SetParent(transform);
			enemyObj.transform.localPosition = Vector3.zero;
			enemyObj.transform.localScale=Vector3.one;
			// 기본적인 초기화(위치, 크기)

			// 게임 obj 내의 Character script init
			Character character = enemyObj.GetComponent<Character>();
			character.Init();
		}

		// 아이템을 맵에 배치함
		int itemCnt = 5;
		for (int i = 0; i<itemCnt;i++)
		{
			// 아이템 생성
			int randomIdx = Random.Range(0, _itemPrefabList.Count); // 랜덤으로 아이템 생성

			// prefabs으로부터 게임 obj 생성(원본이 있고, 원본을 복사해서 생성)
			GameObject itemObj = GameObject.Instantiate(_itemPrefabList[randomIdx]);

			itemObj.transform.SetParent(transform);
			itemObj.transform.localPosition = Vector3.zero;
			itemObj.transform.localScale = Vector3.one;
			// 기본적인 초기화(위치, 크기)

			// 게임 obj 내의 Character script init
			Item item= itemObj.GetComponent<Item>();
			item.Init();
			// 상향작업 필요

			// 아이템 초기화

		}
	}

	void Update ()
	{
	}
}
