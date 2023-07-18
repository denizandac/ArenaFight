using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Animator))]

public class IKControl : MonoBehaviour
{

    protected Animator animator;

    public bool ikActive = false;
    public bool isEnemyNearBy = false;
    public Transform rightHandObj = null;
    public Transform leftHandObj = null;
    public Transform lookObj = null;
    public List<float> objectBoundaries = new List<float>();

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnAnimatorIK()
    {
            if (ikActive)
            {

                if (lookObj != null && isEnemyNearBy)
                {
                    animator.SetLookAtWeight(1f);
                    animator.SetLookAtPosition(lookObj.position);
                }
                else {
                animator.SetLookAtWeight(0f);
                }

                if (rightHandObj != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
                }
                if(leftHandObj != null)
                {
                    animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 1);
                    animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);
                    animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.rotation);
                }

            }

            else
            {
                animator.SetIKPositionWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.RightHand, 0);
                animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, 0);
                animator.SetLookAtWeight(0);
            }
        }

    public List<float> calculateObjectBoundaries(GameObject obj)
    {
        Vector3[] vertices = obj.GetComponent<MeshFilter>().mesh.vertices;
        Vector3 min = vertices[0];
        Vector3 max = vertices[0];
        foreach (Vector3 vertex in vertices)
        {
            min = Vector3.Min(min, vertex);
            max = Vector3.Max(max, vertex);
        }
        return new List<float> { max.x - min.x, max.y - min.y, max.z - min.z };
    }
    public void ikActiveOn()
    {
          ikActive = true;
    } 
    public void ikActiveOff()
    {
        ikActive = false;
    }
}
