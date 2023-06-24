using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Transform playerTransform;

    public PlayerController PlayerController;
    public CameraController CameraController;

    public PlayerMovementData PlayerMovementData;
    public CameraMovementData CameraMovementData;

    private void Awake()
    {
        playerTransform = transform;

        CameraController = new(GetComponentInChildren<Camera>(), CameraMovementData);
        CameraController.EnableController();

        PlayerController = new(transform, PlayerMovementData);
        PlayerController.EnableController();
    }


    private void Update()
    {
        PlayerController.HandlePlayerMovement();
        CameraController.HandleCameraMovement();
    }

}


