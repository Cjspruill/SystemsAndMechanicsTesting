using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{
    [SerializeField] Transform[] spawnLocations;    //List of all spawn locations we can respawn at
    [SerializeField] int spawnIndex;    //Our current spawn index

    public int SpawnIndex { get => spawnIndex; set => spawnIndex = value; } //Property for spawn index

    [SerializeField] float respawnTimer;
    [SerializeField] float respawnTime;

    [SerializeField] Bird bird;

    // Start is called before the first frame update
    void Start()
    {
        //Calling respawn at start to randomize
        RespawnAtARandomPoint();
    }

    // Update is called once per frame
    void Update()
    {
        //Increment respawn timer by time.delta time
        respawnTimer += Time.deltaTime;

        //If respawn timer is greater or equal to respawn time, reset timer and respawn
        if(respawnTimer >= respawnTime)
        {
            respawnTimer = 0;
            RespawnAtARandomPoint();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //If other object tag is equal to respawn, repspawn at a random point
        if (other.CompareTag("Respawn"))
        {
            RespawnAtARandomPoint();
        }
    }

    //Respawns at a random point
    void RespawnAtARandomPoint()
    {
        //Set spawn index to random between 0 and spawn location, then set location an rotation to the new spawn location
        SpawnIndex = Random.Range(0, spawnLocations.Length);
        gameObject.transform.rotation = spawnLocations[SpawnIndex].rotation;
        gameObject.transform.position = spawnLocations[SpawnIndex].position;

            //if (bird != null)
            //{
            //    bird.FireBird();
            //}

    }
}
