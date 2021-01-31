using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuHandler : MonoBehaviour
{
    public bool triggerLevel = false;
    public bool triggerMenu = false;

    private void Update()
    {
        if (triggerLevel)
            startGame();

        if (triggerMenu)
            startMenu();
    }

    public void startGame() => SceneManager.LoadScene(1);

    public void startMenu() => SceneManager.LoadScene(0);

    public void startButton() => GetComponent<Animator>().Play("menu_fade");
}
