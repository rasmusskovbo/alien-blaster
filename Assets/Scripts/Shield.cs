using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Shield : MonoBehaviour
{
    private bool shieldIsActive;
    
    void Awake()
    {
        gameObject.SetActive(false);   
    }

    public bool isActive()
    {
        return shieldIsActive;
    }

    public void ActivateShield(float duration)
    {
        gameObject.SetActive(true);
        StartCoroutine(ShieldActivation(duration));
    }

    IEnumerator ShieldActivation(float duration)
    {
        shieldIsActive = true;
        
        yield return new WaitForSecondsRealtime(duration);
        
        shieldIsActive = false;
        gameObject.SetActive(false);
        
    }

}
