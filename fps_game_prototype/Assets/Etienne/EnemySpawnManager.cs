using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    private Player _target;
    private List<EnemySpawnPoint> _spawnPoints = new List<EnemySpawnPoint>();

    //private List<Gates> _gates = new List<Gates>();
    private void Awake()
    {

    }

    private void PlayerDetected(Player player)
    {
        _target = player;
    }
}
