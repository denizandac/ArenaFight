using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHit : MonoBehaviour
{
    public GameObject bloodEffect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            bloodEffect.SetActive(true);
            bloodEffect.transform.position = other.transform.position;
            bloodEffect.transform.rotation = Random.rotation;
            bloodEffect.transform.SetParent(other.transform);
            //Debug.Log(other.name + "hit at " + other.transform.position); 
        }
    }
}
