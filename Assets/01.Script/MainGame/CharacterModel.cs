using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel : MonoBehaviour {

	void Start()
	{
	}

	void Update()
	{
	}

	public void CharLeftWalk()
	{
		gameObject.GetComponent<Animator>().SetTrigger("Left"); // Left trigger, Animation 실행
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

	public void PlayDamage()
	{
		gameObject.GetComponent<SpriteRenderer>().color = Color.red;
		Invoke("ResetColor", 0.1f);	// 0.1sec 후 원래 모습, ResetColor는 함수!
	}

	void ResetColor()
	{
		gameObject.GetComponent<SpriteRenderer>().color = Color.white;
	}

	public void PlayDead()
	{
		gameObject.GetComponent<SpriteRenderer>().color = Color.black;
		gameObject.GetComponent<SpriteRenderer>().flipY = true;
	}

}	// class
