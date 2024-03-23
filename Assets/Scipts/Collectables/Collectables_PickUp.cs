using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Collectables_PickUp : MonoBehaviour
{
    AudioSource audioSource;


    // Enum for the different types of pick ups
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
    //[SerializeField] float destroyTimer = 0.0f;
    [SerializeField] AudioClip pickUpSound;
    // Five spawn locations within the level
    [SerializeField] Transform[] spawnLocations;

    private void Start()
    {
        SpawnCollectibles();
        audioSource = GetComponent<AudioSource>();
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
            audioSource.PlayOneShot(pickUpSound);
            GetComponent<SpriteRenderer>().enabled = false; //Hide the collectable by turning it off
            Destroy(gameObject, pickUpSound.length);
        }
    }
}
