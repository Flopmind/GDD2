using UnityEngine;

public abstract class Hazard : MonoBehaviour {

    public int score;
    public int[] costs;

    public abstract Vector3 GetImplementLoc1();
    public abstract Vector3 GetImplementLoc2();
    public abstract Quaternion GetImplementRot1();
    public abstract Quaternion GetImplementRot2();
    protected abstract void ApplySpend(int cost);
    public int Spend(int budget)
    {
        if (costs.Length == 0)
        {
            throw new System.NullReferenceException("This prefab has no costs assigned to it.");
        }
        int count = 0;
        int choice = this.RandomNum(0, costs.Length);
        while (budget < costs[choice])
        {
            choice = this.RandomNum(0, costs.Length);
            count++;
            if (count > 5)
            {
                return 0;
            }
        }

        ApplySpend(costs[choice]);

        return (budget - costs[choice]);
    }

    public void Score()
    {
        if (GameObject.Find("Player"))
        {
            GameObject.Find("Player").GetComponent<PlayerScript>().AddScore(score);
        }
    }

    protected int RandomNum(int min, int max)
    {
        int rand = (int)Random.Range(min, max);
        while (rand == max)
        {
            rand = (int)Random.Range(min, max);
        }
        return rand;
    }
}
