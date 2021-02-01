using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePuzzleController : MonoBehaviour
{
    private int[] doneCounter = { 0, 0, 0, 0 };
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
    public PuzzleSide NextMaterial(PuzzleSide curr)
    {
        if (!done)
        {
            switch (curr)
            {
                case PuzzleSide.orange:
                    return PuzzleSide.yellow;
                case PuzzleSide.yellow:
                    return PuzzleSide.white;
                case PuzzleSide.white:
                    return PuzzleSide.blue;
                case PuzzleSide.blue:
                    return PuzzleSide.red;
                case PuzzleSide.red:
                    return PuzzleSide.green;
                case PuzzleSide.green:
                    return PuzzleSide.orange;
                default:
                    return PuzzleSide.orange;
            }
        }
        return curr;
    }

    public Color getColor(PuzzleSide color)
    {
        switch (color)
        {
            case PuzzleSide.orange:
                return orange;
            case PuzzleSide.yellow:
                return yellow;
            case PuzzleSide.white:
                return white;
            case PuzzleSide.blue:
                return blue;
            case PuzzleSide.red:
                return red;
            case PuzzleSide.green:
                return green;
            default:
                return orange;
        }
    }

    public void add(int index)
    {
        doneCounter[index]++;
        print("Curr counter: " + doneCounter.ToString());
        if(doneCounter[index] == 9)
        {
            done = true;
            GameEvents.current.OnPhaseChanged(3);
        }
    }

    public void remove(int index)
    {
        if (!done)
        {
            doneCounter[index]--;
            print("Curr counter: " + doneCounter.ToString());
        }
    }
}
