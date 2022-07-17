using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiAnimation : MonoBehaviour
{
    public GameObject[] heartContainers;
    public GameObject clockIcon;
    void Start()
    {
        HeartTween();
        ClockTween();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void HeartTween()
    {
        for (int i = 0; i < heartContainers.Length; i++)
        {
            heartContainers[i].transform.Rotate(new Vector3(0, 0, -30));
            heartContainers[i].LeanScale(new Vector2(heartContainers[i].transform.localScale.x * 1.2f, heartContainers[i].transform.localScale.y * 1.2f), Random.Range(1.5f, 5)).setLoopPingPong();
            heartContainers[i].LeanRotateZ(Random.Range(20, 30), Random.Range(1.5f, 5)).setLoopPingPong();
        }
    }

    void ClockTween()
    {
        clockIcon.LeanScale(new Vector2(clockIcon.transform.localScale.x * 1.3f, clockIcon.transform.localScale.y * 1.3f), 0.4f).setLoopPingPong();
        clockIcon.LeanRotateZ(clockIcon.transform.rotation.z + 25, 0.8f).setLoopPingPong();
    }
}
