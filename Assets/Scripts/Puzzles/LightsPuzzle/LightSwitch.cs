using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public List<Light> LinkedLights;

    [HideInInspector]
    public bool Triggered;

    private Collider m_collider;
    private Animator m_animator;

    private void Awake()
    {
        m_collider = GetComponent<Collider>();
        m_animator = GetComponent<Animator>();
        GameManager.Instance.EventManager.Register(Enumerators.Events.EnableSwitch, EnableSwitch);
        GameManager.Instance.EventManager.Register(Enumerators.Events.DisableSwitch, DisableSwitch);
    }

    private void OnMouseDown() => TriggersSwitch();
   

    private void TriggersSwitch()
    {
        //if (!LightsManager.GameTriggered) return;
        if (m_animator.GetCurrentAnimatorStateInfo(0).length > m_animator.GetCurrentAnimatorStateInfo(0).normalizedTime) return;

        Triggered = !Triggered;

        m_animator.SetBool("IsSwitchOn", !m_animator.GetBool("IsSwitchOn"));

        GameManager.Instance.SoundEventManager.TriggerEvent(Enumerators.MusicEvents.PlaySoundEnv, GameManager.Instance.SoundManager.SwitchLight);

        TriggersLights();
    }

    private void TriggersLights()
    {
        foreach(Light light in LinkedLights)
        {
            if (light.Particle.activeInHierarchy) light.Particle.SetActive(false);
            else light.Particle.SetActive(true);
        }
    }

    public void EnableSwitch() => m_collider.enabled = true;
    public void DisableSwitch() => m_collider.enabled = false;
}
