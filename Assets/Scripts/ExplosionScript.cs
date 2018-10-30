using UnityEngine;

public class ExplosionScript : MonoBehaviour {

    public float moveSpeed;
    public float explosionGrowthRate;
	// Use this for initialization
	void Start () {
        
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        this.gameObject.GetComponent<Light>().range += explosionGrowthRate;
        this.gameObject.transform.position += new Vector3(0, 0, moveSpeed);
	}
}
