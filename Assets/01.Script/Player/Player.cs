using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character // Character 상속
{
	void Start ()
	{
		_type = eType.PLAYER;
	}

    float _moveInterval = 0.5f;
    float _moveDuration = 0.0f;
    void Update ()
	{
		UpdateInput();

        // 마우스 클릭 이동
        if (true == _isMoving)
        {
            // 출발~목적지 거리
            //while (destcntX >= 0 || destcntY >= 0)
            if (_moveInterval <= _moveDuration)
            {
                _moveDuration = 0.0f;
                Move();
            }
            else
            {
                _moveDuration += Time.deltaTime;
            }
        }
    }   // Update()

    int newTileX = _tileX;
    int newTileY = _tileY;
    void InputLeft()
    {
        _model.CharLeftWalk();  // 방향 animation, _model : script => root에 존재함(작업의 편리함)
        newTileX = _tileX - 1;
    }
    void InputRight()
    {
        _model.CharRightWalk();
        newTileX = _tileX + 1;
    }
    void InputUp()
    {
        _model.CharUpWalk();
        newTileY = _tileY + 1;
    }
    void InputDown()
    {
        _model.CharDownWalk();
        newTileY = _tileY - 1;
    }

    void UpdateInput()
	{
        Dictionary<KeyCode, System.Action> _charMoveDic = new Dictionary<KeyCode, System.Action>();
        _charMoveDic.Add(KeyCode.LeftArrow, InputLeft);
        _charMoveDic.Add(KeyCode.RightArrow, InputRight);
        _charMoveDic.Add(KeyCode.UpArrow, InputUp);
        _charMoveDic.Add(KeyCode.DownArrow, InputDown);

        if (Input.anyKeyDown)
        {
            foreach (var dic in _charMoveDic)
            {
                if (Input.GetKeyDown(dic.Key))
                {
                    dic.Value();
                    MovePos(newTileX, newTileY);
                }
            }
        }

        if (true==Input.GetMouseButtonDown(0))
        {
            MovetoClick();  // 클릭한 위치로 이동
            _isMoving = true;
        }
    } // UpdateInput()

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
