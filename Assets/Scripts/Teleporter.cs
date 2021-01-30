using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField]
    GameObject to;
    [SerializeField] GameObject spawnObj;

    private Transform destination;
    private Transform spawn;

    private void Start()
    {
        destination = to.transform;
        spawn = spawnObj.transform;
        Debug.Log("Spawn: " + spawn);
    }

    private void OnTriggerEnter(Collider collider)
    {
        // if(other.tag == "Player")
        // {
        //     other.gameObject.transform.parent = gameObject.transform;
        //     Vector3 lp = other.gameObject.transform.localPosition;
        //     Quaternion lr = other.gameObject.transform.localRotation;
        //     other.gameObject.transform.parent = to.transform;
        //     other.gameObject.transform.localPosition = lp;
        //     other.gameObject.transform.localRotation = lr;
        //     other.gameObject.transform.parent = null;
        //
        //     // other.transform.position = new Vector3(to.transform.position.x, to.transform.position.y + other.transform.position.y, to.transform.position.z);
        // }
        
        if (collider.CompareTag("Player"))
        {
            Transform pcContainer = collider.transform.GetChild(0);
            
            collider.transform.position = new Vector3(destination.position.x, collider.transform.position.y, destination.position.z);

            float angleBetweenPortals = spawn.rotation.eulerAngles.y - destination.rotation.eulerAngles.y;
            float angleToRotate = (angleBetweenPortals < 1 && angleBetweenPortals > -1) ? 180 :
                (Math.Abs(angleBetweenPortals) > 179 && Math.Abs(angleBetweenPortals) < 181) ? 0 : angleBetweenPortals; 
            pcContainer.Rotate(0, angleToRotate, 0);
        }    
    }
}
