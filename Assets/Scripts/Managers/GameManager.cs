using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameObject PlayerGO;
    public GameObject EntityGO;
    public GameObject[] RoomGOS;

    public float ScaredShitlessMeter;
    public float TimerSet;
    public bool stopTimer = false;
    public bool canDie = true;

    public GameObject fullWaypoint = null;
    

    // Start is called before the first frame update
    void Start()
    {
        EntityGO = GameObject.FindGameObjectWithTag("entity");
        RoomGOS = GameObject.FindGameObjectsWithTag("WayPoints");
        PlayerGO = GameObject.FindGameObjectWithTag("Player");
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
        foreach (GameObject waypoint in RoomGOS)
        {
            if (Vector3.Distance(waypoint.transform.position, PlayerGO.transform.position) < 100.0f)
            {
                VFXRoomManager RoomManager = waypoint.GetComponent<VFXRoomManager>();
                RoomManager.agressionMeter = RoomManager.agressionMeter + 10f * Time.deltaTime;
                if (RoomManager.agressionMeter >= 100.0f)
                {
                    if (fullWaypoint == null)
                        fullWaypoint = waypoint;
                    //eric subtract to 0 then set to null!
                    RoomManager.agressionMeter = 100.0f;
                }
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

    public void FarthestWaypointFromPlayer(Transform[] RoomGOS)
    {
        Transform tMax = null;
        float maxDist = Mathf.Infinity;
        Vector3 playersPosition = PlayerGO.transform.position;

        foreach(Transform wp in RoomGOS)
        {
            float dist = Vector3.Distance(wp.position, playersPosition);
            if(dist > maxDist)
            {
                tMax = wp;
                maxDist = dist;
            }

        }
    }

    private void PollScareShitlessMeter()
    {
        if (Vector3.Distance(PlayerGO.transform.position, EntityGO.transform.position) < 50.0f)
        {
            ScaredShitlessMeter += .7f *Time.deltaTime;
            if (ScaredShitlessMeter > 100.0f)
            {
                ScaredShitlessMeter = 100.0f;
            }
        }
    }

    private void GameTinmer()
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