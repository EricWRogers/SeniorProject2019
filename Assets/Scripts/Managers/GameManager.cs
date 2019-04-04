using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    public GameObject PlayerGO;
    public GameObject EntityGO;
    public GameObject HudGO;
    public GameObject[] RoomGOS;
    public GameObject tMax;
    public GameObject fullWaypoint = null;

    public float ScaredShitlessMeter;
    public float TimerSet;
    public float imageScreenTime = 2.5f;
    public float emptyScreenTime = 2.5f;
    public bool stopTimer = false;
    
    public int adrenaline;
    public int explosives;
    RaycastHit hit;
    public bool hidden;
    public bool playerAttacked;


    void Start()
    {
       
        EntityGO = GameObject.FindGameObjectWithTag("entity");
        RoomGOS = GameObject.FindGameObjectsWithTag("WayPoints");
        PlayerGO = GameObject.FindGameObjectWithTag("Player");
        HudGO = GameObject.Find("HUD");
        hidden = false;
        playerAttacked = false;

        if (DifficultyManager.instance != null)
        {
            adrenaline = DifficultyManager.instance.adrenaline;
        }
        else
        {
            adrenaline = 3;
            explosives = 3;
        }
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        PollAgression();
        PollScareShitlessMeter();
    }

    void PollAgression()
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
                if (RoomM.agressionMeter >= 0f)
                {
                    RoomM.agressionMeter = RoomM.agressionMeter - 15f * Time.deltaTime;
                } else {
                    RoomM.agressionMeter = 0f;
                    fullWaypoint = null;
                }
            }
        }        
    }

    void PollScareShitlessMeter()
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

    void Attacked()
    {
        //do audio stuff heart yada
        //when entity goes to farthest waypoint set wakeup to true then wake up will happen this will happen in
        //entitys script not here 
        adrenaline--;
        playerAttacked = true;
        HudGO.SetActive(false);
        PlayerGO.GetComponent<PlayerMovement>().stopMoving = true;
        PlayerGO.GetComponentInChildren<CamLooking>().enabled = false;
        PlayerGO.transform.Find("Sanity Whispers").gameObject.GetComponent<AudioSource>().Pause();
        GameObject.Find("BlackOut Canvas").transform.Find("Black Image").gameObject.GetComponent<Image>().CrossFadeAlpha(255f, 0.2f, false);
    }
     public void CheckIfHidden()
    {
        if (Physics.Raycast(PlayerGO.transform.position, PlayerGO.transform.up, out hit, 15f))
        {
            if(hit.collider.tag == "HideableObjects")
            {
                hidden = true;
                Debug.Log(hit.collider.name);
            }
        }
        else
        {
            //hidden = false;
        }
        
        if( hidden || playerAttacked)
        {
            gameObject.layer = LayerMask.NameToLayer("obstacles");
            
        }
        else
        {

            gameObject.layer = LayerMask.NameToLayer("target");
        }
    }

    void CountDownTimer()
    {
        if(TimerSet >= 0 && !stopTimer)
        {
            TimerSet -= Time.deltaTime;
        }
        else if(TimerSet <= 0)
        {
            //Do What???????
            //GameOver() ?
        }
    }

    void GameOver()
    {

    }

    //Call this funichion when you want to WakeUp
    public void WakeUp()
    {
        playerAttacked = false;
        HudGO.SetActive(true);
        PlayerGO.GetComponent<PlayerMovement>().stopMoving = false;
        PlayerGO.GetComponentInChildren<CamLooking>().enabled = true;
        PlayerGO.transform.Find("Sanity Whispers").gameObject.GetComponent<AudioSource>().Play();
        GameObject.Find("BlackOut Canvas").transform.Find("Black Image").gameObject.GetComponent<Image>().CrossFadeAlpha(1f, 0.2f, false);
    }

    public void adrenalineAttacked()
    {
        if (adrenaline != 0)
        {
            Attacked();
        }
        else
        {
            GameOver();
        }
    }

    public void Win()
    {
        //DO
    }
}