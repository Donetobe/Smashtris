using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFall : MonoBehaviour
{
    private Quaternion initialRotation;
    private SpawnManager spawner;
    private int weight = 1;




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
                    Debug.Log("collided "+ child.name);
                    dedectCrumble();
                }

                Destroy(parentRigidbody);
                Destroy(parentScript);
                Destroy(col);
            }
           
        }

       

    }
    
    void dedectCrumble()
    {
    
        Vector2 position = transform.position;
        position.y -= 1;

        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, 0.1f);
        Debug.DrawRay(position, Vector2.down, Color.white, 4f);
    }
    
}
