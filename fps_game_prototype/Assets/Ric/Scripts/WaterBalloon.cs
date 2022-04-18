using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBalloon : MonoBehaviour
{
    
    public GameObject explosionFX;
    public GameObject SplashFX;
    public int maxCollisionNumber = 2;
    private int collisions = 0;
    public float explosionRange = 2.2f;
    public int explosionDamage = 20;
    private Enemy enemy;
    public AudioClip[] waterBalloonSounds;
    public AudioSource audioSource;

    private void Start() 
    {
        enemy = FindObjectOfType<Enemy>();
    }
    void OnCollisionEnter(Collision other) 
    {
        if (explosionFX == null)
        {
            return;
        }
        collisions++;
        if(other.gameObject.CompareTag("Enemy"))
        {
            Explode();
        }
        else if( collisions >= maxCollisionNumber)
        {
            Explode();
            collisions = 0;
        }
    }

    public void Explode()
    {
        GameObject go = Instantiate(explosionFX,transform.position,Quaternion.identity);
        GameObject goagain = Instantiate(SplashFX, transform.position, Quaternion.identity);
        int g = Random.Range(0,waterBalloonSounds.Length);
        audioSource.clip = waterBalloonSounds[g];
        audioSource.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;        
        Destroy(gameObject,1f);
        Destroy(go,1f);
        Destroy(goagain, 1f);

        Collider[] enemies = Physics.OverlapSphere(transform.position, explosionRange, LayerMask.GetMask("Enemies"));
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].GetComponent<Enemy>().Health -= explosionDamage;
        }
    }

    private void OnDrawGizmosSelected() 
    {   Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,explosionRange);
    }
}
