using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private float _triggerDelay;
    private float _triggerTimer;
    [SerializeField]
    private bool _triggerOnce = true;
    private void Awake()
    {
        _triggerTimer = Time.time + _triggerDelay;
    }

    // Update is called once per frame
    private void Update()
    {
        if(Time.time > _triggerTimer)
        {
            Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
            if (_triggerOnce)
            {
                _triggerTimer = float.PositiveInfinity;
            }
            else
            {
                _triggerTimer = Time.time + _triggerDelay;
            }
        }
    }
}
