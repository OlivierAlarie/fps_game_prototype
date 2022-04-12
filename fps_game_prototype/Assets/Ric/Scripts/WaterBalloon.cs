using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBalloon : MonoBehaviour
{
    
    public GameObject explosionFX;
    public int maxCollisionNumber = 3;
    private int collisions = 0;

    private void Start() 
    {
        if (explosionFX == null)
        explosionFX =  GameObject.FindWithTag("Water Splash");
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
        Destroy(gameObject,0.2f);
        Destroy(go,1f);
    }
}
