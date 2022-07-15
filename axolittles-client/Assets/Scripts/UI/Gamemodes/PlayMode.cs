using System;
using UnityEngine;

public class PlayMode : GamemodeBase
{
    private void OnEnable()
    {
        SetCursor();
    }

    void Update()
    {
        MouseInput();
    }

  
}