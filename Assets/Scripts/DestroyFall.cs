using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFall : MonoBehaviour
{
    private SpawnManager spawner;

    private void Start()
    {
        spawner = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Access the parent object
        GameObject parentObject = transform.parent.gameObject;
        Rigidbody2D parentRigidbody = parentObject.GetComponent<Rigidbody2D>();
        // Access the specific script on the parent object
        Fall parentScript = parentObject.GetComponent<Fall>();
        
        if (collision.gameObject.tag == "ground")
        {
            if (parentScript != null)
            {
                Debug.Log("Fall destroyed");
                // Destroy the ParentScript component on the parent object
                Destroy(parentRigidbody);
                Destroy(parentScript);
                if (spawner.hasSpawned)
                {
                    spawner.SpawnPiece();
                }


            }
        }

   
    }
    
}
