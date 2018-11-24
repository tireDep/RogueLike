using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;	// Slider 이용

public class CharacterHud : MonoBehaviour {

	void Start ()
	{
	}
	
	void Update ()
	{	
	}

	// hp

	[SerializeField] Slider _hpSlider;	
	public void UpdateHP(int _hp, int _maxHP)	// hp바 설정
	{
		float rate = (float)_hp / (float)_maxHP;	// 백분율
		_hpSlider.value = rate;
	}
}
