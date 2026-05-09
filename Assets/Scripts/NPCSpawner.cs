using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public int count = 20;
    public GameObject NPC;
    void Start()
    {
        for (int i=0; i<=count; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(-15, 15), 1, Random.Range(-15, 20));
            Instantiate(NPC, randomPos, Quaternion.identity);
        }
    }

    
    void Update()
    {
        if (count <= Random.Range(12, 16))
        {
            SpawnNPC();
        }
    }
    void SpawnNPC()
    {
        for(int i=0;i<Random.Range(1,5); i++)
        {
            count++;
            Vector3 randomPos = new Vector3(Random.Range(-15, 15), 1, Random.Range(-15, 20));
            Instantiate(NPC, randomPos, Quaternion.identity);
        }
    }
}
