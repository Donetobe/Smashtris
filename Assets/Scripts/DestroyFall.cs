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

        Collider2D col = parentObject.GetComponent<Collider2D>();
        
        if (collision.gameObject.tag == "ground")
        {
            if (parentScript != null)
            {
                parentScript.fallSpeed = 0;
                Debug.Log("Fall destroyed");
                // Destroy the ParentScript components
                Destroy(parentRigidbody);
                Destroy(parentScript);
                Destroy(col);
                if (spawner.hasSpawned)
                {
                    spawner.SpawnPiece();
                    spawner.hasSpawned = false;
                }
                
              


            }
        }

   
    }
    

}
