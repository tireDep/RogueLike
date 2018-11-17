using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MapObject
{

	// Unity 
	[SerializeField] CharacterModel _model;
	int _tileX, _tileY; // 타일 좌표
	TileMap _map = null;    // 멤버변수

	public void Init()  // GameScene에서 실행됨, Null error 해결
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


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void SetPosition(int newX, int newY)
	{
		_map.ResetMapObject(_tileX, _tileY);	// 직전 위치 삭제(초기화)
		_tileX = newX;
		_tileY = newY;
		_map.SetMapObject(_tileX, _tileY, this);
	}

	// 방향에 맞는 animation 실행, 실질적으로 tilemap에서 움직임
	protected void MoveLeft()
	{
		_model.CharLeftWalk();  // 방향 animation, _model : script => root에 존재함(작업의 편리함)

		int newX = _tileX - 1;
		if (true == _map.CanMove(newX, _tileY)) // 플레이어가 해당위치로 갈 수 있는지 판별
			SetPosition(newX, _tileY);

	}   // MoveLeft()

	protected void MoveRight()
	{
		_model.CharRightWalk();

		int newX = _tileX + 1;
		if (true == _map.CanMove(newX, _tileY))
			SetPosition(newX, _tileY);
	}   // MoveRight()

	protected void MoveUp()
	{
		_model.CharUpWalk();

		int newY = _tileY + 1;
		if (true == _map.CanMove(_tileX, newY))
			SetPosition(_tileX, newY);
	}   // MoveUp()

	protected void MoveDown()
	{
		_model.CharDownWalk();

		int newY = _tileY - 1;
		if (true == _map.CanMove(_tileX, newY))
			SetPosition(_tileX, newY);
	}   // MoveDown()

}
