using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public float MinimumHeight = 2f, time = 2f;
    public GameObject[] Entities;
    public int[] SpawnChance;

    PlayerLauncher launcher;
    new Rigidbody2D rigidbody;
    GameObject[] SpawningPoints;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        SpawningPoints = GameObject.FindGameObjectsWithTag("SpawnPoint");
        launcher = GetComponent<PlayerLauncher>();
        StartCoroutine(SpawnEntity());
    }

    IEnumerator SpawnEntity()
    {
        while (true)
        {
            if (launcher.launched)
            {
                Vector3 spawnPos = SpawningPoints[Random.Range(0, SpawningPoints.Length)].transform.position;
                if (spawnPos.y >= MinimumHeight && !Physics.CheckSphere(spawnPos,0.5f) && rigidbody.velocity.x > 0.5f)
                {
                    Instantiate(Entities[CalculateSpawnChance()], spawnPos, Quaternion.identity);
                }

                yield return new WaitForSeconds(time);
            }
            yield return null;
        }    
    }

    int CalculateSpawnChance()
    {
        int itemweight = 0;

        for (int i = 0; i < SpawnChance.Length; i++)
        {
            itemweight += SpawnChance[i];
        }

        int rng = Random.Range(0,itemweight);

        for (int j = 0; j < SpawnChance.Length; j++)
        {
            if (rng <= SpawnChance[j])
            {
                return j;
            }
            else
            {
                rng -= SpawnChance[j];
            }
        }
        Debug.LogError("Something Went Wrong!");
        return 1;
    }
}
