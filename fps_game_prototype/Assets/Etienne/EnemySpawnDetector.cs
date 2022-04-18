using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnDetector : MonoBehaviour
{
    private EnemySpawnManager _manager;
    private AudioSource _audioWhenTriggered;
    private void Awake()
    {
        _manager = GetComponentInParent<EnemySpawnManager>();
        _audioWhenTriggered = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_manager.Player == null)
        {
            _manager.Player = other.GetComponent<Player>();
            if(_manager.Player != null)
            {
                if(_audioWhenTriggered != null)
                {
                    _audioWhenTriggered.Play();
                }
            }
        }
    }
}
