using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Player : MonoBehaviour
{
    [Header("Player Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] float paddingLeft;
    [SerializeField] float paddingRight;
    [SerializeField] float paddingBottom;
    [SerializeField] float paddingTop;
    private Vector2 inputDirection;
    private Vector2 minBounds;
    private Vector2 maxBounds;
    
    private Shooter shooter;

    private void Awake()
    {
        shooter = GetComponent<Shooter>(); 
    }

    private void Start()
    {
        initBounds();
    }

    void Update()
    {
        UpdatePlayerPosition();
    }

    // Movement
    void OnMove(InputValue value)
    {
        inputDirection = value.Get<Vector2>();
    }
    
    private void UpdatePlayerPosition()
    {
        Vector3 delta = inputDirection * moveSpeed * Time.deltaTime;
        Vector2 newPosition = new Vector2();
        
        newPosition.x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + paddingLeft, maxBounds.x - paddingRight);
        newPosition.y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + paddingBottom, maxBounds.y - paddingTop);
        
        this.transform.position = newPosition;
    }

    void initBounds()
    {
        Camera mainCamera = Camera.main;

        minBounds = mainCamera.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCamera.ViewportToWorldPoint(new Vector2(1, 1));
    }
    
    // Shooting
    void OnFire(InputValue value)
    {
        if (shooter)
        {
            shooter.isFiring = value.isPressed;
        }
    }


}
