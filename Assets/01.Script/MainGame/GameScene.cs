using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour {
	// player, map 상위 object

	[SerializeField] TileMap _tileMap;

	void Start ()
	{
		_tileMap.Create();
		GameManager.Instance.SetMap(_tileMap);
	}
	
	void Update ()
	{
	}
}
