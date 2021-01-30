using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    [SerializeField] private LayerMask IgnoreMe;
    [SerializeField] private string PickUpTag = "pickUp";
    [SerializeField] private GameObject PickUpIndicator;
    [SerializeField] private float throwForce = 7f;

    [Header("Keys")]
    [SerializeField] private KeyCode pickUpKey = KeyCode.Mouse1;
    [SerializeField] private KeyCode throwItemKey = KeyCode.Mouse0;
    [SerializeField] private KeyCode dropKey = KeyCode.E;

    private static bool pickedUp = false;
    private Transform selection;
    private Transform selected;

    void Update()
    {
        RaycastHit hit;
        PickUpIndicator.SetActive(false);

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 50f, ~IgnoreMe))
        {
            selection = hit.transform;

            //if the item is a pick up item
            if (selection.CompareTag(PickUpTag) && !pickedUp && hit.distance < 5f)
            {
                PickUpIndicator.SetActive(true);
                PickUpIndicator.transform.position = hit.point;
                Debug.Log("I see");
         
                if (Input.GetKey(pickUpKey))
                {
                    selected = selection;
                    pickedUp = true;

                    //make object the child of the player
                    selected.parent = gameObject.transform;
                    selected.localPosition = new Vector3(0, -1, 2);
                    selected.localRotation = Quaternion.identity;

                    //stop its physics so it will stop jiggling around
                    if (selected.gameObject.GetComponent<Rigidbody>() != null)
                    {
                        selected.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                        selected.gameObject.GetComponent<MeshCollider>().enabled = false;
                    }
                }
            }
        }

            //drop object that is picked up                       
            if (Input.GetKey(dropKey) && pickedUp)
            {
                pickedUp = false;
                selected.parent = null;

                if (selected.gameObject.GetComponent<Rigidbody>() != null)
                {
                    selected.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    selected.gameObject.GetComponent<MeshCollider>().enabled = true;
                }
                else
                {
                    selected.gameObject.AddComponent<Rigidbody>();
                }
            }

            //yeet that object
            else if (Input.GetKey(throwItemKey) && pickedUp)
            {
                pickedUp = false;
                selected.parent = null;

                if (selected.gameObject.GetComponent<Rigidbody>() != null)
                {
                    selected.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    selected.gameObject.GetComponent<MeshCollider>().enabled = true;
                    selected.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);

                }
                else
                {
                    selected.gameObject.AddComponent<Rigidbody>();
                    selected.gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * throwForce, ForceMode.Impulse);
                }
            }

            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        }
    }

