using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Collectables_PickUp : MonoBehaviour
{
    public enum PickUp_Type
    {
        Life,
        Score,  
        PowerUp
    }

    [System.Serializable]
    public class PickUpPrefab
    {
        public PickUp_Type type;
        public GameObject prefab;
    }

    [SerializeField] List<PickUpPrefab> pickUpPrefabs; // List of PickUpType to GameObject mappings
    [SerializeField] float destroyTimer = 0;

    // Five spawn locations within the level
    [SerializeField] Transform[] spawnLocations;

    private void Start()
    {
        SpawnCollectibles();
    }

    private void SpawnCollectibles()
    {
        // Spawn the selected collectible at each of the five spawn locations
        foreach (Transform spawnLocation in spawnLocations)
        {
            // Randomly select a pickUpPrefab
            PickUpPrefab selectedPrefab = pickUpPrefabs[Random.Range(0, pickUpPrefabs.Count)];

            // Instantiate the selected prefab at the spawn location
            Instantiate(selectedPrefab.prefab, spawnLocation.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
