using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStates : MonoBehaviour
{
    public static GameStates current;

    //How much the high decays?
    [SerializeField] private float highDecay = .01f;
    //How often the high decays
    [SerializeField] private float decayDelay = 1f;

    private float timer = 0f;

    //Clamp high value to [0, 1]
    private float _high;
    public float High
    {
        get
        {
            return _high;
        }
        
        set
        {
            _high = value < 0 ? 0 : value > 1 ? 1 : value;
            Debug.Log("High: " + High);
        }
    }

    private void Awake()
    {
        current = this;
    }

    private void Start()
    {
        High = 0;
        GameEvents.current.onPillPicked += onPillPicked;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        //High should constantly decay
        if (timer > decayDelay)
        {
            timer = 0;
            High -= highDecay;
        }
    }

    private void onPillPicked(float highValue)
    {
        High += highValue;
    }
}
