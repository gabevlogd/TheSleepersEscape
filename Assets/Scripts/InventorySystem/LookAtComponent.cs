using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtComponent : MonoBehaviour
{
    public Transform Target;
    public float m_angularSpeed;
    [Range(0, 1)]
    public float AlignmentPrecision;


    private bool AlignmentNeeded() => Mathf.Abs(Vector3.Dot(transform.forward, Target.forward)) <= AlignmentPrecision;
    
    private void AlignView()
    {
        //Debug.Log(AlignmentNeeded());
        if (AlignmentNeeded())
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Target.rotation, m_angularSpeed * Time.deltaTime);
            //transform.rotation = Target.rotation;
        }
    }

    private void Update() => AlignView();


}
