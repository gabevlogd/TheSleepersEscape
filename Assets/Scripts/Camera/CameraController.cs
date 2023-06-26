using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CameraController 
{
    private Transform m_targetTransform;
    private Transform m_pov;
    private Collider m_targetCollider;
    private PlayerInput m_input;
    private Camera m_camera;
    private CameraMovementData m_movementData;

    public CameraController(Camera camera, Transform pov, CameraMovementData data)
    {
        m_camera = camera;
        m_movementData = data;

        m_pov = pov;

        m_input = new();
        m_input.Enable();

        GameManager.Instance.EventManager.Registrer(Enumerators.Events.PuzzleCompleted, BackToPlayer);
        GameManager.Instance.EventManager.Registrer(Enumerators.Events.ResetPuzzle, BackToPlayer);
    }


    public void EnableController()
    {
        m_input.Selections.ItemSelection.performed += LookForTarget;
        m_input.Selections.Unselect.performed += BackToPlayer;
    }


    public void DisableController()
    {
        m_input.Selections.ItemSelection.performed -= LookForTarget;
        m_input.Selections.Unselect.performed -= BackToPlayer;
    }


    public void HandleCameraMovement()
    {
        if (m_targetTransform != null)
        {
            MoveCamera(m_targetTransform);

            //Debug.Log("position " + (Vector3.Distance(m_camera.transform.position, m_targetTransform.position) <= 0.0001));
            //Debug.Log("rotation " + (Mathf.Abs(Quaternion.Dot(m_camera.transform.rotation, m_targetTransform.rotation) - 1f) <= 0.0001f));

            if (Vector3.Distance(m_camera.transform.position, m_targetTransform.position) <= 0.0001 && Mathf.Abs(Quaternion.Dot(m_camera.transform.rotation, m_targetTransform.rotation) - 1f) <= 0.0001f)
            {
                m_camera.transform.position = m_targetTransform.position;
                m_camera.transform.rotation = m_targetTransform.rotation;

                this.EnableController();
                if (m_camera.transform.position == m_pov.position) GameManager.Instance.Player.PlayerController.EnableController();

                m_targetTransform = null;
            }
        }
    }

    /// <summary>
    /// Move the camera to the position and rotation of the passed target
    /// </summary>
    private void MoveCamera(Transform target)
    {
        m_camera.transform.position = Vector3.MoveTowards(m_camera.transform.position, target.position, m_movementData.Speed * Time.deltaTime);
        m_camera.transform.rotation = Quaternion.RotateTowards(m_camera.transform.rotation, target.rotation, 100f * m_movementData.AngularSpeed * Time.deltaTime);
    }

    /// <summary>
    /// try to finds a valid target for the camera to move on
    /// </summary>
    private void LookForTarget(InputAction.CallbackContext context)
    {
        if (m_camera.transform.position != m_pov.position) return;
        //Debug.Log("LookForTarget");
        int layerMask = 1 << 6; //puzzle trigger objects layer mask
        Vector3 pointedPosition = m_camera.ScreenToWorldPoint(new Vector3(m_camera.pixelWidth * 0.5f, m_camera.pixelHeight * 0.5f, 1f));
        Vector3 direction = pointedPosition - m_camera.transform.position;
        //Debug.DrawRay(m_camera.transform.position, direction);

        RaycastHit hitInfo;
        if (Physics.Raycast(m_camera.transform.position, direction, out hitInfo, m_movementData.MaxInteractionDistance, layerMask))
        {
            //Debug.Log("Hit");
            m_targetTransform = hitInfo.transform;
            m_targetCollider = hitInfo.collider;

            m_targetCollider.enabled = false;
            GameManager.Instance.Player.PlayerController.DisableController(); //put into gamemanger
            this.DisableController();

            GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.StartPuzzle);
        }
    }

    /// <summary>
    /// Moves back the camera to the player POV (only for debug, the player does not have the possibility to exits from puzzles)
    /// </summary>
    private void BackToPlayer(InputAction.CallbackContext context)
    {
        if (m_camera.transform.position == m_pov.position) return;
        m_targetTransform = m_pov;

        if (m_targetCollider != null) m_targetCollider.enabled = true;
        this.DisableController();
        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.ResetPuzzle);
    }

    public void BackToPlayer()
    {
        if (m_camera.transform.position == m_pov.position) return;
        m_targetTransform = m_pov;

        if (m_targetCollider != null) m_targetCollider.enabled = true;
        this.DisableController();
    }


}




[System.Serializable]
public struct CameraMovementData
{
    [Min(0.5f)]
    public float Speed;
    [Min(0.5f)]
    public float AngularSpeed;
    [Range(1f, 10f)]
    public float MaxInteractionDistance;
}
