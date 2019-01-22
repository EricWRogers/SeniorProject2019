using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject PlayerGO;
    public GameObject[] RoomGOS;
    // Start is called before the first frame update
    void Start(){
        RoomGOS = GameObject.FindGameObjectsWithTag("WayPoints");
        PlayerGO = GameObject.FindGameObjectWithTag("Player");
    }
    void Update(){}
    void FixedUpdate()
    {

    }
    void PollAgression()
    {
        foreach(GameObject waypoint in RoomGOS)
        {
            if ( Vector3.Distance(waypoint.transform.position, PlayerGO.transform.position) < 5.0f )
            {
                VFXRoomManager RoomManager = waypoint.GetComponent<VFXRoomManager>();
                RoomManager.agressionMeter++;
                if( RoomManager.agressionMeter > 100.0f) { RoomManager.agressionMeter = 100.0f; }
            }
        }
    }
}
