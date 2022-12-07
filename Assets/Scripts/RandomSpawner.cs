using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{

    [SerializeField] private GameObject Player01;
    [SerializeField] private GameObject Player02;
    public float randomMin = -10;
    public float randomMax = 10;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Vector3 randomSpawnPosition = new Vector3(Random.Range(randomMin, randomMax),(0),Random.Range(randomMin, randomMax));
            Instantiate(Player01, randomSpawnPosition, Quaternion.identity);
        }
    }
    
}
