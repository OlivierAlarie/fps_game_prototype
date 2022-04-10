using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NerfBullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        Debug.Log("Collided");
    }
}
