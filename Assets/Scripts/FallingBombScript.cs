using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBombScript : Hazard {
    
    public float fallHeight;
    public float initialScale;
    public float finalScale;
    public float goneTime;
    //Explosion prefab holder
    public GameObject explosion;

    private GameObject player;
    private GameObject ground;
    private float distancePercent;
    private float goneTimer;
    private bool gone;

    // Use this for initialization
    void Start()
    {
        transform.localScale = new Vector3(initialScale, initialScale, initialScale);
        player = GameObject.Find("Player");
        ground = GameObject.Find("Ground");
        distancePercent = 0;
        goneTimer = 0.0f;
        gone = false;
    }

    // Update is called once per frame
    void Update()
    {
        distancePercent = 1 / ((transform.position - ground.transform.position).magnitude / fallHeight);
        float scale = (((finalScale - initialScale) * distancePercent) / finalScale);
        transform.localScale = new Vector3(scale, scale, scale);

        if (gone)
        {
            goneTimer += Time.deltaTime;
            if (goneTimer >= goneTime)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            GameObject explosionSpawn = Instantiate(explosion, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
            Score();
            gone = true;
            goneTimer = 0.0f;
            GetComponent<MeshRenderer>().enabled = false;
        }
    }

    public override Vector3 GetImplementLoc1()
    {
        GameObject ground = GameObject.Find("Ground");
        if (ground)
        {
            return new Vector3(
                Random.Range(ground.transform.position.x - (ground.transform.localScale.x / 2), ground.transform.position.x + (ground.transform.localScale.x / 2)),
                fallHeight,
                Random.Range(ground.transform.position.z - (ground.transform.localScale.z / 2), ground.transform.position.z + (ground.transform.localScale.z / 2)));
        }
        else
        {
            throw new System.Exception("invalid gameobject 'Ground' in FallingBombScript");
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

    protected override void ApplySpend(int cost)
    {

    }
}
