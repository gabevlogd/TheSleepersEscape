using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Transform playerTransform;

    public PlayerController Controller;

    public MovementData MovementData;

    private void Awake()
    {
        Controller = new(transform, MovementData);
        playerTransform = transform;
    }

    private void OnEnable() => Controller.EnableInput();
    private void OnDisable() => Controller.DisableInput();

    private void Update()
    {
        Controller.HandleMovement();
    }

}


