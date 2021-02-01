using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePuzzle : MonoBehaviour
{
    [SerializeField]
    PuzzleSide currentMat;
    [SerializeField]
    PuzzleSide[] targetMat;
    private Renderer currRenderer;

    private IEnumerator Start()
    {
        yield return new WaitForEndOfFrame();
        currRenderer = GetComponent<Renderer>();
        CubePuzzleController ctrl = GetComponentInParent<CubePuzzleController>();
        currRenderer.material.color = ctrl.getColor(currentMat);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            CubePuzzleController ctrl = GetComponentInParent<CubePuzzleController>();
            PuzzleSide nextMat = ctrl.NextMaterial(currentMat);
            for (int i = 0; i < targetMat.Length; i++) {
                if (currentMat == targetMat[i]) ctrl.remove(i);
                else if (nextMat == targetMat[i]) ctrl.add(i);
            }
            currRenderer.material.color = ctrl.getColor(nextMat);
            currentMat = nextMat;
        }
    }
}
