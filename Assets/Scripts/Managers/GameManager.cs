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
        foreach(GameObject waypoint in RoomGOS)
        {
            if ( Vector3.Distance(waypoint.transform.position, PlayerGO.transform.position) < 10.0f )
            {
                VFXRoomManager RoomManager = waypoint.GetComponent<VFXRoomManager>();
                RoomManager.agressionMeter =  RoomManager.agressionMeter + 0.5f * Time.deltaTime;
                if( RoomManager.agressionMeter > 100.0f) { RoomManager.agressionMeter = 100.0f; }
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
        if (Vector3.Distance(PlayerGO.transform.position, EntityGO.transform.position) < 10.0f)
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