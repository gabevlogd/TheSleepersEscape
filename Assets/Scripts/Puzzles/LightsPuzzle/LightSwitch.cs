using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public List<Light> LinkedLights;

    [HideInInspector]
    public bool Triggered;

    private void OnMouseDown() => TriggersSwitch();
   

    private void TriggersSwitch()
    {
        if (!LightsManager.GameTriggered) return;

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
}
