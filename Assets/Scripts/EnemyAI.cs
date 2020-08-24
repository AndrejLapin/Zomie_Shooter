﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] float chaseRange = 5f;
    [SerializeField] float turnSpeed = 5f;

    Transform target;
    NavMeshAgent navMeshAgent;
    Animator myAnimator;
    CapsuleCollider myCollider;
    float distanceToTarget = Mathf.Infinity;
    bool isProvoked = false;

    const string IDLE_TRIGGER = "idle";
    const string MOVE_TRIGGER = "move";
    const string ATTACK_BOOL = "attack";
    const string DIE_TRIGGER = "die";

    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerHealth>().transform;
        myAnimator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        myCollider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        distanceToTarget = Vector3.Distance(target.position, transform.position);
        if (isProvoked)
        {
            EngageTarget();
        }
        else if (distanceToTarget <= chaseRange)
        {
            isProvoked = true;
            //navMeshAgent.SetDestination(target.position);
        }
    }

    public void OnDamageTaken()
    {
        isProvoked = true;
    }

    private void EngageTarget()
    {
        FaceTarget();
        if (distanceToTarget >= navMeshAgent.stoppingDistance)
        {
            ChaseTarget();
        }

        if (distanceToTarget <= navMeshAgent.stoppingDistance)
        {
            AttackTarget();
        }
    }

    private void ChaseTarget()
    {
        myAnimator.SetBool(ATTACK_BOOL, false);
        myAnimator.SetTrigger(MOVE_TRIGGER);
        navMeshAgent.SetDestination(target.position);
    }

    private void AttackTarget()
    {
        myAnimator.SetBool(ATTACK_BOOL, true);
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0);
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    public void Die()
    {
        myAnimator.SetTrigger(DIE_TRIGGER);
        navMeshAgent.enabled = false;
        myCollider.enabled = false;
        this.enabled = false;
    }
}