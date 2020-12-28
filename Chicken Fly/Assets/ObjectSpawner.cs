using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public float MinimumHeight = 2f, time = 2f;
    public GameObject[] Entities;
    public float[] SpawnChance;

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
                    Instantiate(Entities[Random.Range(0, Entities.Length)], spawnPos, Quaternion.identity);
                }

                yield return new WaitForSeconds(time);
            }
            yield return null;
        }    
    }
}
