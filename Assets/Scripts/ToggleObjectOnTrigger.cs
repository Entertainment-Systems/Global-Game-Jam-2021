using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObjectOnTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] Enables;
    [SerializeField] GameObject[] Disables;
    [SerializeField] GameObject trigger;
    [SerializeField] bool destroySelf = false;

    private void OnTriggerEnter(Collider other)
    {
        print("we in somethin");
        if (other.name == trigger.name)
        {
            foreach (GameObject x in Enables)
                x.SetActive(true);

            foreach (GameObject x in Disables)
                x.SetActive(false);

            if (destroySelf)
            {
                print("kill the child");
                Destroy(this.gameObject);
            }

        }
    }
}
