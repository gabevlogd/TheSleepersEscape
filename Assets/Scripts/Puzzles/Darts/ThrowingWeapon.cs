using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ThrowingWeapon : MonoBehaviour
{
    public GameObject Darts;
    public Transform DartsPosition;
    public Transform Viewfinder;

   






    private void Shoot()
    {
        GameObject FirstDarts = Instantiate(Darts, DartsPosition.position, Quaternion.identity);
    }

    


}
