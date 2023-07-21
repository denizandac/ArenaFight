using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHit : MonoBehaviour
{
    public int id;
    public GameObject bloodEffect;
    public WeaponType weaponType;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            bloodEffect.SetActive(true);
            bloodEffect.transform.position = other.transform.position;
            bloodEffect.transform.rotation = Random.rotation;
            bloodEffect.transform.SetParent(other.transform);
            
            switch (weaponType)
            {
                case WeaponType.TwoHandedWeapon:
                    EventManager.OnEnemyHit(25, id);
                    break;
                case WeaponType.OneHandedWeapon:
                    EventManager.OnEnemyHit(15, id);
                    break;
                case WeaponType.CrossBow:
                    EventManager.OnEnemyHit(35, id);
                    break;
            }
        }
    }
    public enum WeaponType {
            TwoHandedWeapon,
            OneHandedWeapon,
            CrossBow
    }
}
