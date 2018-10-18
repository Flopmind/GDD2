using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsButton : Button
{
    public override void OnMouseDown()
    {
        Debug.Log("Switch to credits scene");
    }
}
