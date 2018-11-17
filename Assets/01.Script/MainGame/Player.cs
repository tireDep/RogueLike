﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// Unity 
	[SerializeField] CharacterModel _model;
	int _tileX, _tileY; // 타일 좌표
	TileMap _map = null;	// 멤버변수

	public void Init()	// GameScene에서 실행됨, Null error 해결
	{
		// 타일기반게임 이동 -> 타일간 이동(x, y좌표 이동 x)
		_tileX = 0;
		_tileY = 0;
		// 해당위치의 타일

		_map = GameManager.Instance.GetMap();
		_map.SetCharcter(_tileX, _tileY, this);
	}

	void Start ()
	{
	}

	void Update ()
	{
		if (true == Input.GetKeyDown(KeyCode.LeftArrow))
			MoveLeft();

		if (true == Input.GetKeyDown(KeyCode.RightArrow))
			MoveRight();

		if (true == Input.GetKeyDown(KeyCode.UpArrow))
			MoveUp();

		if (true == Input.GetKeyDown(KeyCode.DownArrow))
			MoveDown();

	}   // Update()

	void SetPosition(int newX, int newY)
	{
		_tileX = newX;
		_tileY = newY;

		_map = GameManager.Instance.GetMap();
		_map.SetCharcter(_tileX, _tileY, this);
	}

	// 방향에 맞는 animation 실행, 실질적으로 tilemap에서 움직임
	void MoveLeft()
	{
		_model.CharLeftWalk();  // 방향 animation, _model : script => root에 존재함(작업의 편리함)

		int newX = _tileX - 1;
		if (true == _map.CanMove(newX, _tileY)) // 플레이어가 해당위치로 갈 수 있는지 판별
			SetPosition(newX, _tileY);
		
	}   // MoveLeft()

	void MoveRight()
	{
		_model.CharRightWalk();

		int newX = _tileX + 1;
		if (true == _map.CanMove(newX, _tileY))
			SetPosition(newX, _tileY);
	}   // MoveRight()

	void MoveUp()
	{
		_model.CharUpWalk();

		int newY = _tileY + 1;
		if (true == _map.CanMove(_tileX, newY))
			SetPosition(_tileX, newY);
	}   // MoveUp()

	void MoveDown()
	{
		_model.CharDownWalk();

		int newY = _tileY - 1;
		if (true == _map.CanMove(_tileX, newY))
			SetPosition(_tileX, newY);
	}   // MoveDown()

}	// class
