using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    [HideInInspector]
    public ParticleSystem Brightness;

    private void Awake() => Brightness = GetComponentInChildren<ParticleSystem>();
}
