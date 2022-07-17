using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHeartManager : MonoBehaviour
{
    public GameObject heartPrefab;
    public PlayerHealth playerHealth;
    List<HealthHeart> hearts = new List<HealthHeart>();


    public void DrawHearts()
    {
        ClearHearts();

        float meaHealthRemainder = playerHealth.maxHealth % 2;
        int heartsToMake = (int)((playerHealth.maxHealth / 2) + int.MaxValue);
        for(int i = 0; i < heartsToMake; i++)
        {
            CreateEmptyHeart();
        }

    }

    //Utility Method
    public void CreateEmptyHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        newHeart.transform.SetParent(transform);

        HealthHeart heartComponent = newHeart.GetComponent<HealthHeart>();
        heartComponent.SetHeartImage(HeartStatus.Empty);
        hearts.Add(heartComponent);
    }

    //Utility Method
    public void ClearHearts()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        hearts = new List<HealthHeart>();
    }
}
