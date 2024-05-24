using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image healthBar;
    private float currentHealth;
    private float maxHealth = 100f;
    PlayerDamaged player;

    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
        player = FindObjectOfType<PlayerDamaged>();
        //Debug.Log("from image: " + player.playerHealth.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = player.playerHealth;
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}
