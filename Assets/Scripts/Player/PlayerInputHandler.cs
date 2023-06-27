using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler 
{

    public PlayerInput m_inputs;
    public bool isShooting;

    public PlayerInputHandler()
    {
        m_inputs = new();
        m_inputs.Enable();
    }


    private void OnPerformClick(InputAction.CallbackContext context) => isShooting = context.ReadValueAsButton();

    public void EnableController()
    {
        m_inputs.PlayerShoot.Shoot.performed += OnPerformClick;
    }

    public void DisableController()
    {
        m_inputs.PlayerShoot.Shoot.performed -= OnPerformClick;
    }



}
