using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour {

    public GameObject[] hazardPrefabs;
    public GameObject player;
    public float interval;
    public int startBudget;
    public int budgetIncrement;
    public int lowestCost;
    //public int lives; // if we want to implement this later

    //private List<GameObject> activeHazards;
    private float timer;
    private float score;
    private bool odd;
    private string highScoreList = "Loading high scores";
    private int budget;
    private int currentBudget;
    private string playersName = "";
    private bool submit = false;
    private bool submitted = false;
    private bool laserUsed = false;
    // Use this for initialization
    void Start()
    {
        timer = 0;
        odd = true;
        if (interval <= 0)
        {
            throw new System.Exception("Something cannot every at or less than every 0 seconds. Change interval in the GameManager Prefab");
        }
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
        }
        //condition if we're tracking lives: player.GetComponent<PlayerScript>().Lives() == 0
        //if dead, display
        else if (player == null)
        {
            GUI.Box(new Rect(10, 10, 250, 23), "Game Over!");

            //final score
            GUI.Box(new Rect(10, 31, 250, 23), "Final Score: " + scoreInt);
            if(!submit && !submitted)
            {
                GUI.Box(new Rect(10, 55, 90, 25), "Your Name: ");
                playersName =GUI.TextField(new Rect(100, 55, 100, 25), playersName, 25);
                submit = GUI.Button(new Rect(225, 55, 75, 25), "Submit");
            }
            else
            {
                GUI.Box(new Rect(10, 55, 250, 250), highScoreList);
                if(GUI.Button(new Rect(10, 55+250, 100, 25), "Restart"))
                {
                    SceneManager.LoadScene("Menu");
                }
            }
        }
    }

    // Update is called once per frame
    async void Update ()
    {
        if (player)
        {
            score = player.GetComponent<PlayerScript>().Score();
        }

        timer += Time.deltaTime;
		if (timer >= interval)
        {
            currentBudget = budget;
            timer -= interval;
            GameObject nextHazard;
            int price;
            int count1 = 0;
            int count2 = 0;
            do
            {
                count1++;
                int oldBudget = currentBudget;
                do
                {
                    count2++;
                    nextHazard = hazardPrefabs[Random.Range(0, hazardPrefabs.Length)];
                    if (!laserUsed && nextHazard == hazardPrefabs[3])
                    {
                        laserUsed = true;
                    }
                    else if (nextHazard == hazardPrefabs[3])
                    {
                        do
                        {
                            nextHazard = hazardPrefabs[Random.Range(0, hazardPrefabs.Length)];
                        }
                        while (nextHazard == hazardPrefabs[3]);
                    }
                    price = nextHazard.GetComponent<Hazard>().Spend(currentBudget);
                    currentBudget -= price;
                }
                while (oldBudget == currentBudget);
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

                GameObject haz = Instantiate(nextHazard, nextHazardPosition, nextHazardRotation);
                haz.GetComponent<Hazard>().ApplySpend(price);
            }
            while (currentBudget > lowestCost);
            laserUsed = false;
            budget += budgetIncrement;
        }

        if (submit)
        {
            submit = false; // never submit again
            // Upload the high score
            await HighScoreScript.UploadHighScore(playersName, (int)score);

            // Get high score display
            highScoreList = await HighScoreScript.GetHighScores();
            // Set as submitted
            submitted = true;
        }

    }
}
