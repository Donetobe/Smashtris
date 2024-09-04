using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public bool hasSpawned;
    public List<GameObject> pieceList;
    // Start is called before the first frame update
    void Start()
    {
        hasSpawned = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPiece()
    {
     
        int randomIndex = Random.Range(0, pieceList.Count);
        GameObject pieceToSpawn = pieceList[randomIndex];
        Instantiate(pieceToSpawn, transform.position, Quaternion.identity);
        hasSpawned = true;
    }
}
