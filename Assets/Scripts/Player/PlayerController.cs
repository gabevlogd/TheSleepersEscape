using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController
{
    private PlayerInput m_inputs;
    private Transform m_transform;
    private Transform m_cameraTransform;

    private float m_speed;
    private float m_angularSpeed;

    private float m_forwardInput;
    //private float m_verticalInput; not needed
    private float m_lateralInput;

    private float m_pitchInput;
    private float m_yawInput;

    public PlayerController(Transform transform, float speed, float angularSpeed)
    {
        m_transform = transform;
        m_speed = speed;
        m_angularSpeed = angularSpeed;
        m_inputs = new();
        m_inputs.Enable();
        m_cameraTransform = m_transform.GetComponentInChildren<Camera>().transform;
    }

    public void EnableInput()
    {
        Cursor.lockState = CursorLockMode.Locked;

        //m_inputs.Traslation.Vertical.performed += OnPerformVertical;
        m_inputs.Traslation.Lateral.performed += OnPerformLateral;
        m_inputs.Traslation.Forward.performed += OnPerformForward;

        //m_inputs.Traslation.Vertical.canceled += OnCancelVertical;
        m_inputs.Traslation.Lateral.canceled += OnCancelLateral;
        m_inputs.Traslation.Forward.canceled += OnCancelForward;

        m_inputs.Rotation.Pitch.performed += OnPerformPitch;
        m_inputs.Rotation.Yaw.performed += OnPerformYaw;

        m_inputs.Rotation.Pitch.canceled += OnCancelPitch;
        m_inputs.Rotation.Yaw.canceled += OnCancelYaw;
    }

    public void DisableInput()
    {
        Cursor.lockState = CursorLockMode.None;

        //m_inputs.Traslation.Vertical.performed -= OnPerformVertical;
        m_inputs.Traslation.Lateral.performed -= OnPerformLateral;
        m_inputs.Traslation.Forward.performed -= OnPerformForward;

        //m_inputs.Traslation.Vertical.canceled -= OnCancelVertical;
        m_inputs.Traslation.Lateral.canceled -= OnCancelLateral;
        m_inputs.Traslation.Forward.canceled -= OnCancelForward;

        m_inputs.Rotation.Pitch.performed -= OnPerformPitch;
        m_inputs.Rotation.Yaw.performed -= OnPerformYaw;

        m_inputs.Rotation.Pitch.canceled -= OnCancelPitch;
        m_inputs.Rotation.Yaw.canceled -= OnCancelYaw;
    }

    public void HandleMovement()
    {
        MoveForward();
        //MoveVertical();
        MoveLateral();
        MoveYaw();
        MovePitch();
    }


    #region Movements:
    private void MoveForward() => m_transform.Translate(new Vector3(0f, 0f, m_forwardInput * m_speed * Time.deltaTime));
    //private void MoveVertical() => m_transform.Translate(new Vector3(0f, m_verticalInput * m_speed * Time.deltaTime, 0f));
    private void MoveLateral() => m_transform.Translate(new Vector3(m_lateralInput * m_speed * Time.deltaTime, 0f, 0f));

    private void MoveYaw() => m_transform.RotateAround(m_transform.position, m_transform.up, m_yawInput * m_angularSpeed * Time.deltaTime);
    private void MovePitch()
    {
        if (m_cameraTransform.eulerAngles.x > 90) Debug.Log(m_cameraTransform.eulerAngles.x);
        m_cameraTransform.RotateAround(m_cameraTransform.position, m_cameraTransform.right, m_pitchInput * m_angularSpeed * Time.deltaTime);
    }

    #endregion

    #region OnPerform:
    private void OnPerformForward(InputAction.CallbackContext context) => m_forwardInput = context.ReadValue<float>();
    //private void OnPerformVertical(InputAction.CallbackContext context) => m_verticalInput = context.ReadValue<float>();
    private void OnPerformLateral(InputAction.CallbackContext context) => m_lateralInput = context.ReadValue<float>();

    private void OnPerformPitch(InputAction.CallbackContext context) => m_pitchInput = -context.ReadValue<float>();
    private void OnPerformYaw(InputAction.CallbackContext context) => m_yawInput = context.ReadValue<float>();
    #endregion



    #region OnCancel:
    private void OnCancelForward(InputAction.CallbackContext context) => m_forwardInput = 0f;
    //private void OnCancelVertical(InputAction.CallbackContext context) => m_verticalInput = 0f;
    private void OnCancelLateral(InputAction.CallbackContext context) => m_lateralInput = 0f;

    private void OnCancelPitch(InputAction.CallbackContext context) => m_pitchInput = 0f;
    private void OnCancelYaw(InputAction.CallbackContext context) => m_yawInput = 0f;
    #endregion



}
