using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
	// Unity 기능 사용x라서 다 지움!
	// Singleton

	static GameManager _instance;
	public static GameManager Instance
	{
		get
		{
			if (null == _instance)	//	생성 x시 생성
			{
				_instance = new GameManager();
			}
			return _instance;
		}
	}   // Instance

	TileMap _tileMap;
	public TileMap GetMap()
	{
		return _tileMap;
	}

	public void SetMap(TileMap tileMap)
	{
		_tileMap = tileMap;
	}

	Character _player = null;

	public void SetPlayer(Character player)
	{
		_player = player;
	}

	public Character GetPlayer()
	{
		return _player;
	}

}	// class
