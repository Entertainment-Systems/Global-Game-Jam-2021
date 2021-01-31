using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleObjectOnTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] Enables;
    [SerializeField] GameObject[] Disables;
    [SerializeField] string triggerName;

    private void OnTriggerEnter(Collider other)
    {
        print("we in somethin");
        if (other.name == triggerName) {
            foreach (GameObject x in Enables)
                x.SetActive(true);

            foreach (GameObject x in Disables)
                x.SetActive(false);

            print("we in da boiiii");
            Destroy(this);
        }
    }
}
