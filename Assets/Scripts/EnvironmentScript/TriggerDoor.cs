using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : MonoBehaviour
{
    private Rigidbody rigidbody;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PandaRolling"))
        {
            rigidbody.isKinematic = false;
        }
    }
}
