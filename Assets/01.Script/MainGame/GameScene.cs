using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour {
	// player, map 상위 object

	[SerializeField] TileMap _tileMap;
	[SerializeField] Player _player;

	void Start ()
	{
		_tileMap.Create();
		GameManager.Instance.SetMap(_tileMap);

		_player.Init();	// Null error 해결!
	}
	
	void Update ()
	{
	}
}
