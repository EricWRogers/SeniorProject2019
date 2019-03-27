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
    public bool testWakeUp;
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

        if(DifficultyManager.instance != null)
        {
            adrenaline = DifficultyManager.instance.adrenaline;
        }
        
        playerAttacked = false;
        wakeUp = false;
        testWakeUp = false;
    }

    void Update()
    {
        if(testWakeUp)
        {
            WakeUp();
        }
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
        //Debug.Log("Game Over!!!");
    }

    void Attacked()
    {
        //do audio stuff heart yada
        //when entity goes to farthest waypoint set wakeup to true then wake up will happen this will happen in
        //entitys script not here 
    Debug.Log("stuff should be happening");
        playerAttacked = true;

        if(playerAttacked)
        PlayerGO.layer = 9;
        else
        PlayerGO.layer = 10;

        adrenaline--;
        HudGO.SetActive(false);
        PlayerGO.GetComponent<PlayerMovement>().stopMoving = true;
        PlayerGO.GetComponentInChildren<CamLooking>().enabled = false;

        if (blackImage != null)
        {
            blackImage.CrossFadeAlpha(255f, 0.2f, false);
        }
    }

    void WakeUp()
    {
        if(wakeUp)
        {
            HudGO.SetActive(true);
            PlayerGO.GetComponent<PlayerMovement>().stopMoving = false;
            PlayerGO.GetComponentInChildren<CamLooking>().enabled = true;

            if (blackImage != null)
            {
                blackImage.CrossFadeAlpha(1f, 0.2f, false);
            }
        }

        //For Testing Only
        {
            if (!wakeUp)
            {
                HudGO.SetActive(false);
                PlayerGO.GetComponent<PlayerMovement>().stopMoving = true;
                PlayerGO.GetComponentInChildren<CamLooking>().enabled = false;

                if (blackImage != null)
                {
                    blackImage.CrossFadeAlpha(255f, 0.2f, false);
                }
            }
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