using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class UiAnimation : Singleton<UiAnimation>
{
    public GameObject[] heartContainers;
    public GameObject clockIcon;

    PlayerInstance player;

    [SerializeField] TextMeshProUGUI timerText;
    float startTime = 10f * 60f;
    float currentTime;

    private void Awake()
    {
        CreateSingleton();
    }

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerInstance>();
        HeartTween();
        ClockTween();
        currentTime = startTime;
    }

	private void Update()
	{
		TickTimer();

        if(currentTime <= 0)
		{
            ///////////// END GAME
		}
	}

    TimeSpan timeSpan;

	void TickTimer()
	{
        if (currentTime <= 0f)
            return;

        currentTime -= Time.deltaTime;

        timeSpan = TimeSpan.FromSeconds(currentTime);

        timerText.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
	}

    void HeartTween()
    {
        for (int i = 0; i < heartContainers.Length; i++)
        {
            heartContainers[i].transform.Rotate(new Vector3(0, 0, -15));
            heartContainers[i].LeanScale(new Vector2(heartContainers[i].transform.localScale.x * 1.2f, heartContainers[i].transform.localScale.y * 1.2f), UnityEngine.Random.Range(1.5f, 5)).setLoopPingPong();
            heartContainers[i].LeanRotateZ(UnityEngine.Random.Range(10, 15), UnityEngine.Random.Range(1.5f, 5)).setLoopPingPong();
        }
    }

    void ClockTween()
    {
        clockIcon.LeanScale(new Vector2(clockIcon.transform.localScale.x * 1.3f, clockIcon.transform.localScale.y * 1.3f), 0.4f).setLoopPingPong();
        clockIcon.LeanRotateZ(clockIcon.transform.rotation.z + 25, 0.8f).setLoopPingPong();
    }

    public void UpdateHearts(int currentHP)
    {
        for (int i = 0; i < heartContainers.Length; i++)
        {
            if (i > currentHP - 1)
            {
                heartContainers[i].SetActive(false);
            }
            if (currentHP == 1)
            {
                heartContainers[i].LeanScale(new Vector2(heartContainers[i].transform.localScale.x * 1.5f, heartContainers[i].transform.localScale.y * 1.5f), 0.2f).setLoopPingPong();
                heartContainers[i].LeanRotateZ(UnityEngine.Random.Range(10, 15), 0.3f).setLoopPingPong();
            }
        }
    }
}
