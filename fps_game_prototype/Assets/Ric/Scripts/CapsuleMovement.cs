using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 10f;

    
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, 0, _movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= new Vector3(0, 0, _movementSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= new Vector3(_movementSpeed * Time.deltaTime,0, 0 );
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(_movementSpeed * Time.deltaTime, 0, 0);
        }
    }
}
