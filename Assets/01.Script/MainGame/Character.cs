﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MapObject
{
	// Unity 
	[SerializeField] protected CharacterModel _model;
	[SerializeField] CharacterHud _hud;

    // Use this for initialization
    void Start ()
	{
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

    int destcntX = 0;
    int destcntY = 0;
    protected bool _isMoving = false;
    protected void Move()
    {
        if (true == _map.CanMove((int)_destPos.x, (int)_destPos.y))
        {
            int moveX = EasyGetDirection(_tileX, (int)_destPos.x);
            int moveY = EasyGetDirection(_tileY, (int)_destPos.y);
            //_model.CharDownWalk();  // animation 출력

            if (true == _map.CanMove(moveX, moveY))
                Move(moveX, moveY);
            else
                Collide(moveX, moveY);

            destcntX--;
            destcntY--;
        }
        else
        {
            Collide((int)_destPos.x, (int)_destPos.y);  // 가고자 하는 타일에서 충돌이 일어남
            destcntX = 0;
            destcntY = 0;
        }

        if (0 == destcntX && 0 == destcntY)
        {
            _isMoving = false;
        }
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


	public Vector2 GetPosition()
	{
		return new Vector2(_tileX, _tileY);
	}

    // 방향에 맞는 animation 실행, 실질적으로 tilemap에서 움직임

    protected void MovePos(int tileX, int tileY)
    {
        if (true == _map.CanMove(tileX, tileY)) // 플레이어가 해당위치로 갈 수 있는지 판별
        {
            Move(tileX, tileY);
        }
        else
        {
            Collide(tileX, tileY);  // 가고자 하는 타일에서 충돌이 일어남
        }
    }
    

    private GameObject particle;    
    Vector3 _destPos = Vector3.zero;
    void SetDestPos(Vector3 destPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(destPos);
        if (Physics.Raycast(ray))
            Instantiate(particle, transform.position, transform.rotation);

        int destX = (int)Mathf.Round(ray.origin.x);
        int destY = (int)Mathf.Round(ray.origin.y);
        int destZ = 0;  // z축은 사용 x
        _destPos = new Vector3(destX, destY, destZ);
    } // GetMousePos()

    int EasyGetDirection(int currentTile, int destTile) // 목적지까지의 좌표값 계산
    {
        int moveTile;
        if (currentTile < destTile)
            moveTile = currentTile + 1;
        else if (currentTile > destTile)
            moveTile = currentTile - 1;
        else
            moveTile = currentTile;

        return moveTile;
    }

    protected void MovetoClick()	// 클릭으로 이동함 => 현 상황 순간이동
	{
        Vector3 clickPos = Input.mousePosition;
        SetDestPos(clickPos);  // 클릭좌표를 받고, 목적지로 설정

        Debug.Log(_destPos); // 좌표 출력
        destcntX = Mathf.Abs((int)_destPos.x - _tileX);
        destcntY = Mathf.Abs((int)_destPos.y - _tileY);
        _isMoving = true;
        Move();
    }   // MovetoClick(Vector3 mouseClick)


	// item 관련

	[SerializeField] Item _item = null;
    [SerializeField] GameObject _itemRoot = null;	// ex) 손 등
	void Equip(MapObject mapObject)	// 장비 함수(무슨 오브젝트인지는 모름)
	{
		// 현 위치 플레이어의 아이템 제거
		GameObject.Destroy(_item.gameObject);
		// GameObject.DestroyImmediate(_item.gameObject, true);

		// 새 위치의 아이템을 플레이어에게 붙임
		mapObject.transform.parent = _itemRoot.transform;
		mapObject.transform.localPosition = Vector3.zero;
		mapObject.transform.localScale = Vector3.one;
		// 이미지상 같다 붙임

		_item = mapObject.GetComponent<Item>();
		// 아이템 사용

	}

	void Move(int tileX, int tileY)	// 갔을 경우 evt(아이템 등)
	{
		if (true == _isDead)	//  죽으면 이동 x
			return;

		// 현 위치 플레이어 삭제
		_map.ResetMapObject(_tileX, _tileY);
		// 직전 위치 삭제(초기화)

		// 새 위치 아이템 존재시
		MapObject mapObject = _map.GetMapObject(tileX, tileY);
		if (null != mapObject)
		{
			switch (mapObject.GetObjectType())
			{
				case MapObject.eType.ITEM:
					Equip(mapObject);//장착
					break;
				default:    // 그외 다른 경우 ex) npc면 대화 등
					break;
			}
		}

	// 새 위치로 플레이어 바꿈 => 기존함수 : SetPosition(tileX, tileY);
	_tileX = tileX;
	_tileY = tileY;
	_map.SetMapObject(_tileX, _tileY, this);
	} // Move()

	protected virtual void Collide(int tileX, int tileY)	// 충돌 관련 다양한 evt 발생 함수 -> 적이 적을 때리는 것 방지용 가상함수화
	{
		/*MapObject mapObject = _map.GetMapObject(tileX, tileY);

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
		}*/
	}   // Collide()

	public override void Attacked(MapObject attackObject)  // 가상함수화!
	{
		Debug.Log("Character Attacked"); // 충돌 시 공격 들어가는지 확인!
										 // obj마다 공격방식을 다르게 함 -> 가상함수화

		if (true == _isDead)	// 죽으면 필요x
			return;
		Damage(attackObject);
	}

	protected bool _isDead = false;
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
