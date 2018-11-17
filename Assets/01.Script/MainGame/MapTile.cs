using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour
{
	public void Init(Sprite sprite, int x, int y)
	{
		gameObject.GetComponent<SpriteRenderer>().sprite =sprite;	// 자기자신
		gameObject.transform.localPosition = new Vector2(x, y);
	}   // Init()

	bool _canMove = false;

	public bool CanMove()
	{
		// 갈 수 있는 타일인지 아닌지만 판별
		return _canMove;
	}

	public void SetCanMove(bool setPostion)
	{
		_canMove = setPostion;
	}

} // MapTile Class
