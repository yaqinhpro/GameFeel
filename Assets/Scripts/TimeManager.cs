using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.01f;

    public void SlowMotion()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }

    public void ReturnToNormal()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
    }
}
