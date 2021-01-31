using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePuzzleController : MonoBehaviour
{
    private int doneCounter = 0;
    private Color orange, yellow, white, blue, red, green;
    private bool done = false;
    private void Start()
    {
        orange = new Color(1, 0.65f, 0);
        yellow = Color.yellow;
        white = Color.white;
        blue = Color.blue;
        red = Color.red;
        green = Color.green;
    }
    public string NextMaterial(string curr)
    {
        if (!done)
        {
            switch (curr)
            {
                case "Orange":
                    return "Yellow";
                case "Yellow":
                    return "White";
                case "White":
                    return "Blue";
                case "Blue":
                    return "Red";
                case "Red":
                    return "Green";
                case "Green":
                    return "Orange";
                default:
                    return "Orange";
            }
        }
        return curr;
    }

    public Color getColor(string color)
    {
        switch (color)
        {
            case "Orange":
                return orange;
            case "Yellow":
                return yellow;
            case "White":
                return white;
            case "Blue":
                return blue;
            case "Red":
                return red;
            case "Green":
                return green;
            default:
                return orange;
        }
    }

    public void add()
    {
        doneCounter++;
        print("Curr counter: " + doneCounter);
        if(doneCounter==9)
        {
            done = true;
            GameEvents.current.OnPhaseChanged(3);
        }
    }

    public void remove()
    {
        if (!done)
        {
            doneCounter--;
            print("Curr counter: " + doneCounter);
        }
    }
}
