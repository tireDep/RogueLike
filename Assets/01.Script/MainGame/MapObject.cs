using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour
{

	protected bool _canMove = false;

	public enum eType
	{
		ENEMY   // , NPC etc..
	};  // mapObject type

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

	eType _type = eType.ENEMY;	// Enemy

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
