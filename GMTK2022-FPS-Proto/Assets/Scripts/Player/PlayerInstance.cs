using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerInstance : Singleton<PlayerInstance>
{
	[SerializeField] int maxHealthPoints;
	[SerializeField] float invulnerabilityTime;
	[HideInInspector] public int currentHealthPoints;
	Clock invulnerabilityTimer;
	bool canTakeDamage = true;

	UiAnimation ui;
	AudioManager audioManager;

	private void Awake()
	{
		CreateSingleton();
	}

	private void Start()
	{
		currentHealthPoints = maxHealthPoints;

		invulnerabilityTimer = new Clock();
		invulnerabilityTimer.ClockEnded += InvulnerabilityTimer_ClockEnded;
		ui = GameObject.Find("Player_UI").GetComponent<UiAnimation>();
		audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
	}

	private void InvulnerabilityTimer_ClockEnded()
	{
		canTakeDamage = true;
	}
	
	public bool ReceiveDamage(int amount)
	{
		if (!canTakeDamage)
			return false;

		canTakeDamage = false;
		invulnerabilityTimer.SetTime(invulnerabilityTime);

		if(currentHealthPoints - amount <= 0)
		{
			currentHealthPoints = 0;
			Death();
		}
		else
		{
			currentHealthPoints -= amount;
			ui.UpdateHearts(currentHealthPoints);
			audioManager.PlaySound(12, gameObject.transform.position);
		}

		return true;
	}

	private void Death()
	{
#if UNITY_EDITOR
		EditorApplication.ExitPlaymode();
#else
		Application.Quit();
#endif
	}
}