using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NerfBullet : MonoBehaviour
{
    public string Source;
    public int Damage;
    public bool CanDamage = true;
    [SerializeField]
    private ParticleSystem _ps;
    private bool _psPlayed;

    private void OnCollisionEnter(Collision collision)
    {
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<Rigidbody>().velocity = Vector3.Reflect(GetComponent<Rigidbody>().velocity/5, collision.GetContact(0).normal);
        GetComponent<Rigidbody>().AddTorque(transform.right * 5f,ForceMode.Impulse);
        if(_ps != null && !_psPlayed)
        {
            _ps.Play();
            _psPlayed = true;
        }
    }
}
