using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    private Player _target;
    private EnemySpawnManager _manager;

    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private float _triggerDelay;
    private float _triggerTimer;
    [SerializeField]
    private int _numberOfTriggers = 1;
    private void Awake()
    {
        _manager = GetComponentInParent<EnemySpawnManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        _target = _manager.Player;
        if(_target == null)
        {
            return;
        }
        if(_triggerTimer == 0)
        {
            _triggerTimer = Time.time + _triggerDelay;
        }

        if(Time.time > _triggerTimer)
        {
            Enemy newenemy = Instantiate(_enemyPrefab, transform.position, Quaternion.identity).GetComponent<Enemy>();
            newenemy.Target = _target;
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
