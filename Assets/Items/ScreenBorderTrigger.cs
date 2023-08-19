using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenBorderTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Rigidbody rb))
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            var position = transform.position;
            position.z = transform.position.z;
            other.transform.position = position;
        }
    }
}
