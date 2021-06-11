using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    private static float timeRemaining;
    private static float shakeMagnitude;
    private static float fadeTime;

    void LateUpdate()
    {
        if(timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;

            float randX = Random.Range(-1, 1) * shakeMagnitude;
            float randY = Random.Range(-1, 1) * shakeMagnitude;

            transform.position += new Vector3(randX, randY, 0);

            shakeMagnitude = Mathf.MoveTowards(shakeMagnitude, 0, fadeTime * Time.deltaTime);
        }
    }

    public static void StartShake(float duration, float magnitude)
    {
        timeRemaining = duration;
        shakeMagnitude = magnitude;

        fadeTime = magnitude / duration;
    }

}
