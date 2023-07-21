using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour { 
    public GameObject lastActive, leftLastActive;
    public GameObject[] weapons;
    public IKControl ikControl;
    public AttackController attackController;
    private void Start()
    {
        EventManager.TwoHandedWeaponSpawned += SpawnTwoHandedWeapon;
        EventManager.OneHandedWeaponSpawned += SpawnOneHandedWeapon;
        EventManager.CrossBowSpawned += SpawnCrossbow;
    }

    private void SpawnTwoHandedWeapon()
    {
        lastActive.SetActive(false);
        leftLastActive.SetActive(false);
        ikControl.ikActive = true;
        var th = weapons[0];
        th.SetActive(true);
        th.transform.SetParent(ikControl.rightHandObj);
        th.transform.localPosition = new Vector3(-0.174999997f, 0.179000005f, -0.0189999994f);
        th.transform.localRotation = new Quaternion(0.206693038f, -0.308702558f, 0.632317901f, 0.679819643f);
        ikControl.rightHandObj.position = th.transform.position;
        ikControl.leftHandObj.position = th.transform.position;
        attackController.attackType = AttackController.AttackType.TwoHandedSword;
        lastActive = th;
    }

    private void SpawnOneHandedWeapon()
    {
        lastActive.SetActive(false);
        leftLastActive.SetActive(false);
        ikControl.ikActive = true;
        var oh = weapons[1];
        var sh = weapons[2];
        oh.SetActive(true);
        sh.SetActive(true);
        oh.transform.SetParent(ikControl.rightHandObj);
        oh.transform.localPosition = new Vector3(-0.119999997f, 0.0410000011f, 0.00100000005f);
        oh.transform.localRotation = new Quaternion(0.740572333f, -0.425489664f, 0.444782734f, 0.2695916f);
        sh.transform.SetParent(ikControl.leftHandObj);
        sh.transform.localPosition = new Vector3(-0.0730366483f, 0.0259790942f, 0.044283092f);
        sh.transform.localRotation = new Quaternion(0.668236792f, 0.31752345f, -0.585976362f, 0.330560535f);
        ikControl.rightHandObj.position = oh.transform.position;
        ikControl.leftHandObj.position = sh.transform.position;
        attackController.attackType = AttackController.AttackType.OneHandedSword;
        lastActive = oh;
        leftLastActive = sh;
    }

    private void SpawnCrossbow()
    {
        lastActive.SetActive(false);
        leftLastActive.SetActive(false);
        ikControl.ikActive = true;
        var cb = weapons[3];
        cb.SetActive(true);
        cb.transform.SetParent(ikControl.leftHandObj);
        cb.transform.localPosition = new Vector3(0.214000002f, 0.308999985f, 0.0250000004f);
        cb.transform.localRotation = new Quaternion(0.471128017f, -0.0817502663f, -0.684128463f, -0.550748229f);
        ikControl.rightHandObj.position = cb.transform.position;
        ikControl.leftHandObj.position = cb.transform.position;
        attackController.attackType = AttackController.AttackType.Crossbow;
        lastActive = cb;
    }
    private void OnDestroy()
    {
        EventManager.CrossBowSpawned -= SpawnCrossbow;
        EventManager.TwoHandedWeaponSpawned -= SpawnTwoHandedWeapon;
        EventManager.OneHandedWeaponSpawned -= SpawnOneHandedWeapon;
    }
}
