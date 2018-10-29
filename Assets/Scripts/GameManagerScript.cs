using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {

    public GameObject[] hazardPrefabs;
    public GameObject player;
    public float interval;
    public int startBudget;
    public int lives; // if we want to implement this later

    //private List<GameObject> activeHazards;
    private float timer;
    private float score;
    private bool odd;
    private string highScoreList;
    private int budget;
    private int currentBudget;

	// Use this for initialization
	async void Start ()
    {
        timer = 0;
        odd = true;
        if (interval <= 0)
        {
            throw new System.Exception("Something cannot every at or less than every 0 seconds. Change interval in the GameManager Prefab");
        }
        // high score display
        highScoreList = await HighScoreScript.GetHighScores();
        budget = startBudget;
        currentBudget = budget;
    }

   

    //writing score on screen
    private void OnGUI()
    {
        //type casting score to int
        int scoreInt = (int)score;

        if (player)
        {
            //score display
            GUI.Box(new Rect(10, 10, 250, 23), "Score: " + scoreInt);
            GUI.Box(new Rect(100, 10, 250, 250), highScoreList);
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

        timer += Time.deltaTime;
		if (timer >= interval)
        {
            timer -= interval;
            GameObject nextHazard;
            do
            {
                int oldBudget = currentBudget;
                do
                {
                    nextHazard = hazardPrefabs[Random.Range(0, hazardPrefabs.Length)];
                    currentBudget = nextHazard.GetComponent<Hazard>().Spend(currentBudget);
                }
                while (oldBudget == currentBudget);
            }
            while (currentBudget > 0);
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
