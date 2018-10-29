using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float moveMag;
    public float jumpMag;
    
    private int jumpsLeft;
    private float score;

	// Use this for initialization
	void Start ()
    {
        jumpsLeft = 2;
	}
	
	// Update is called once per frame
	void Update ()
    {
        score += Time.deltaTime;

        Vector3 moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveMag, 0.0f, Input.GetAxis("Vertical") * moveMag);
        moveDirection = moveDirection.normalized;

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            GetComponent<Rigidbody>().velocity =
            Quaternion.Euler(0.0f, 45.0f, 0.0f) *
            new Vector3(
                moveDirection.x * moveMag,
                GetComponent<Rigidbody>().velocity.y,
                moveDirection.z * moveMag);
        }
        else
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0.0f, GetComponent<Rigidbody>().velocity.y, 0.0f);
        }

        if (Input.GetButtonDown("Jump") && jumpsLeft > 0)
        {
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, jumpMag, GetComponent<Rigidbody>().velocity.z);
            jumpsLeft--;
        }

        if (gameObject.transform.position.y < -4f) Destroy(gameObject);
	}

    //accessing score of the player
    public float Score()
    {
        return score;
    }

    public void AddScore(int deltaScore)
    {
        if (deltaScore < 0)
        {
            deltaScore *= -1;
        }
        score += deltaScore;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            GetComponent<Rigidbody>().velocity = new Vector3(GetComponent<Rigidbody>().velocity.x, 0.0f, GetComponent<Rigidbody>().velocity.z);
            jumpsLeft = 2;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().CompareTag("Hazard") || other.GetComponent<Collider>().CompareTag("Enemy"))
        {
            Destroy(gameObject);
            //Application.LoadLevel(Application.loadedLevel); RESETS SCENE, USE FOR GAMEOVER SCENARIO(?)
        }
    }
}
