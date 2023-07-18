using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollHandler : MonoBehaviour
{
    public GameObject ragdoll;
    public Rigidbody rb;
    public Rigidbody[] ragdollRigidbodies;
    public BoxCollider boxCollider;
    public Collider[] ragdollColliders;
    public Animator animator;

    private void Start()
    {
        ragdollRigidbodies = ragdoll.GetComponentsInChildren<Rigidbody>();
        ragdollColliders = ragdoll.GetComponentsInChildren<Collider>();
        EnableColliders(false);
        SetRigidbodies(true);
        boxCollider.enabled = true;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            ActivateHits();
        }
    }   

    private void EnableColliders(bool enabled)
    {
        foreach (Collider collider in ragdollColliders)
        {
            collider.enabled = enabled;
            collider.isTrigger = !enabled;
            animator.enabled = !enabled;
        }
    }

    private void SetRigidbodies(bool enabled)
    {
        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.useGravity = !enabled;
            rb.isKinematic = enabled;
        }
    }
    public void ActivateRagdoll()
    {
        EnableColliders(true);
        SetRigidbodies(false);
        boxCollider.enabled = false;
        rb.isKinematic = false;
    }
    private void ActivateHits() {
        foreach (Collider collider in ragdollColliders)
        {
            collider.enabled = enabled;
            collider.isTrigger = enabled;
        }
        rb.isKinematic = true;
    }
}
