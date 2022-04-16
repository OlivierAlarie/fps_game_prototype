using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject Target;
    private List<EnemySpawnPoint> _spawnPoints = new List<EnemySpawnPoint>();

    //private List<Gates> _gates = new List<Gates>();
    void Awake()
    {
        EnemySpawnPoint[] spawnPoints = GetComponentsInChildren<EnemySpawnPoint>(true);
        foreach (var sp in spawnPoints)
        {
            sp.Target = Target;
            _spawnPoints.Add(sp);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
