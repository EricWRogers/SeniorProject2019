using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameObject PlayerGO;
    public GameObject EntityGO;
    public GameObject[] RoomGOS;
    public GameObject tMax;
    public GameObject fullWaypoint = null;

    public float ScaredShitlessMeter;
    public float TimerSet;
    public bool stopTimer = false;
    public bool canDie = true;

    private int adrenaline;

    void Start()
    {
        EntityGO = GameObject.FindGameObjectWithTag("entity");
        RoomGOS = GameObject.FindGameObjectsWithTag("WayPoints");
        PlayerGO = GameObject.FindGameObjectWithTag("Player");

        adrenaline = DifficultyManager.instance.adrenaline;
    }

    void Update()
    {
        
    }

    void FixedUpdate()
    {
        PollAgression();
        PollScareShitlessMeter();
        GameTinmer();
    }

    private void PollAgression()
    {
        float maxDist = 0f;

        foreach (GameObject waypoint in RoomGOS)
        {
            if (Vector3.Distance(waypoint.transform.position, PlayerGO.transform.position) < 100.0f && Vector3.Distance(waypoint.transform.position,EntityGO.transform.position) > 100.0f)
            {
                VFXRoomManager RoomManager = waypoint.GetComponent<VFXRoomManager>();
                RoomManager.agressionMeter = RoomManager.agressionMeter + 4f * Time.deltaTime;
                if (RoomManager.agressionMeter >= 100.0f)
                {
                    if (fullWaypoint == null)
                        fullWaypoint = waypoint;
                    RoomManager.agressionMeter = 100.0f;
                }
            }

            float dist = Vector3.Distance(waypoint.transform.position, PlayerGO.transform.position);
            if (dist > maxDist)
            {
                tMax = waypoint;
                maxDist = dist;
            }
        }
        if(fullWaypoint != null)
        {
            VFXRoomManager RoomM = fullWaypoint.GetComponent<VFXRoomManager>();
            if (Vector3.Distance(fullWaypoint.transform.position, EntityGO.transform.position) < 100.0f)
            {
                if (RoomM.agressionMeter >= 0f){
                    RoomM.agressionMeter = RoomM.agressionMeter - 15f * Time.deltaTime;
                } else {
                    RoomM.agressionMeter = 0f;
                    fullWaypoint = null;
                }
            }
        }        
    }

    private void PollScareShitlessMeter()
    {
        if (Vector3.Distance(PlayerGO.transform.position, EntityGO.transform.position) < 180.0f)
        {
            ScaredShitlessMeter += 2f *Time.deltaTime;
            if (ScaredShitlessMeter > 100.0f)
            {
                Debug.Log("the Farthest Waypoint is" + tMax);
                ScaredShitlessMeter = 100.0f;
            }
        }
        if (ScaredShitlessMeter >= 100.0f)
        {
            if (Vector3.Distance(PlayerGO.transform.position, EntityGO.transform.position) > 180.0f)
            {
                if (ScaredShitlessMeter >= 0f)
                {
                    ScaredShitlessMeter = ScaredShitlessMeter - 10f * Time.deltaTime;
                }
                if(ScaredShitlessMeter < 0f)
                {
                   ScaredShitlessMeter = 0f;
                   
                }
            }
        }
    }

    public void adrenalineAttack()
    {
        switch (adrenaline)
        {
            case 0:
                GameOver();
                break;
            case 1:
                adrenaline -= 1;
                break;
            case 2:
                adrenaline -= 1;
                break;
            case 3:
                adrenaline -= 1;
                break;
        }
    }

    void GameOver()
    {

    }

    void GameTinmer()
    {
        if(!stopTimer)
        {
            TimerSet -= Time.deltaTime;
        }
        if (TimerSet <= 0.0f && !stopTimer)
        {
            stopTimer = true;
        }
    }
}