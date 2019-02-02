using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFlocking : MonoBehaviour
{ 
    public float speed = .001f;
    float rotationSpeed = 4.0f;
    Vector3 averageHeading;
    Vector3 averagePosition;
    float NeighbourDistance = 2.0f;
    bool turning = false;

    void Start()
    {
        speed = Random.Range(0.5f, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, Vector3.zero) >= FishAI.tankSize)
        {
            turning = true;
        }
        else
        {
            turning = false;
        }

        if(turning)
        {
            Vector3 direction = Vector3.zero - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
            speed = Random.Range(0.5f, 1);
        }

       else
        { 
            if (Random.Range(0, 5) < 1)
                ApplyRules();
        }
            transform.Translate(0, 0, Time.deltaTime * speed);
        
    }

    void ApplyRules()
    {
        GameObject[] gos;
        gos = FishAI.allFish;

        Vector3 vCenter = Vector3.zero;
        Vector3 vAvoid = Vector3.zero;
        float gSpeed = .1f;

        Vector3 goalPos = FishAI.goalPos;

        float dist;
        int groupSize = 0;
        foreach(GameObject go in gos)
        {
            if(go != this.gameObject)
            {
                dist = Vector3.Distance(go.transform.position, this.transform.position);
                if(dist <= NeighbourDistance)
                {
                    vCenter += go.transform.position;
                    groupSize++;

                    if (dist < 1.0f)
                    {
                        vAvoid = vAvoid + (this.transform.position - go.transform.position);
                    }
                    FishFlocking anotherFLock = go.GetComponent<FishFlocking>();
                    gSpeed = gSpeed + anotherFLock.speed;
                }
            }

        }
        if(groupSize > 0)
        {
            vCenter = vCenter / groupSize + (goalPos - this.transform.position);
            speed = gSpeed / groupSize;

            Vector3 direction = (vCenter + vAvoid) - transform.position;
            if(direction != Vector3.zero)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),rotationSpeed * Time.deltaTime);
            }
        }
        
    }

}