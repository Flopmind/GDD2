﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnGUI()
    {
        Rect area = Screen.safeArea;
        area.height *= 0.75f;
        GUIStyle style = GUI.skin.box;
        style.fontSize = 50;
        GUI.Box(area, "Controls go here\r\nDon't forget to do controls", style);
    }
}
