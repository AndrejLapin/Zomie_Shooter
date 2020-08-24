using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float playerHealth = 100f;
    DeathHandler myDeathHandler;
    DisplayDamage myDamageDisplay;


    private void Start()
    {
        myDeathHandler = GetComponent<DeathHandler>();
        myDamageDisplay = FindObjectOfType<DisplayDamage>();
    }

    public void TakeDamage(float damage)
    {
        playerHealth -= damage;
        myDamageDisplay.ShowDamageImpact();

        if (playerHealth <= 0)
        {
            print("Player has died");
            myDeathHandler.HandleDeath();
        }
    }
}
