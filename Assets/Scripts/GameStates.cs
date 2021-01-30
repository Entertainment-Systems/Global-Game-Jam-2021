using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStates : MonoBehaviour
{
    public bool debug = false;
    
    public static GameStates current;

    //How much the high decays?
    [SerializeField] private float highDecay = .01f;
    //How often the high decays
    [SerializeField] private float changeSpeed = 3f;

    private float timer = 0f;

    //Clamp high value to [0, 1]
    private float _high;
    private float _targetHigh;
    public float High
    {
        get
        {
            return _high;
        }
        
        set
        {
            _targetHigh = value < 0 ? 0 : value > 1 ? 1 : value;
            if(debug)
                Debug.Log("Target high: " + _targetHigh + ", " + "Current high: " + _high);
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
        //If the difference between target and current hight is bigger than tolerance, update current high
        if (Math.Abs(High - _targetHigh) > .01f)
        {
            High += _targetHigh * changeSpeed * Time.deltaTime;
        }
        
        //Target decays decay val per second
        _targetHigh -= Time.deltaTime * highDecay;
    }

    private void onPillPicked(float highValue)
    {
        High += highValue;
    }
}
