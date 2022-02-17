using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Player : MonoBehaviour
{
    [SerializeField] float horizontalMoveSpeed;
    [SerializeField] float verticalMoveSpeed;
    
    private Vector2 rawInput;
    
    
    void Update()
    {
        Vector3 delta = rawInput;
        transform.position += delta;
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }
}
