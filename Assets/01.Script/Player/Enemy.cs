using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character	// Character 상속
{
	void Start()
	{
	}

	void Update()
	{
		UpdateAI();
	}

	float _aiSpeed = 0.4f;
	float _aiTime = 0.0f;

	void UpdateAI()
	{
		if (true == _isDead)   // 죽었을 때 실행 x
			return;

		if(_aiTime< _aiSpeed)  // 일정 시간간격 두고 움직임
		{
			_aiTime += Time.deltaTime;
			return;
		}
		_aiTime = 0.0f;

		// 기초적인 길찾기 -> 목표가 있으면 일단 움직임(장애물 등 신경 x)
		Character player = GameManager.Instance.GetPlayer();
		Vector2 playerPos = player.GetPosition();
        // 플레이어를 가지고오고 위치 확인

        int newTileX = _tileX;
        int newTileY = _tileY;
        if (_tileX > playerPos.x)
        {
            _model.CharLeftWalk();  // 방향 animation, _model : script => root에 존재함(작업의 편리함)
            newTileX = _tileX - 1;
        }
		if (_tileX < playerPos.x)
        {
            _model.CharRightWalk();
            newTileX = _tileX + 1;
        }
        if (_tileY < playerPos.y)
        {
            _model.CharUpWalk();
            newTileY = _tileY + 1;
        }
        if (_tileY > playerPos.y)
        {
            _model.CharDownWalk();
            newTileY = _tileY - 1;
        }
        MovePos(newTileX, newTileY);
    }

	protected override void Collide(int tileX, int tileY)    // 충돌 관련 다양한 evt 발생 함수 -> 적이 적을 때리는 것 방지용 가상함수화
	{
		MapObject mapObject = _map.GetMapObject(tileX, tileY);

		if (null != mapObject)
		{
			switch (mapObject.GetObjectType())
			{
				case MapObject.eType.PLAYER: // 적일경우 공격
					mapObject.Attacked(this);
					break;
				default:    // 그외 다른 경우 ex) npc면 대화 등
					break;
			}
		}
	}   // Collide()
}
