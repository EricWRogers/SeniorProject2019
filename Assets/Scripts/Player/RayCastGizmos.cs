using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastGizmos : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 directionLeft = transform.TransformDirection(Vector3.left) * 6;
        Gizmos.DrawRay(transform.position, directionLeft);

        Gizmos.color = Color.red;
        Vector3 directionRight = transform.TransformDirection(Vector3.right) * 6;
        Gizmos.DrawRay(transform.position, directionRight);

        Gizmos.color = Color.blue;
        Vector3 directionUp = transform.up * 10;
        Gizmos.DrawRay(transform.position, directionUp);
    }
}
