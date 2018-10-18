using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Button : MonoBehaviour {
    private Material mat;
    public Color color;
	// Use this for initialization
	void Start () {
        mat = new Material(Shader.Find("Standard"));
        mat.color = color;
        gameObject.GetComponent<Renderer>().material = mat;
        TextMesh childText = GetComponentInChildren<TextMesh>();
	}
	
	// Update is called once per frame
	void Update () {
        mat.color = color;
	}

    public abstract void OnMouseDown();
}
