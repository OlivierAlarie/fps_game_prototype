using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HUD : MonoBehaviour
{
    [Header("Health Variables")]
    [SerializeField]
    private int HealthTracker;
    [SerializeField]
    private TextMeshProUGUI _healthText;

    [Header("Inventory Variables")]
    [SerializeField]
    public int AmmoTracker;
    [SerializeField]
    private TextMeshProUGUI _ammoText;

    public void Update()
    {
        HealthTracker = gameObject.GetComponent<Player>().Health;
        _healthText.text = ("X" + HealthTracker);
        _ammoText.text = ("X" + AmmoTracker);
    }

    public void AddHealth()
    {
        HealthTracker++;
    }

    public void AddAmmo()
    {
        AmmoTracker++;
    }

    public void removeLife()
    {
        AmmoTracker--;
        if (AmmoTracker <= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Dead");
        }
    }
}
