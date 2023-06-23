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
        playerTransform = transform;
        Controller = new(transform, MovementData);
        Controller.EnableInput();
    }


    private void Update()
    {
        Controller.HandleMovement();
    }

}


