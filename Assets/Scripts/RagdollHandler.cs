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
            ActivateRagdoll();
        }
    }   

    private void EnableColliders(bool enabled)
    {
        foreach (Collider collider in ragdollColliders)
        {
            collider.enabled = enabled;
            collider.isTrigger = enabled;
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
    private void ActivateRagdoll() {
        Debug.Log("Ragdoll activated");
        EnableColliders(true);
        rb.isKinematic = true;
    }
}
