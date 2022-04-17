using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnDetector : MonoBehaviour
{
    private EnemySpawnManager _manager;
    private void Awake()
    {
        _manager = GetComponentInParent<EnemySpawnManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_manager.Player == null)
        {
            _manager.Player = other.GetComponent<Player>();
        }
    }
}
