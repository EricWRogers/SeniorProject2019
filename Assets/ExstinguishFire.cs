using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExstinguishFire : MonoBehaviour
{
    public ParticleSystem part;
    public List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        part = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        Debug.Log("Impact");
        int numCollisionEvents = part.GetCollisionEvents(other, collisionEvents);
        int i = 0;

        while (i < numCollisionEvents)
        {
            if (other.gameObject.CompareTag ( "Fire"))
            {
                other.SetActive(false);
            }
            i++;
        }
    }
}
