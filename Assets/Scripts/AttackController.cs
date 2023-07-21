using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using System.Net.Mail;

public class AttackController : MonoBehaviour
{
    public bool isAiming, isDefending, canShoot, canAttack;
    public float timeBetweenShoots = 3f, timeBetweenAttacks = 1.3f;
    public Animator animator;
    public Camera mainCamera;
    public GameObject aimCross, defenseSprite, bulletPrefab;
    public CinemachineVirtualCamera fpsCamera;
    
    public Vector3 oldCameraPosition, oldCbPosition;
    public Quaternion oldCameraRotation;
    public AttackType attackType;

    public void Update()
    {
        CheckActions();
        CheckInputs();
        if (timeBetweenShoots > 0f)
        {
            timeBetweenShoots -= Time.deltaTime;
        }
        if(timeBetweenAttacks > 0f)
        {
            timeBetweenAttacks -= Time.deltaTime;
        }
    }
    public void FixedUpdate()
    {
        if(canShoot)
        {
            FireArrow();
            canShoot = false;
            timeBetweenShoots = 3f; 
        }
    }
    private void CheckActions()
    {
        if (isAiming)
        {
            fpsCamera.Priority = 11;
            aimCross.SetActive(true);
            if (Input.GetMouseButtonDown(0) && timeBetweenShoots <= 0f)
            {
                canShoot = true;
            }
        }
        else if (isDefending)
        {
            fpsCamera.Priority = 5;
            defenseSprite.SetActive(true);
        }
        else
        {
            fpsCamera.Priority = 5;
            defenseSprite.SetActive(false);
            aimCross.SetActive(false);

        }
    }
    private void CheckInputs()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (attackType == AttackType.TwoHandedSword && timeBetweenAttacks <= 0f)
            {
                canAttack = true;
                timeBetweenAttacks = 1f;
                animator.SetTrigger("PerformLeftAttack");
            }
            else if (attackType == AttackType.OneHandedSword && timeBetweenAttacks <= 0f)
            {
                canAttack = true;
                timeBetweenAttacks = 1f;
                var attackType = Random.Range(0, 2);
                switch (attackType)
                {
                    case 0:
                        animator.SetTrigger("PerformLeftAttack");
                        break;
                    case 1:
                        animator.SetTrigger("PerformRightAttack");
                        break;
                }
            }
        }
        else if (Input.GetMouseButton(1))
        {
            if (attackType == AttackType.OneHandedSword)
            {
                isDefending = true;
                animator.SetBool("isBlocking", true);

            }
            else if (attackType == AttackType.Crossbow)
            {
                oldCameraPosition = mainCamera.transform.position;
                oldCameraRotation = mainCamera.transform.rotation;
                isAiming = true;
                //animator.SetTrigger("Aim");

            }
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isAiming = false;
            isDefending = false;
            animator.SetBool("isBlocking", false);
        }
        if (attackType == AttackType.TwoHandedSword && timeBetweenAttacks <= 0f)
        {
            if (Input.GetMouseButtonDown(1))
            {
                canAttack = true;
                timeBetweenAttacks = 1f;
                animator.SetTrigger("PerformRightAttack");
            }
        }
    }
    private void FireArrow()
    {
        GameObject bullet = Instantiate(bulletPrefab, mainCamera.transform.position, mainCamera.transform.rotation);
        bullet.GetComponent<Rigidbody>().AddForce(mainCamera.transform.forward * 2500f);
        Destroy(bullet, 3f);
    }
    public enum AttackType
    {
        Unarmed,
        TwoHandedSword,
        OneHandedSword,
        Crossbow
    }
}