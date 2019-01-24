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
    void Update() { }
    void FixedUpdate()
    {
        PollAgression();
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

 

}
