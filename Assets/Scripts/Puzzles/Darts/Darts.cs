using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darts : MonoBehaviour
{
    private Rigidbody rb;
    private bool targetHit;
    


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // make sure only to stick to the first target you hit
        if (targetHit)
            return;
        else
            targetHit = true;
        if(collision.gameObject.TryGetComponent(out RightCenter rightCenter))
        {
            

        }

        // make sure projectile sticks to surface
        rb.isKinematic = true;
 
    }





}
