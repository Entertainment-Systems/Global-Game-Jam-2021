using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardTower : MonoBehaviour
{
    [SerializeField]
    private GameObject parent;

    public void collapseCards()
    {
        Rigidbody[] RBArray = parent.GetComponentsInChildren<Rigidbody>();
        foreach(Rigidbody rb in RBArray)
        {
            rb.isKinematic = false;
        }
        GetComponent<Rigidbody>().isKinematic = true;
    }

}
