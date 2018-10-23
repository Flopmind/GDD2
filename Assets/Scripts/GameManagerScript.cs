using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    public GameObject[] hazardPrefabs;
    public GameObject player;
    public float interval;
    public int lives; // if we want to implement this later

    //private List<GameObject> activeHazards;
    private float timer;
    private float score;
    private bool odd;

	// Use this for initialization
	void Start ()
    {
        timer = 0;
        odd = true;
        if (interval <= 0)
        {
            throw new System.Exception("Something cannot every at or less than every 0 seconds. Change interval in the GameManager Prefab");
        }
	}

   

    //writing score on screen
    private void OnGUI()
    {
        //type casting score to int
        int scoreInt = (int)score;

        if (player)
        {
            //score display
            GUI.Box(new Rect(10, 10, 250, 23), "Score: " + scoreInt + "Interval: "+interval);
        }
        //condition if we're tracking lives: player.GetComponent<PlayerScript>().Lives() == 0
        //if dead, display
        else if (player == null)
        {
            GUI.Box(new Rect(10, 10, 250, 23), "Game Over!");

            //final score
            GUI.Box(new Rect(10, 31, 250, 23), "Final Score: " + scoreInt) ;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if (player)
        {
            score = player.GetComponent<PlayerScript>().Score();
        }

        if (((int)timer) < (int)(timer + Time.deltaTime))
            interval /= 1.01f;
        timer += Time.deltaTime;
		if (timer >= interval)
        {
            timer -= interval;
            GameObject nextHazard = hazardPrefabs[Random.Range(0, hazardPrefabs.Length)];
            Vector3 nextHazardPosition;
            Quaternion nextHazardRotation;

            if (odd)
            {
                nextHazardPosition = nextHazard.GetComponent<Hazard>().GetImplementLoc1();
                nextHazardRotation = nextHazard.GetComponent<Hazard>().GetImplementRot1();
            }
            else
            {
                nextHazardPosition = nextHazard.GetComponent<Hazard>().GetImplementLoc2();
                nextHazardRotation = nextHazard.GetComponent<Hazard>().GetImplementRot2();
            }
            odd = !odd;

            Instantiate(nextHazard, nextHazardPosition, nextHazardRotation);
        }
	}
}
