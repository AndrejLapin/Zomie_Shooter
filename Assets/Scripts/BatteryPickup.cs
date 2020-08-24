using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPickup : MonoBehaviour
{
    [SerializeField] float restoreAngle = 70f;
    [SerializeField] float restoreIntensity = 4f;
    [SerializeField] bool infinite = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //print("Ammo has been picked up");
            other.GetComponentInChildren<FlashLightSystem>().RestoreLightAngle(restoreAngle);
            other.GetComponentInChildren<FlashLightSystem>().RestoreLightIntensity(restoreIntensity);
            if (!infinite)
            {
                Destroy(gameObject);
            }
        }
    }
}
