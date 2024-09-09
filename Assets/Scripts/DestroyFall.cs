using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class DestroyFall : MonoBehaviour
{
    private Quaternion initialRotation;
    private SpawnManager spawner;
    private int row;




    private void Start()
    {
        spawner = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        initialRotation = transform.rotation;
    
    }

    private void LateUpdate()
    {
        transform.rotation = initialRotation;

        
       
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
            spawner.hasSpawned = false;
            if (parentScript != null)
            {
                parentScript.fallSpeed = 0;
               // Debug.Log("Fall destroyed");
                // Destroy the ParentScript components
                
     
                
                foreach (Transform child in parentObject.transform)
                {

                    child.gameObject.layer = 3;
                    child.gameObject.tag = "ground";
                    

                    for (int i = -8; i == child.transform.position.x; i++)
                    {

                    }

                }


                Destroy(parentRigidbody);
                Destroy(parentScript);
                Destroy(col);
            }
           
        }

       

    }
    
    
    
}
