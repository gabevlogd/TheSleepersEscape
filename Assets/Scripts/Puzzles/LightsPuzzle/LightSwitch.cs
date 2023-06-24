using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public List<Light> LinkedLights;

    [HideInInspector]
    public bool Triggered;

    private Collider m_collider;

    private void OnMouseDown()
    {
        TriggersSwitch();
    }

    private void TriggersSwitch()
    {
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
            if (light.Brightness.isPlaying) light.Brightness.Stop();
            else light.Brightness.Play();
        }
    }
}
