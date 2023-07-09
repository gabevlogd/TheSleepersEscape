using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Transform playerTransform;

    [Tooltip("position of the camera (point of view)")]
    public Transform POV;
    public Transform ClockView;

    public PlayerStateMachine PlayerStateMachine;
    public PlayerController PlayerController;
    public CameraController CameraController;
    public ItemsDetector ItemsDetector;

    public PlayerMovementData PlayerMovementData;
    public CameraMovementData CameraMovementData;


    private void Awake()
    {
        playerTransform = transform;

        CameraController = new(GetComponentInChildren<Camera>(), POV, ClockView, CameraMovementData);
        //CameraController.EnableController();

        PlayerController = new(transform, PlayerMovementData);
        //PlayerController.EnableController();

        PlayerStateMachine = new();


    }


    private void Update()
    {
        PlayerStateMachine.CurrentState.OnUpdate();
        PlayerController.HandlePlayerMovement();
        CameraController.HandleCameraMovement();
        
    }

}


