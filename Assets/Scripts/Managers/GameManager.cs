using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public GameObject PlayerGO;
    public GameObject[] RoomGOS;

    //AI STUFF!!
    public List<Transform> Waypoints = new List<Transform>();
    

    // Start is called before the first frame update
    void Start(){
        RoomGOS = GameObject.FindGameObjectsWithTag("WayPoints");
        PlayerGO = GameObject.FindGameObjectWithTag("Player");
        GetWaypoints();
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

    //AI STUFF!!

    public void GetWaypoints()
    {
       Transform[] wpList = transform.GetComponentsInChildren<Transform>();

        for(int i = 0; i < wpList.Length; i++)
        {
            if(wpList[i].tag == "WayPoints")
            {
                Waypoints.Add(wpList[i]);
            }
        }
        
    }

}
