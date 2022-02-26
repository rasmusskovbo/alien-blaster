using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteScroller : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    [SerializeField]private float maxScrollLength = -28;
    
    private Vector2 initialPosition;
    private Vector2 offset;

    private void Awake()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        if (transform.position.y > maxScrollLength)
        {
            float offset = scrollSpeed * Time.deltaTime;
            transform.position = new Vector2(transform.position.x, transform.position.y - offset);
            Debug.Log(transform.position.y);
        }
        else
        {
            Debug.Log("Resetting position");
            transform.position = initialPosition;
        }
        
    }

}
