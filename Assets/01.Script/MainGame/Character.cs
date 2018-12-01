using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MapObject
{
	// Unity 
	[SerializeField] CharacterModel _model;
	[SerializeField] CharacterHud _hud;

	// Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	public override void Init()  // 상속, 재정의
	{
		base.Init();	// 공통적인 것은 부모클래스것 사용, 나머지 부분만 추가함
		if (null != _hud)   // 이부분만 캐릭터에 적용 -> 가상함수 이용(하향)
			_hp = _maxHP;
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
		else
			Collide(newX, _tileY);  // 가고자 하는 타일에서 충돌이 일어남

	}   // MoveLeft()

	protected void MoveRight()
	{
		_model.CharRightWalk();

		int newX = _tileX + 1;
		if (true == _map.CanMove(newX, _tileY))
			SetPosition(newX, _tileY);
		else
			Collide(newX, _tileY);  // 가고자 하는 타일에서 충돌이 일어남
	}   // MoveRight()

	protected void MoveUp()
	{
		_model.CharUpWalk();

		int newY = _tileY + 1;
		if (true == _map.CanMove(_tileX, newY))
			SetPosition(_tileX, newY);
		else
			Collide(_tileX, newY);	// 가고자 하는 타일에서 충돌이 일어남
	}   // MoveUp()

	protected void MoveDown()
	{
		_model.CharDownWalk();

		int newY = _tileY - 1;
		if (true == _map.CanMove(_tileX, newY))
			SetPosition(_tileX, newY);
		else
			Collide(_tileX, newY);  // 가고자 하는 타일에서 충돌이 일어남
	}   // MoveDown()


	void Collide(int tileX, int tileY)	// 충돌 관련 다양한 evt 발생 함수
	{
		MapObject mapObject = _map.GetMapObject(tileX, tileY);

		if(null!=mapObject)
		{
			switch(mapObject.GetObjectType())
			{
				case MapObject.eType.ENEMY:	// 적일경우 공격
					mapObject.Attacked(this);
					break;
				default:	// 그외 다른 경우 ex) npc면 대화 등
					break;
			}
		}
	}   // Collide()

	public override void Attacked(MapObject attackObject)  // 가상함수화!
	{
		Debug.Log("Character Attacked"); // 충돌 시 공격 들어가는지 확인!
										 // obj마다 공격방식을 다르게 함 -> 가상함수화

		if (true == _isDead)	// 죽으면 필요x
			return;
		Damage(attackObject);
	}

	bool _isDead = false;
	int _hp = 100;
	int _maxHP = 100;	// 최대 hp
	void Damage(MapObject attackObject)
	{
		int atkPoint = attackObject.GetAtkPoint();
		// todo : item 관련 수치 추가
		int finalPoint = atkPoint;

		_hp -= finalPoint;

		if(_hp<=0)	// 죽었을 경우
		{
			_hp = 0;
			Dead();
			_model.PlayDead();
		}
		else
		{
			_model.PlayDamage();
			Debug.Log("attackted -"+atkPoint);
		}

		// UI 관련
		if(null !=_hud)
		{
			_hud.UpdateHP(_hp, _maxHP);
		}
	}

	void Dead()
	{
		_isDead = true;
		Debug.Log("Enemy Dead!");
	}

	// item 관련
	[SerializeField] Item _item;	// = null;
	int _atkPoint = 5;

	public override int GetAtkPoint()
	{
		int itemAtkPoint = 5;

		if(null!=_item)
		{
			itemAtkPoint = _item.GetAtkPoint();
		}

		int finalPoint = _atkPoint + itemAtkPoint;
		return finalPoint;  //	임시
	}
}	// Class()
