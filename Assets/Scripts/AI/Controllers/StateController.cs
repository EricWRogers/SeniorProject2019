using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour
{

    public State currentState;
    public EntityStats enemyStats;
    public Transform eyes;
    public State remainState;
    public int nextWayPoint;
    public GameManager gameManager;
    public PlayerMovement player;
    [HideInInspector] public NavMeshAgent navMeshAgent;
    [HideInInspector] public float stateTimeElapsed;
    private bool aiActive;

    //fov 
    public LayerMask playerTargetMask;
    public LayerMask ObstacleMask;
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;




    void Awake()
    {
        
        navMeshAgent = GetComponent<NavMeshAgent>();
        
    }
    private void Start()
    {
        SetupAI();
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
