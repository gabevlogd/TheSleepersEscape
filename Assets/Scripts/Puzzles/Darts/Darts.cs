using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Darts : MonoBehaviour
{
    private Rigidbody m_rigidBody;


    private void OnEnable()
    {
        m_rigidBody = GetComponent<Rigidbody>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        Target.ReadyToThrow = true;
        m_rigidBody.isKinematic = true;

        if (collision.gameObject.TryGetComponent(out Target target))
        {
           
          
            Debug.Log(collision.contacts[0].point);
            if (target.BullEyeHitted(collision.contacts[0].point)) target.SetScore(1);
            else target.SetScore(0);


        }

    }





}
