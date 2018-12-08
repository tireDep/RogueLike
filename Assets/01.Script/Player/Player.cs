﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character // Character 상속
{
	void Start ()
	{
		_type = eType.PLAYER;
	}

	private GameObject particle;

	void Update ()
	{
		UpdateInput();
	}   // Update()

	void UpdateInput()
	{
		if (true == Input.GetKeyDown(KeyCode.LeftArrow))
			MoveLeft(); // protected로 선언됨(상속받은 객체만 접근 가능)

		if (true == Input.GetKeyDown(KeyCode.RightArrow))
			MoveRight();

		if (true == Input.GetKeyDown(KeyCode.UpArrow))
			MoveUp();

		if (true == Input.GetKeyDown(KeyCode.DownArrow))
			MoveDown();

		// 마우스 좌클릭, 위치 좌표
		if (true==Input.GetMouseButtonDown(0))
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray))
				Instantiate(particle, transform.position, transform.rotation);

			Vector3 mouseClick = ray.origin;	// 클릭 좌표 받아옴

			// 클릭 위치 int화
			//int clickX = (int) Mathf.Round(mouseClick.x);
			//int clickY = (int) Mathf.Round(mouseClick.y);
			//int clickZ = (int) Mathf.Round(mouseClick.z);
			//Debug.Log(clickX + "/" + clickY + "/" + clickZ);

			MovetoClick(mouseClick);

		}
	}

	protected override void Collide(int tileX, int tileY)    // 충돌 관련 다양한 evt 발생 함수 -> 적이 적을 때리는 것 방지용 가상함수화
	{
		MapObject mapObject = _map.GetMapObject(tileX, tileY);

		if (null != mapObject)
		{
			switch (mapObject.GetObjectType())
			{
				case MapObject.eType.ENEMY: // 적일경우 공격
					mapObject.Attacked(this);
					break;
				default:    // 그외 다른 경우 ex) npc면 대화 등
					break;
			}
		}
	}   // Collide()

}	// class
