using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour
{
	public void Init(Sprite sprite, int x, int y)
	{
		gameObject.GetComponent<SpriteRenderer>().sprite = sprite;  // 자기자신
		gameObject.transform.localPosition = new Vector2(x, y);
	}   // Init()

	bool _canMove = false;
	MapObject _mapObject;

	public bool CanMove()   // 내가 갈 수 있는 타일인지 유무
	{
		if (null != _mapObject) // 갈 수 잇는 오브젝트인지 판별
		{
			return _mapObject.CanMove();
		}
		return _canMove;    // 지형검사
	}

	public void SetCanMove(bool setPostion)
	{
		_canMove = setPostion;
	}

	public void SetMapObject(MapObject mapObject)
	{
		_mapObject = mapObject;
		mapObject.transform.position = gameObject.transform.position;
	}

	/*
	// 나중에 수정해보기!
	List<MapObject> _mapObjectList = new List<MapObject>();
	public void ResetMapObject(MapObject mapObject)
	{
		// 인자로 넘어온 mapObject에 해당하는 요소만
		// _mapObjectList에서 Remove
		// 이렇게 해결 안하면 순서지켜서 코딩해야함
	}
	*/

	public void ResetMapObject()
	{
		_mapObject = null;
	}

	public MapObject GetMapObject()
	{
		return _mapObject;
	}
} // MapTile Class
