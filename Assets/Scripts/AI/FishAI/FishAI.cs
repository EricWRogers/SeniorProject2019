using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAI : MonoBehaviour
{
    public GameObject fish;
    public static int tankSize = 5;
   // public GameObject goalPre;
    public static int numFish = 10;
    public static GameObject[] allFish = new GameObject[numFish];

    public static Vector3 goalPos = Vector3.zero;

    void Start()
    {
       for(int i = 0; i < numFish; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-tankSize, tankSize), Random.Range(-tankSize, tankSize), Random.Range(-tankSize, tankSize));
            allFish[i] = (GameObject)Instantiate(fish, pos, Quaternion.identity);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if(Random.Range(0,1000) < 50)
        {
            goalPos = new Vector3(Random.Range(-tankSize, tankSize), Random.Range(-tankSize, tankSize), Random.Range(-tankSize, tankSize));
            //goalPre.transform.position = goalPos;
        }
    }










}
