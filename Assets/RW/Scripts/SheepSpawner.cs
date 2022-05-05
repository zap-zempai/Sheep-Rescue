using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheepSpawner : MonoBehaviour
{
    public bool canSpawn = true;

    public GameObject sheepPrefab;
    public GameObject GoldsheepPrefab;
    public GameObject BlacksheepPrefab;

    public float probGold;
    public float probBlack;

    public List<Transform> sheepSpawnPositions = new List<Transform>();
    public float timeBetweenSpawns;

    private List<GameObject> sheepList = new List<GameObject>();

    private SheepSpawner sheepSpawner;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SpawnSheep()
    {
        Vector3 randomPosition = sheepSpawnPositions[Random.Range(0, sheepSpawnPositions.Count)].position;
        float num = Random.Range(0.0f, 1.0f);
        GameObject sheep;
        if (num < probGold)
        {
            sheep = Instantiate(GoldsheepPrefab, randomPosition, sheepPrefab.transform.rotation);
        }
        else if (num < probGold + probBlack)
        {
            sheep = Instantiate(BlacksheepPrefab, randomPosition, sheepPrefab.transform.rotation);
        }
        else 
        {
            sheep = Instantiate(sheepPrefab, randomPosition, sheepPrefab.transform.rotation);
        }
        sheepList.Add(sheep);
        sheep.GetComponent<Sheep>().SetSpawner(this);
    }

    private IEnumerator SpawnRoutine()
    {
        while (canSpawn)
        {
            SpawnSheep();
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }

    public void RemoveSheepFromList(GameObject sheep)
    {
        sheepList.Remove(sheep);
    }

    public void DestroyAllSheep()
    {
        foreach (GameObject sheep in sheepList) 
        {
            Destroy(sheep); 
        }

        sheepList.Clear();
    }
}
