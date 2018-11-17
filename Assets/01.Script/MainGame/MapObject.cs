using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapObject : MonoBehaviour {

	protected bool _canMove = false;

	void Start ()
	{
	}
	
	void Update ()
	{
	}

	public bool CanMove()
	{
		return _canMove;
	}
}
