using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public bool triggerLevel = false;

    private void Update()
    {
        if (triggerLevel)
            startGame();
    }

    public void startGame() => SceneManager.LoadScene(1);

    public void startButton() => GetComponent<Animator>().Play("menu_fade");
}
