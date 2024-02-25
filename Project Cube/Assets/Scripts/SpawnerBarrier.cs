using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerBarrier : MonoBehaviour
{
    public GameObject[] spawnedBarrier;
    GameObject instantiatedMid;
    GameObject instantiatedLeft;
    GameObject instantiatedRight;
    public Transform[] spawners;


   public void Start()
    {
        SpawnBarriers();
    }


    public void Update()
    {        
        if(PlayerManager.isGameStarted == true)
        {
            Destroy(instantiatedMid, 10f);
            Destroy(instantiatedLeft, 10f);
            Destroy(instantiatedRight, 10f);
        }

     }

    public void SpawnBarriers()
    {
        
        List<int> shuffledIndexes = Enumerable.Range(0, spawnedBarrier.Length).OrderBy(x => Random.value).ToList();

        for (int i = 0; i < spawners.Length; i++)
        {
            
            int barrierIndex = shuffledIndexes[i % shuffledIndexes.Count];
            GameObject instantiatedBarrier = Instantiate(spawnedBarrier[barrierIndex], spawners[i].position, Quaternion.identity);

            if (i == 0)
            {
                instantiatedLeft = instantiatedBarrier;
            }
            else if (i == 1)
            {
                instantiatedMid = instantiatedBarrier;
            }
            else if (i == 2)
            {
                instantiatedRight = instantiatedBarrier;
            }
        }
    }
}