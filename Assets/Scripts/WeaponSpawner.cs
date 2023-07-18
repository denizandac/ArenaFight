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
        th.transform.localPosition = new Vector3(-0.619000018f, 0.149000004f, 0.114f);
        th.transform.localRotation = new Quaternion(0.0571185835f, 0.068987906f, 0.687078059f, 0.721042275f);
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
        oh.transform.localPosition = new Vector3(-0.347000003f, 0.0820000023f, 0.0979999974f);
        oh.transform.localRotation = new Quaternion(0.0571185835f, 0.068987906f, 0.687078059f, 0.721042275f);
        sh.transform.SetParent(ikControl.leftHandObj);
        sh.transform.localPosition = new Vector3(-0.0790000036f, 0.00800000038f, 0.0839999989f);
        sh.transform.localRotation = new Quaternion(0.192056775f, 0.718971729f, 0.660794556f, 0.0976961851f);
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
        cb.transform.localPosition = new Vector3(0.56099999f, -0.201000005f, 0.34799999f);
        cb.transform.localRotation = new Quaternion(0.618779957f, 0.35654819f, 0.317947388f, 0.623613954f);
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
