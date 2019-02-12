using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameObject PlayerGO;
    public GameObject EntityGO;
    public GameObject[] RoomGOS;

    public float ScaredShitlessMeter;
    


    // Start is called before the first frame update
    void Start(){
        EntityGO = GameObject.FindGameObjectWithTag("entity");
        RoomGOS = GameObject.FindGameObjectsWithTag("WayPoints");
        PlayerGO = GameObject.FindGameObjectWithTag("Player");
        
    }
    void Update() { }
    void FixedUpdate()
    {
        PollAgression();
        PollScareShitlessMeter();
    }

    private void PollAgression()
    {
        foreach(GameObject waypoint in RoomGOS)
        {
            if ( Vector3.Distance(waypoint.transform.position, PlayerGO.transform.position) < 10.0f )
            {
                VFXRoomManager RoomManager = waypoint.GetComponent<VFXRoomManager>();
                RoomManager.agressionMeter =  RoomManager.agressionMeter + 0.1f;
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
            ScaredShitlessMeter += .5f;

            if (ScaredShitlessMeter > 100.0f) { ScaredShitlessMeter = 100.0f; }
        }
    }
}
