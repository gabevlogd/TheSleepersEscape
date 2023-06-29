using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController
{
    public bool Enabled;

    private PlayerInput m_inputs;
    private Transform m_transform;
    private Transform m_cameraTransform;

    private PlayerMovementData m_movementData;

    public PlayerController(Transform transform, PlayerMovementData data)
    {
        m_movementData = data;
        m_transform = transform;
        m_cameraTransform = m_transform.GetComponentInChildren<Camera>().transform;

        m_inputs = new();
        m_inputs.Enable();
    }

    public void EnableController()
    {
        Enabled = true;
        //Debug.Log("EnableController");
        Cursor.lockState = CursorLockMode.Locked;

        m_inputs.Traslation.Lateral.performed += OnPerformLateral;
        m_inputs.Traslation.Forward.performed += OnPerformForward;

        m_inputs.Traslation.Lateral.canceled += OnCancelLateral;
        m_inputs.Traslation.Forward.canceled += OnCancelForward;

        m_inputs.Rotation.Pitch.performed += OnPerformPitch;
        m_inputs.Rotation.Yaw.performed += OnPerformYaw;

        m_inputs.Rotation.Pitch.canceled += OnCancelPitch;
        m_inputs.Rotation.Yaw.canceled += OnCancelYaw;
    }

    public void DisableController()
    {
        Enabled = false;
        //Debug.Log("DisableController");
        Cursor.lockState = CursorLockMode.None;

        m_inputs.Traslation.Lateral.performed -= OnPerformLateral;
        m_inputs.Traslation.Forward.performed -= OnPerformForward;

        m_inputs.Traslation.Lateral.canceled -= OnCancelLateral;
        m_inputs.Traslation.Forward.canceled -= OnCancelForward;

        m_inputs.Rotation.Pitch.performed -= OnPerformPitch;
        m_inputs.Rotation.Yaw.performed -= OnPerformYaw;

        m_inputs.Rotation.Pitch.canceled -= OnCancelPitch;
        m_inputs.Rotation.Yaw.canceled -= OnCancelYaw;

        m_movementData.ForwardInput = 0f;
        m_movementData.LateralInput = 0f;
        m_movementData.PitchInput = 0f;
        m_movementData.YawInput = 0f;

    }

    public void HandlePlayerMovement()
    {
        MoveForward();
        MoveLateral();
        MoveYaw();
        MovePitch();
    }


    #region Movements:
    private void MoveForward()
    {
        if (m_movementData.ForwardInput > 0 && !Physics.Raycast(Player.playerTransform.position, Player.playerTransform.forward, 0.5f, -1 ,QueryTriggerInteraction.Ignore))
            m_transform.Translate(new Vector3(0f, 0f, m_movementData.ForwardInput * m_movementData.Speed * Time.deltaTime));
        else if (m_movementData.ForwardInput < 0 && !Physics.Raycast(Player.playerTransform.position, -Player.playerTransform.forward, 0.5f, -1, QueryTriggerInteraction.Ignore))
            m_transform.Translate(new Vector3(0f, 0f, m_movementData.ForwardInput * m_movementData.Speed * Time.deltaTime));
    }

    private void MoveLateral()
    {
        if (m_movementData.LateralInput > 0 && !Physics.Raycast(Player.playerTransform.position, Player.playerTransform.right, 0.5f, -1 ,QueryTriggerInteraction.Ignore))
            m_transform.Translate(new Vector3(m_movementData.LateralInput * m_movementData.Speed * Time.deltaTime, 0f, 0f));
        else if (m_movementData.LateralInput < 0 && !Physics.Raycast(Player.playerTransform.position, -Player.playerTransform.right, 0.5f, -1 ,QueryTriggerInteraction.Ignore))
            m_transform.Translate(new Vector3(m_movementData.LateralInput * m_movementData.Speed * Time.deltaTime, 0f, 0f));
    }

    private void MoveYaw() => m_transform.Rotate(0F, m_movementData.YawInput * m_movementData.AngularSpeed * Time.deltaTime, 0f);
    private void MovePitch()
    {
        float angle = Vector3.SignedAngle(m_cameraTransform.forward, m_transform.forward, m_transform.right);
        float pitchRotation = m_movementData.PitchInput * m_movementData.AngularSpeed * Time.deltaTime;

        if (angle > m_movementData.PitchMaxDegrees)
        {
            if (m_movementData.PitchInput > 0) m_cameraTransform.Rotate(pitchRotation, 0f, 0f);
        }
        else if (angle < m_movementData.PitchMinDegrees)
        {
            if (m_movementData.PitchInput < 0) m_cameraTransform.Rotate(pitchRotation, 0f, 0f);
        }
        else m_cameraTransform.Rotate(pitchRotation, 0f, 0f);
    }

    #endregion

    #region OnPerform:
    private void OnPerformForward(InputAction.CallbackContext context) => m_movementData.ForwardInput = context.ReadValue<float>();
    private void OnPerformLateral(InputAction.CallbackContext context) => m_movementData.LateralInput = context.ReadValue<float>();

    private void OnPerformPitch(InputAction.CallbackContext context) => m_movementData.PitchInput = -context.ReadValue<float>();
    private void OnPerformYaw(InputAction.CallbackContext context) => m_movementData.YawInput = context.ReadValue<float>();
    #endregion

    #region OnCancel:
    private void OnCancelForward(InputAction.CallbackContext context) => m_movementData.ForwardInput = 0f;
    private void OnCancelLateral(InputAction.CallbackContext context) => m_movementData.LateralInput = 0f;

    private void OnCancelPitch(InputAction.CallbackContext context) => m_movementData.PitchInput = 0f;
    private void OnCancelYaw(InputAction.CallbackContext context) => m_movementData.YawInput = 0f;
    #endregion
}

[System.Serializable]
public struct PlayerMovementData
{
    public float Speed;
    public float AngularSpeed;
    [Range(1f, 90f)]
    public float PitchMaxDegrees;
    [Range(-1f, -90f)]
    public float PitchMinDegrees;

    [HideInInspector]
    public float ForwardInput;
    [HideInInspector]
    public float LateralInput;
    [HideInInspector]
    public float PitchInput;
    [HideInInspector]
    public float YawInput;
}


