using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel : MonoBehaviour {

	void Start ()
	{
	}
	
	void Update ()
	{
	}

	public void CharLeftWalk()
	{
		gameObject.GetComponent<Animator>().SetTrigger("Left");	// Left trigger, Animation 실행
	}   // CharLeftWalk()

	public void CharRightWalk()
	{
		gameObject.GetComponent<Animator>().SetTrigger("Right");
	}   // CharRightWalk()

	public void CharUpWalk()
	{
		gameObject.GetComponent<Animator>().SetTrigger("Up");
	}   // CharUpWalk()

	public void CharDownWalk()
	{
		gameObject.GetComponent<Animator>().SetTrigger("Down");
	}   // CharDownWalk()

}	// class
