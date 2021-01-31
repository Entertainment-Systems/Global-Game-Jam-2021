using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutroHandler : MonoBehaviour
{
    [SerializeField] AudioClip knock;
    [SerializeField] Animator door;
    [SerializeField] Animator canvas;
    [SerializeField] GameObject man;
    AudioSource src, jakob;

    // Start is called before the first frame update
    void OnEnable()
    {
        src = GetComponent<AudioSource>();
        StartCoroutine(endGame());
    }


    IEnumerator endGame()
    {
        door.enabled = true;
        yield return new WaitForSecondsRealtime(1);
        src.PlayOneShot(knock);
        yield return new WaitForSecondsRealtime(2);
        src.PlayOneShot(knock);
        yield return new WaitForSecondsRealtime(2);
        src.PlayOneShot(knock);
        yield return new WaitForSecondsRealtime(2);
        src.PlayOneShot(knock);
        yield return new WaitForSecondsRealtime(2);

        door.Play("DoorOpen");
        man.SetActive(true);

        yield return new WaitForSecondsRealtime(2);
        canvas.Play("Outro");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
