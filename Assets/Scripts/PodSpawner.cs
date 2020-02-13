using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodSpawner : MonoBehaviour
{
    public GameObject pod;
    public List<GameObject> pods;
    public float nrOfPods;
    public float xPos;
    public float yPos;
    void Start()
    {
        for(int i=0; i<nrOfPods; i++)
        {
            CreateSecretPod(i);
        }
        
    }

    public void Setup()
    {
        for (int i = 0; i < nrOfPods; i++)
        {
            CreateSecretPod(i);
        }
    }

    public void CreateSecretPod(int i)
    {
        GameObject tempPod;
        tempPod = Instantiate(pod,
            new Vector2(Random.Range(-8, 8), Random.Range((i * 10) - 5, (i * 10) + 5)),
            Quaternion.identity);
        pods.Add(tempPod);
    }
}
