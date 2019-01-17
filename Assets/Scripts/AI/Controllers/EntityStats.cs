using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu (menuName ="PluggableAI/EntityStats")]
public class EntityStats : ScriptableObject
{
    public float moveSpeed = 10;
    public float lookRange = 40f;
    public float lookSphereCastRadius = 1f;
    public float wanderRadius = 9f;

    //attack variables
    public int attackRange = 5;
    public float attackRate = 2f;

    public float wanderDuration = 20f;
    public float searchDuration = 4f;
    public float searchingTurnSpeed = 2f;




}
