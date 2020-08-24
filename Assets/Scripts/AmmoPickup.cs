using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField] int ammoAmount = 5;
    [SerializeField] AmmoType ammoType;
    [SerializeField] bool infinite = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //print("Ammo has been picked up");
            other.GetComponent<Ammo>().IncreaseAmmo(ammoAmount, ammoType);
            if (!infinite)
            {
                Destroy(gameObject);
            }
        }
    }
}
