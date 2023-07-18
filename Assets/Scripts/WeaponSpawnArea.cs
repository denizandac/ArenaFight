using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponSpawnArea : MonoBehaviour
{
    public int weaponID, spawnerID;
    public Animator animator;

    private void Start()
    {
        EventManager.SpawnButtonPressed += SpawnButtonPressed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {//Debug.Log(other.tag + " " + other.name + " entered");
            EventManager.OnSpawnButtonPressed(spawnerID);
            switch (weaponID)
            {
                case 0:
                    EventManager.OnTwoHandedWeaponSpawned();
                    animator.SetBool("TwoHanded", true);
                    break;
                case 1:
                    EventManager.OnOneHandedWeaponSpawned();
                    break;
                case 2:
                    EventManager.OnCrossBowSpawned();
                    break;
                default:
                    break;
            }
        }
    }
    private void SpawnButtonPressed(int id)
    {
        if(id == spawnerID)
        {
        transform.DOLocalMoveY(-4f, 4f);
        }
    }
    
    private void OnDestroy()
    {
        EventManager.SpawnButtonPressed -= SpawnButtonPressed;
    }
}
