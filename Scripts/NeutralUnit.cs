using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeutralUnit : MonoBehaviour
{
    int droneSpawnRateMin = 2;
    int droneSpawnRateMax = 5;
    public float droneSpawnCooldown;
    [SerializeField] Button endTurnButtonH;
    GameManager gm;
    [SerializeField] GameObject[] neutralDrones;
    [SerializeField] GameObject droneSpawnIndicator;
    Vector3 droneSpawnPos;

    [SerializeField] GameObject camera;
    public Ray ray;
    public RaycastHit hit;
    public bool hasHit;
    bool canSpawn;
    int crashPreventInt;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
        droneSpawnCooldown = Random.Range(droneSpawnRateMin, droneSpawnRateMax);
        Debug.Log("Drone spawns in " + droneSpawnCooldown + " turn");
    }

    // Update is called once per frame
    void Update()
    {
        if (droneSpawnCooldown == 1)
        {
            DroneSpawnIndicator();
        }

        if (droneSpawnCooldown <= 0)
        {
            DroneSpawner();
        }
    }

    void NURayCast()
    {
        Vector3 direction = (droneSpawnPos - camera.transform.position).normalized;
        Ray ray = new Ray(camera.transform.position, direction);
        hasHit = Physics.Raycast(ray, out hit);

        if (hit.transform.CompareTag("Tile"))
        {
            canSpawn = true;
        }
        else if (!hit.transform.CompareTag("Tile"))
        {
            canSpawn = false;
        }
    }

    private void DroneSpawnIndicator()
    {
        droneSpawnPos = new Vector3(Random.Range(1, 4), 0, Random.Range(1, 4));

        crashPreventInt = 0;
        //use while loop so that when the ray hits the tile, it confirms the spawnlocation. And if the ray hits any units, generate another position and try raycasting until it hits a tile.
        NURayCast();


        while (!canSpawn && crashPreventInt < 100)
        {
            droneSpawnPos = new Vector3(Random.Range(1, 4), 0, Random.Range(1, 4));
            NURayCast();
            crashPreventInt++;
        }

        if (!canSpawn && crashPreventInt >= 100)
        {
            Debug.Log("All Tiles Obstucted!");
        }

        if (canSpawn)
        {
            droneSpawnIndicator.transform.position = droneSpawnPos;
            droneSpawnIndicator.gameObject.SetActive(true);
            droneSpawnCooldown -= 0.01f;
        }
    }

    private void DroneSpawner()
    {
        NURayCast();

        if (canSpawn)
        {
            droneSpawnIndicator.gameObject.SetActive(false);
            Instantiate(neutralDrones[Random.Range(0, 3)], droneSpawnPos, Quaternion.identity);

            droneSpawnCooldown = Random.Range(droneSpawnRateMin, droneSpawnRateMax);
            Debug.Log("Drone spawns again in " + droneSpawnCooldown + " turn");
            crashPreventInt = 0;
        }

        if (!canSpawn)
        {
            droneSpawnCooldown = 1;
        }
    }


    //Indicator that appears a turn prior to drone spawn
    //Add two gold to whoever kills the drone
    //Indicator turns on a turn prior to spawing
    //Neutrals don't spawn on the same tile

}
