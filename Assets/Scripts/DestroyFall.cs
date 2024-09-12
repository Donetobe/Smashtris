using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;

public class DestroyFall : MonoBehaviour
{
    private Quaternion initialRotation;
    private SpawnManager spawner;


    LayerMask mask;


    private void Start()
    {
        spawner = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        initialRotation = transform.rotation;
        mask = LayerMask.GetMask("Detector", "ground");
    }

    private void LateUpdate()
    {
        transform.rotation = initialRotation;

        
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Access the parent object
    
    
       
 
        
        if (collision.gameObject.tag == "ground")
        {
            dedectCrumble();

            GameObject parentObject = transform.parent.gameObject;
            Rigidbody2D parentRigidbody = parentObject.GetComponent<Rigidbody2D>();
            // Access the specific script on the parent object
            Fall parentScript = parentObject.GetComponent<Fall>();

            Collider2D col = parentObject.GetComponent<Collider2D>();

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
                    child.SetParent(null);



                }


              
            }



            Destroy(parentRigidbody);
            Destroy(parentScript);
            Destroy(col);
       
        
         
          
         
        }

        

    }

    void dedectCrumble()
    {
        Debug.Log("Raysent");
        int weight = 1;
        Vector2 position = transform.position;
        position.y += 1;

        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.up, 100f, mask);

        while (!hit.collider.CompareTag("Detector"))
        {
            position.y += 1;
            weight++;
            hit = Physics2D.Raycast(position, Vector2.up, 100f, mask);
        }
        CrumbleDetector crumbleManager;

        crumbleManager = hit.collider.gameObject.GetComponent<CrumbleDetector>();

        crumbleManager.CheckIfCrumble(weight);

        Debug.DrawRay(position, Vector2.up, Color.white, 4f);

      
        
    }

}
