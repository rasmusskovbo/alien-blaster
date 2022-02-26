using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 1f;
    [SerializeField] private float shakeMagnitude = 0.5f;
    
    private Vector3 initialPosition;
    
    void Start()
    {
        initialPosition = transform.position;
    }

    public void Play()
    {
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        float remainingTime = shakeDuration;

        while (remainingTime > 0)
        {
            transform.position = initialPosition + (Vector3) Random.insideUnitCircle * shakeMagnitude;
            remainingTime -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transform.position = initialPosition;
    }
}
