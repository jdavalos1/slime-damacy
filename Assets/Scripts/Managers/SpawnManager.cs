using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public int currentlyActive;

    [SerializeField]
    private Vector2 boundaries;
    /// <summary>
    /// Objects to pool
    /// </summary>
    [SerializeField]
    private int maxPooled;

    /// <summary>
    /// Minimum distance before a summon is placed
    /// </summary>
    [SerializeField]
    private float summonDistThreshhold;

    /// <summary>
    /// Object to spawn
    /// </summary>
    [SerializeField]
    private GameObject spawnObject;

    /// <summary>
    /// Player transform
    /// </summary>
    [SerializeField]
    private Transform player;

    /// <summary>
    /// Max number of active spawns on the field
    /// </summary>
    [SerializeField]
    private int maxSpawn;

    /// <summary>
    /// Spawn limits from the actual spawn manager
    /// </summary>
    [SerializeField]
    private Vector2 spawnLimits;

    /// <summary>
    /// Scale difference between self and player
    /// </summary>
    [SerializeField]
    private Vector3 spawnScale;

    private List<GameObject> spawnObjects;
    // Start is called before the first frame update
    void Start()
    {
        // Create a list of pregenerated objects
        spawnObjects = new List<GameObject>();
        GameObject temp;

        for(int i = 0; i < maxPooled; i++)
        {
            temp = Instantiate(spawnObject);
            temp.SetActive(false);
            temp.GetComponent<Enemy>().spawnOwner = this;
            spawnObjects.Add(temp);
        }

        currentlyActive = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.SharedInstance.isGameOver)
        {
            if (Vector3.Distance(transform.position, player.position) <= summonDistThreshhold)
            {
                UpdateSpawnScale();
                SummonSpawn();
            }
        }
    }

    private void UpdateSpawnScale()
    {
        foreach(var spawn in spawnObjects)
        {
            if (!spawn.activeSelf)
            {
                spawn.transform.localScale = player.localScale + spawnScale;
                spawn.GetComponent<ItemAttributes>().itemScale = player.localScale + spawnScale;
            }
        }
    }

    public void SummonSpawn()
    {
        if(currentlyActive < maxSpawn)
        {
            currentlyActive++;
            GameObject spawn = spawnObjects.Find(s => !s.activeInHierarchy);
            spawn.SetActive(true);
            spawn.transform.position = GetRandomPosition() + transform.position;
        }

    }

    private Vector3 GetRandomPosition()
    {
        return new Vector3(Random.Range(-spawnLimits.x, spawnLimits.x),
                           Random.Range(-spawnLimits.y, spawnLimits.y),
                           -0.1f);
    }
}
