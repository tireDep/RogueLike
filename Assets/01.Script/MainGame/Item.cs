using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MapObject
{
	[SerializeField] int _atkPoint = 1;

	public override int GetAtkPoint()
	{
		_atkPoint = Random.Range(1, 10);    // 랜덤
		return _atkPoint;
	}

	void Start()
	{
	}

	void Update ()
	{	
	}

	public override void Init()  // 상속, 재정의
	{
		base.Init();    // 공통적인 것은 부모클래스것 사용, 나머지 부분만 추가함
		_canMove = true;	// 아이템은 통과 가능
	}
}
