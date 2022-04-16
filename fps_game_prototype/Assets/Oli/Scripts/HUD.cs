using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HUD : MonoBehaviour
{
    [Header("Health Variables")]
    [SerializeField]
    private Player _player;
    [SerializeField]
    private PlayerWeapon _playerWeapon;
    [SerializeField]
    private GameObject _fullHealth;
    [SerializeField]
    private GameObject _mediumHealth;
    [SerializeField]
    private GameObject _lowHealth;
    [SerializeField]
    private TextMeshProUGUI _healthText;

    [Header("Inventory Variables")]
    [SerializeField]
    private TextMeshProUGUI _ammoText;
    [SerializeField]
    private GameObject _blaster;
    [SerializeField]
    private GameObject _sniper;
    [SerializeField]
    private GameObject _sword;
    [SerializeField]
    private GameObject _waterBalloon;

    public void Update()
    {
        _healthText.text = ("" + _player.Health);

        if (_player.Health >= 75)
        {
            _healthText.color = Color.green;
            _fullHealth.SetActive(true);
            _mediumHealth.SetActive(false);
            _lowHealth.SetActive(false);
        }

        else if (_player.Health <= 75 && _player.Health >= 25)
        {
            _healthText.color = Color.yellow;
            _fullHealth.SetActive(false);
            _mediumHealth.SetActive(true);
            _lowHealth.SetActive(false);
        }

        else if (_player.Health <= 25)
        {
            _healthText.color = Color.red;
            _fullHealth.SetActive(false);
            _mediumHealth.SetActive(false);
            _lowHealth.SetActive(true);
        }

        _ammoText.text = (_player.WeaponManager.CurrentWeapon.AmmoCount + " / " + _player.WeaponManager.CurrentWeapon.MaxAmmo);

        if (_player.WeaponManager.CurrentWeapon.WeaponType == 1)
        {
            _ammoText.text = ("");
            _blaster.SetActive(false);
            _sniper.SetActive(false);
            _sword.SetActive(true);
            _waterBalloon.SetActive(false);
        }

        else if (_player.WeaponManager.CurrentWeapon.WeaponType == 2)
        {
            _blaster.SetActive(true);
            _sniper.SetActive(false);
            _sword.SetActive(false);
            _waterBalloon.SetActive(false);
        }

        else if (_player.WeaponManager.CurrentWeapon.WeaponType == 3)
        {
            _blaster.SetActive(false);
            _sniper.SetActive(true);
            _sword.SetActive(false);
            _waterBalloon.SetActive(false);
        }
        else if (_player.WeaponManager.CurrentWeapon.WeaponType == 4)
        {
            _blaster.SetActive(false);
            _sniper.SetActive(false);
            _sword.SetActive(false);
            _waterBalloon.SetActive(true);
        }
    }
}
