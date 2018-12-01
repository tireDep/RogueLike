using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{

	protected bool _canMove = false;

	public enum eType
	{
		ENEMY,
		ITEM,
		PLAYER	// , NPC etc..
	};  // mapObject type

	// Charater에서 이동, 공통적인 부분 -> 상향!
	protected TileMap _map = null;    // 멤버변수
	protected int _tileX, _tileY; // 타일 좌표

	public virtual void Init()  // GameScene에서 실행됨, Null error 해결
	{
		// 캐릭터 맵상에 random 배치
		_map = GameManager.Instance.GetMap();
		_canMove = false;

		int x = 0;
		int y = 0;

		while (true)
		{
			x = Random.Range(0, 32);
			y = Random.Range(0, 32);

			if (true == _map.CanMove(x, y))
				break;
		}

		_tileX = x;
		_tileY = y;
		// 타일기반게임 이동 -> 타일간 이동(x, y좌표 이동 x), 해당위치의 타일
		_map.SetMapObject(_tileX, _tileY, this);
	}


	void Start()
	{
	}

	void Update()
	{
	}

	public bool CanMove()
	{
		return _canMove;
	}

	protected eType _type = eType.ENEMY;	// Enemy 

	public eType GetObjectType()
	{
		return _type;	// 맵 obj의 타입 ret
	}

	public virtual void Attacked(MapObject attackObject)	// 가상함수화!
	{
		Debug.Log("Attack from : " + attackObject); // 충돌 시 공격 들어가는지 확인!
		// obj마다 공격방식을 다르게 함 -> 가상함수화
	}

	public virtual int GetAtkPoint()
	{
		return 10;	//	임시
	}
}
