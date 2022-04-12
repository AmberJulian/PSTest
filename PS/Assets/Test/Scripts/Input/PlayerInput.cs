using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : Singleton<PlayerInput>
{
    public delegate void ThrustKeyPressed();
    public ThrustKeyPressed ThrustKeyEvent;


    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
         {
            if (ThrustKeyEvent != null)
            ThrustKeyEvent.Invoke();
        }
    }
}
