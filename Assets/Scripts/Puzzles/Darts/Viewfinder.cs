using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viewfinder : MonoBehaviour
{

    [SerializeField]
    private float ViewfinderDistanceToThePlayer;
    public float Radius;

    private void Start()
    {
        transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, ViewfinderDistanceToThePlayer));
    }




}

    

