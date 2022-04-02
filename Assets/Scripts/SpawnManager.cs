using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private int maxSpawned;
    [SerializeField]
    private float summonDistThreshhold;
    [SerializeField]
    private GameObject spawnObject;
    [SerializeField]
    private Transform player;

    public int currentlyActive;
    [SerializeField]
    private int spawnThreshhold;

    private List<GameObject> spawnObjects;
    // Start is called before the first frame update
    void Start()
    {
        // Create a list of pregenerated objects
        spawnObjects = new List<GameObject>();
        GameObject temp;

        for(int i = 0; i < maxSpawned; i++)
        {
            temp = Instantiate(spawnObject);
            temp.SetActive(false);
            temp.GetComponent<ItemAttributes>().spawnOwner = this;
            spawnObjects.Add(temp);
        }

        currentlyActive = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.position) <= summonDistThreshhold)
        {
            SummonSpawn();
        }
    }

    public void SummonSpawn()
    {
        if(currentlyActive <= spawnThreshhold)
        {
            currentlyActive++;
            GameObject spawn = spawnObjects.Find(s => !s.activeInHierarchy);
            spawn.SetActive(true);
            spawn.transform.position = GetRandomPosition();
        }

    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3();
    }
}
