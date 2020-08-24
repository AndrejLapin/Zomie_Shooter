using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacks : MonoBehaviour
{
    PlayerHealth target;
    [SerializeField] float damage = 40f;

    void Start()
    {
        target = FindObjectOfType<PlayerHealth>();
    }

    // Update is called once per frame
    public void AttackHitEvent()
    {
        if (target == null) return;
        target.TakeDamage(damage);
        //Debug.Log("bang bang");
    }
}
