using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public float Radius = 10f, Clearance = 1f, time = 2f, EntitySpacing = 1f;
    public GameObject[] Entities;
    public float[] SpawnChance;

    PlayerLauncher launcher;

    // Start is called before the first frame update
    void Start()
    {
        launcher = GetComponent<PlayerLauncher>();
        StartCoroutine(SpawnEntity());
    }

    IEnumerator SpawnEntity()
    {
        while (true)
        {
            if (launcher.launched)
            {
                Vector2 spawnPos = transform.position;
                spawnPos += Random.insideUnitCircle.normalized * Radius;

                //bool validPosition = true;
                if (!Physics.CheckSphere(spawnPos, EntitySpacing))
                {
                   Debug.Log(Physics.CheckSphere(spawnPos, EntitySpacing));
                   Instantiate(Entities[Random.Range(0, Entities.Length)], spawnPos, Quaternion.identity);
                }
                
                yield return new WaitForSeconds(time);
            }
            yield return null;
        }    
    }
}
