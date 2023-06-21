using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Transform playerTransform;

    public float Speed;
    public float AngularSpeed;
    public PlayerController Controller;

    private void Awake()
    {
        Controller = new(transform, Speed, AngularSpeed);
        playerTransform = transform;
    }

    private void OnEnable() => Controller.EnableInput();
    private void OnDisable() => Controller.DisableInput();

    private void Update()
    {
        Controller.HandleMovement();
    }

}
