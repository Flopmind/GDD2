using UnityEngine;

public class ExplosionScript : MonoBehaviour {

    public float moveSpeed;
    public float explosionGrowthRate;
	// Use this for initialization
	void Start () {
        
		
	}
	
	// Update is called once per frame
	void Update () {
 //       transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
        this.gameObject.GetComponent<Light>().range += explosionGrowthRate;
        this.gameObject.transform.position += new Vector3(0, 0, moveSpeed);
        if (this.transform.position.z <= -5)
        {
            Destroy(this.gameObject);
        }
	}
}
