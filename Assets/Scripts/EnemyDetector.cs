using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;


public class EnemyDetector : MonoBehaviour
{
    public bool isEnemyNearBy = false;
    public float distance, minDistance = 5f;
    public GameObject enemy;
    public IKControl ikControl;
    public Vector3 distanceVector;


    private void Update()
    {
        distanceVector = enemy.transform.position - transform.position;
        distance = distanceVector.magnitude;
        if(distance < minDistance)
        {
            isEnemyNearBy = true;
            ikControl.ikActive = true;
            ikControl.lookObj = enemy.transform;
            ikControl.isEnemyNearBy = true;
        }
        else
        {
            isEnemyNearBy = false;
            ikControl.isEnemyNearBy = false;
        }
    }

}
