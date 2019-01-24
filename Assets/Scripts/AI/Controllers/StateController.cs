﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{

    public State currentState;
    public EntityStats enemyStats;
    public Transform eyes;
    public State remainState;
    public Transform chaseTarget;
    public List<Transform> wayPoints;
    public GameManager gameManager;
    public int nextWayPoint;
  

    [HideInInspector] public NavMeshAgent navMeshAgent;
 
    [HideInInspector] public float stateTimeElapsed;

    private bool aiActive;


    void Awake()
    {
        
        navMeshAgent = GetComponent<NavMeshAgent>();
        
    }
    private void Start()
    {
        SetupAI();
    }

    public void SetupAI()
    {
        wayPoints = gameManager.Waypoints;
        aiActive = true;

        if (aiActive)
        {
            navMeshAgent.enabled = true;
        }
        else
        {
            navMeshAgent.enabled = false;
        }
    }

    void Update()
    {
        if (!aiActive)
            return;
        currentState.UpdateState(this);
    }

    void OnDrawGizmos()
    {
        if (currentState != null && eyes != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(eyes.position, enemyStats.lookSphereCastRadius);
        }
    }
    public void TransitionToState(State nextState)
    {
        if (nextState != remainState)
        {
            currentState = nextState;
            OnExitState();
        }
    }

    public bool CheckIfCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

    private void OnExitState()
    {
        stateTimeElapsed = 0;
    }
}
