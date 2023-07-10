using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightsManager : MonoBehaviour
{
    public List<LightSwitch> LightSwitches;

    public static bool GameTriggered;

    public bool[] TargetPattern;

    private Collider m_collider;
    private Animator m_animator;

    private void Awake()
    {
        //GameTriggered = false;
        m_collider = GetComponent<Collider>();
        m_animator = GetComponent<Animator>();
        GameManager.Instance.EventManager.Register(Enumerators.Events.EnableSwitch, EnableLightsPuzzle);
        GameManager.Instance.EventManager.Register(Enumerators.Events.DisableSwitch, DisableLightsPuzzle);
    }

    private void OnMouseDown() => CheckWinCondition();


    public void CheckWinCondition()
    {
        if (LightSwitches.Count != TargetPattern.Length)
        {
            Debug.Log("the length of Target Pattern need to match the number of switches, modify it in the Light Manager inspector");
            return;
        }

        if (AnimationIsRunning()) return;
        m_animator.SetBool("IsSwitchOn", true);

        StartCoroutine(PlaySound());

        for (int i = 0; i < LightSwitches.Count; i++)
        {
            if (LightSwitches[i].Triggered != TargetPattern[i])
            {
                Debug.Log("not win");
                StartCoroutine(TurnOff());
                return;
            }
        }

        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.DisableSwitch);
        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.TurnOnLights);
        GameManager.Instance.EventManager.TriggerEvent(Enumerators.Events.OpenDoor);

        Debug.Log("win");
    }

    public void EnableLightsPuzzle() => m_collider.enabled = true;
    public void DisableLightsPuzzle() => m_collider.enabled = false; 

    private IEnumerator TurnOff()
    {
        yield return new WaitUntil(() => AnimationIsRunning() == true);
        m_animator.SetBool("IsSwitchOn", false);
        StartCoroutine(PlaySound());
    }

    private bool AnimationIsRunning() => m_animator.GetCurrentAnimatorStateInfo(0).length > m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime;

    private IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(0.8f);
        GameManager.Instance.SoundEventManager.TriggerEvent(Enumerators.MusicEvents.PlaySoundEnv, GameManager.Instance.SoundManager.MasterSwitch);
    }


}


