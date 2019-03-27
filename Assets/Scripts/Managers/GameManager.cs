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
    public GameObject PauseUIGO;
    public GameObject[] RoomGOS;
    public GameObject tMax;
    public GameObject fullWaypoint = null;

    public float ScaredShitlessMeter;
    public float TimerSet;
    public float imageScreenTime = 2.5f;
    public float emptyScreenTime = 2.5f;
    public bool stopTimer = false;
    public bool playerAttacked;
    public bool wakeUp;
    public int adrenaline;

    private Image blackImage;

    void Start()
    {
        EntityGO = GameObject.FindGameObjectWithTag("entity");
        RoomGOS = GameObject.FindGameObjectsWithTag("WayPoints");
        PlayerGO = GameObject.FindGameObjectWithTag("Player");
        HudGO = HudGO = GameObject.Find("HUD");
        PauseUIGO = GameObject.Find("Pause UI");
        blackImage = GameObject.Find("BlackOut Canvas/Black Image").GetComponent<Image>();
        adrenaline = DifficultyManager.instance.adrenaline;

        Debug.Log(adrenaline);
        playerAttacked = false;
        wakeUp = false;
    }

    void Update()
    {

    }

    void FixedUpdate()
    {
        PollAgression();
        PollScareShitlessMeter();
        //GameTinmer();
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

    public void adrenalineAttacked()
    {
       if(adrenaline!=0)
       {
           Attacked();
       }
       else
       {
          GameOver();
       }
    }

    void GameOver()
    {
        Debug.Log("Game Over!!!");
    }

    void Attacked()
    {
        playerAttacked = true;
        adrenaline--;
        HudGO.SetActive(false);

        if (blackImage != null)
        {
            blackImage.CrossFadeAlpha(255f, 0.2f, false);
        }

        //
        //

        //do audio stuff heart yada
        //do not let blink up till wake up is true
        //when entity goes to farthest waypoint set wakeup to true then wake up will happen this will happen in
        //entitys script not here 
    }

    void WakeUp()
    {
        if(wakeUp)
        {
            //wake up will be set true by entity

            if (blackImage != null)
            {
                blackImage.CrossFadeAlpha(1f, 0.2f, false);
            }
            
            HudGO.SetActive(true);
        }
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