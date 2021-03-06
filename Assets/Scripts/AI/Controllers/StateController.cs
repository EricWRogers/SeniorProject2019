﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{
    public static ThrowObject _throwObject;

    public State currentState;
    public EntityStats enemyStats;
    public Transform eyes;
    public State remainState;
    public int nextWayPoint;
    public GameManager gameManager;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public float stateTimeElapsed;
    private bool aiActive;
    public float normalSpeed;
    public float runspeed;
    //fov 
    public LayerMask playerTargetMask;
    public LayerMask ObstacleMask;
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    
    public Animator anim;
    public DifficultyManager diffMan;
    public Vector3 lastKnownPosition;

    public ThrowObject throwableObject {
        get {
            return StateController._throwObject;
        }

        set {
            StateController._throwObject = value;
        }
    }  

    void Awake()
    {
        
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        
    }
    private void Start()
    {
        SetupAI();
        Rigidbody entityRb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        normalSpeed = DifficultyManager.instance.EntityWalkSpeed;
        runspeed = DifficultyManager.instance.EntityRunSpeed;
    }
    void FixedUpdate()
    {
        
        
    }

    public void SetupAI()
    {
       
        aiActive = true;

        if (aiActive)
        {
            navMeshAgent.enabled = true;
        }
        else
        {
            navMeshAgent.enabled = false;
        }

        currentState.OnStateEnter(this);
    }

    void Update()
    {
        if (!aiActive)
            return;
        currentState.UpdateState(this);

        Vector3 vel =navMeshAgent.velocity;
        if(vel.magnitude >= 1 && vel.magnitude  < runspeed)
        {
            anim.SetBool("isIdle",false);
            anim.SetBool("isWalking",true);
            anim.SetBool("isRunning",false);
        }
        if(vel.magnitude >= runspeed)
        {
            anim.SetBool("isIdle",false);
            anim.SetBool("isWalking",false);
            anim.SetBool("isRunning",true);
        }
        if(vel.magnitude  <= 0)
        {
            anim.SetBool("isIdle",true);
            anim.SetBool("isWalking",false);
            anim.SetBool("isRunning",false);
        }
    }

    void OnDrawGizmos()
    {
       
        
        if (currentState != null && eyes != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(transform.position, viewRadius);
            Vector3 viewAngleA = DirFromAngle(-viewAngle / 2, false);
            Vector3 viewAngleB = DirFromAngle(viewAngle / 2, false);

            Gizmos.DrawLine(transform.position, transform.position + viewAngleA * viewRadius);
            Gizmos.DrawLine(transform.position, transform.position + viewAngleB * viewRadius);
            Gizmos.DrawLine(transform.position, transform.position + viewAngleA * viewRadius * -1);
            Gizmos.DrawLine(transform.position, transform.position + viewAngleB * viewRadius * -1);

        }
    }
    public void TransitionToState(State nextState)
    {
        if (nextState != remainState)
        {
            OnExitState(currentState);
            currentState = nextState;
            currentState.OnStateEnter(this);
        }
    }

    public bool CheckIfCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGloabal)
    {
        if(!angleIsGloabal)
        {
            angleInDegrees += eyes.transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    private void OnExitState(State exitedState)
    {
        stateTimeElapsed = 0;
        exitedState.OnStateExit(this);
    }
}
