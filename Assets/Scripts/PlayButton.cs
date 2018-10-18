using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayButton : Button
{
    public override void OnMouseDown()
    {
        Debug.Log("Gonna switch the scene now...");
    }
}
