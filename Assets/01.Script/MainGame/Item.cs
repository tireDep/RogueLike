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

}
