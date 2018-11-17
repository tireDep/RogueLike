using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character // Character 상속
{
	void Start ()
	{
	}

	
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
	}

}	// class
