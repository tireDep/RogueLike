using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour
{
	public void Init(Sprite sprite, int x, int y)
	{
		gameObject.GetComponent<SpriteRenderer>().sprite =sprite;	// 자기자신
		gameObject.transform.localPosition = new Vector2(x, y);
	}	// Init()

} // MapTile Class
