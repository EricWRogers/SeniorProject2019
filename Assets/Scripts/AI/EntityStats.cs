using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (menuName ="PluggableAI/EntityStats")]
public class EntityStats : ScriptableObject
{
    public float moveSpeed;
    public float lookRange;
    public float wanderRadius;

    public float minPlayerDetectDist;

    //attack variables
    public int attackRange;
    public float attackRate;
    public float attackPower;

    public float wanderDuration;
    public float searchDuration;
    public float searchingTurnSpeed;
    

 
}
