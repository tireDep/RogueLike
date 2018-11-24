using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour {
	// player, map 상위 object

	[SerializeField] GameObject _enemyPrefab;
	[SerializeField] TileMap _tileMap;
	[SerializeField] Player _player;
	// [SerializeField] Enemy _enemy;
	void Start ()
	{
		_tileMap.Create();
		GameManager.Instance.SetMap(_tileMap);

		_player.Init(); // Null error 해결!
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
			// 과제
			// 아이템 생성
			// 아이템 초기화
		}
	}

	void Update ()
	{
	}
}
