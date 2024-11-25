using System.Collections;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab; 
    [SerializeField] private int spawnCount = 3; 
    [SerializeField] private float spawnDelay = 0.5f; 
    [SerializeField] private float destroyDelay = 3f;

    Animator anim;

    private Camera playerCamera;

    void Start()
    {
        playerCamera = Camera.main; 
        anim = GetComponent<Animator>();
    }

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(SpawnPrefabsCoroutine());
        }
    }

    IEnumerator SpawnPrefabsCoroutine()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            SpawnPrefab();
            yield return new WaitForSeconds(spawnDelay); 
        }
    }

    void SpawnPrefab()
    {
   
        Vector3 cameraPosition = playerCamera.transform.position;
        float cameraHeight = playerCamera.orthographicSize * 2; 
        float cameraWidth = cameraHeight * playerCamera.aspect; 

       
        float spawnX = Random.Range(cameraPosition.x - cameraWidth / 2, cameraPosition.x + cameraWidth / 2);
        float spawnY = Random.Range(cameraPosition.y - cameraHeight / 2, cameraPosition.y + cameraHeight / 2);
        Vector3 spawnPosition = new Vector3(spawnX, spawnY, 0);

        
        GameObject spawnedObject = Instantiate(prefab, spawnPosition, Quaternion.identity, transform);
        Rigidbody2D obsw = spawnedObject.GetComponent<Rigidbody2D>();

        anim.Play("");

    }

    
}
