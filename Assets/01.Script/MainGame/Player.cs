using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	// Unity 
	void Start ()
	{
		// 타일기반게임 이동 -> 타일간 이동(x, y좌표 이동 x)
		int tileX = 5;
		int tileY = 5;
		// 해당위치의 타일

		TileMap map = GameManager.Instance.GetMap();
		map.SetCharcter(tileX, tileY, this);
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

	[SerializeField] CharacterModel _model;

	// 방향에 맞는 animation 실행, 실질적으로 tilemap에서 움직임
	void MoveLeft()
	{
		_model.CharLeftWalk();  // 방향 animation, _model : script => root에 존재함(작업의 편리함)
	}   // MoveLeft()

	void MoveRight()
	{
		_model.CharRightWalk();
	}   // MoveRight()

	void MoveUp()
	{
		_model.CharUpWalk();
	}   // MoveUp()

	void MoveDown()
	{
		_model.CharDownWalk();
	}   // MoveDown()

}	// class
