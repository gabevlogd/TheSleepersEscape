using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public List<Light> LinkedLights;

    [HideInInspector]
    public bool Triggered;

    private Collider m_collider;

    private void Awake()
    {
        m_collider = GetComponent<Collider>();
        GameManager.Instance.EventManager.Register(Enumerators.Events.EnableSwitch, EnableSwitch);
        GameManager.Instance.EventManager.Register(Enumerators.Events.DisableSwitch, DisableSwitch);
    }

    private void OnMouseDown() => TriggersSwitch();
   

    private void TriggersSwitch()
    {
        //if (!LightsManager.GameTriggered) return;
        //if (switch animation is running) return;

        Triggered = !Triggered;

        //placeholder
        MeshRenderer renderer = GetComponentInChildren<MeshRenderer>();
        if (renderer.material.color == Color.red) renderer.material.color = Color.green;
        else renderer.material.color = Color.red;
        //placeholder

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
