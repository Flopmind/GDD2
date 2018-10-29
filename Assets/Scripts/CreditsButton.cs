using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    private void OnMouseDown()
    {
        SceneManager.LoadScene("ControlsScene");
    }
}
