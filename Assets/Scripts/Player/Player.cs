using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Transform playerTransform;

    [Tooltip("position of the camera (point of view)")]
    public Transform POV;

    public PlayerController PlayerController;
    public CameraController CameraController;

    public PlayerMovementData PlayerMovementData;
    public CameraMovementData CameraMovementData;


    private void Awake()
    {
        playerTransform = transform;

        CameraController = new(GetComponentInChildren<Camera>(), POV, CameraMovementData);
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


