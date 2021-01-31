using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePuzzle : MonoBehaviour
{

    [SerializeField]
    string currentMat, targetMat;
    private Renderer currRenderer;
    private void Start()
    {
        currRenderer = GetComponent<Renderer>();
        CubePuzzleController ctrl = GetComponentInParent<CubePuzzleController>();
        currRenderer.material.color = ctrl.getColor(currentMat);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            CubePuzzleController ctrl = GetComponentInParent<CubePuzzleController>();
            string nextMat = ctrl.NextMaterial(currentMat);
            if (currentMat == targetMat) ctrl.remove();
            else if (nextMat == targetMat) ctrl.add();
            currRenderer.material.color = ctrl.getColor(nextMat);
            currentMat = nextMat;
        }
    }
}
