using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    private Player _target;

    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private float _triggerDelay;
    private float _triggerTimer;
    [SerializeField]
    private int _numberOfTriggers = 1;
    private void Awake()
    {
        _triggerTimer = Time.time + _triggerDelay;
    }

    // Update is called once per frame
    private void Update()
    {
        if(Time.time > _triggerTimer)
        {
            Enemy newenemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity).GetComponent<Enemy>();
            _numberOfTriggers--;
            if (_numberOfTriggers <= 0)
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
