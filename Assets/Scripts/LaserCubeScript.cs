using UnityEngine;

public class LaserCubeScript : Hazard {

    public float speed;
    public float time;
    public float laserDelay;
    public GameObject myLaser;
    
    private bool odd;
    private float timer;

	// Use this for initialization
	void Start ()
    {
        GameObject ground = GameObject.Find("Ground");
        if (transform.position == new Vector3(
                ground.transform.position.x + (ground.transform.localScale.x / 2) + 8,
                ground.transform.position.y + 1,
                ground.transform.position.z + (ground.transform.localScale.z / 2) + 5))
        {
            odd = true;
        }
        else
        {
            odd = false;
        }
	}

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= time)
        {
            Score();
            Destroy(gameObject);
        }
        if (!myLaser.activeSelf && timer >= laserDelay)
        {
            myLaser.SetActive(true);
        }
    }

    void FixedUpdate ()
    {
		if (odd)
        {
            transform.position = new Vector3(transform.position.x - (speed * Time.deltaTime), transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - (speed * Time.deltaTime));
        }
	}

    public override Vector3 GetImplementLoc1()
    {
        GameObject ground = GameObject.Find("Ground");
        if (ground)
        {
            return new Vector3(
                ground.transform.position.x + (ground.transform.localScale.x / 2) + 5,
                ground.transform.position.y + 1,
                ground.transform.position.z + (ground.transform.localScale.z / 2) + 8);
        }
        else
        {
            throw new System.Exception("invalid gameobject 'Ground' in LaserCubeScript");
        }
    }

    public override Quaternion GetImplementRot1()
    {
        return Quaternion.identity;
    }

    public override Vector3 GetImplementLoc2()
    {
        GameObject ground = GameObject.Find("Ground");
        if (ground)
        {
            return new Vector3(
                ground.transform.position.x + (ground.transform.localScale.x / 2) + 8,
                ground.transform.position.y + 1,
                ground.transform.position.z + (ground.transform.localScale.z / 2) + 5);
        }
        else
        {
            throw new System.Exception("invalid gameobject 'Ground' in LaserCubeScript");
        }
    }

    public override Quaternion GetImplementRot2()
    {
        // testing to see if it returns the correct angle
        print(Quaternion.AngleAxis(-90, new Vector3(0, 1, 0)));
        return Quaternion.AngleAxis(-90, new Vector3(0, 1, 0));
    }
}
