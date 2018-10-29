using System.Collections.Generic;
using UnityEngine;

public class BombScript : Hazard {

    public float timerLimit;
    public float deathRange;
    public Color startColor;
    public Color endColor;

    //Explosion prefab holder
    public GameObject explosion;

    private GameObject player;
    private float timer;

	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("Player");
        timer = 0.0f;
    }
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        if (timer >= timerLimit)
        {
            foreach (GameObject enemy in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Exterminate(enemy);
            }
            timer = timerLimit;
            Exterminate(player);
            GameObject explosionSpawn = Instantiate(explosion, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
            Score();
            Destroy(gameObject);
        }
        GetComponent<MeshRenderer>().material.color = Color.Lerp(startColor, endColor, timer / timerLimit);
	}

    public override Vector3 GetImplementLoc1()
    {
        GameObject ground = GameObject.Find("Ground");
        Vector3 myVec;
        GameObject[] bombs = GameObject.FindGameObjectsWithTag("Bomb");
        bool tooClose = true;
        int count = 0;
        if (ground)
        {
            do
            {
                count++;
                myVec = new Vector3(
                Random.Range(ground.transform.position.x - (ground.transform.localScale.x / 2), ground.transform.position.x + (ground.transform.localScale.x / 2)),
                1.5f,
                Random.Range(ground.transform.position.z - (ground.transform.localScale.z / 2), ground.transform.position.z + (ground.transform.localScale.z / 2)));
                foreach (GameObject bomb in bombs)
                {
                    if ((myVec - bomb.transform.position).magnitude < 4)
                    {
                        tooClose = false;
                    }
                }
            }
            while (tooClose && count < 20);
            return myVec;
        }
        else
        {
            throw new System.Exception("invalid gameobject 'Ground' in BombScript");
        }
    }

    public override Vector3 GetImplementLoc2()
    {
        return GetImplementLoc1();
    }

    public override Quaternion GetImplementRot1()
    {
        return Quaternion.identity;
    }

    public override Quaternion GetImplementRot2()
    {
        return Quaternion.identity;
    }

    private void Exterminate(GameObject obj)
    {
        if (obj && (obj.transform.position - transform.position).magnitude <= deathRange)
        {
            Destroy(obj);
        }
    }

    public override void ApplySpend(int cost)
    {

    }
}
