using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class DestroyFall : MonoBehaviour
{
    private Quaternion initialRotation;
    private SpawnManager spawner;

    GameObject parentObject;
    Rigidbody2D parentRigidbody;
    Fall parentScript;
    Collider2D col;

    public bool didItStop = false;

    LayerMask mask;

    private void Start()
    {
        spawner = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        initialRotation = transform.rotation;
        mask = LayerMask.GetMask("Detector", "ground");
        parentObject = transform.parent.gameObject;
        parentRigidbody = parentObject.GetComponent<Rigidbody2D>();
        parentScript = parentObject.GetComponent<Fall>();
        col = parentObject.GetComponent<Collider2D>();
    }

    private void LateUpdate()
    {
        transform.rotation = initialRotation;

        
       
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Access the parent object
   




        if (collision.gameObject.tag == "ground" && !didItStop)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("ground"))
            {
                DestroyFall hitObject;

                hitObject = collision.gameObject.GetComponent<DestroyFall>();

                hitObject.didItStop = true;

            }

        



            spawner.hasSpawned = false;
            if (parentScript != null)
            {

                parentScript.fallSpeed = 0;
                // Debug.Log("Fall destroyed");
                // Destroy the ParentScript components

                
              


              
                    int childCount = parentObject.transform.childCount;

                    for (int i = childCount - 1; i >= 0; i--)
                    {
                        Transform child1 = parentObject.transform.GetChild(i);
                        child1.SetParent(null);
                        child1.gameObject.layer = 3;
                        child1.gameObject.tag = "ground";
                        
                    }



              
            }



            didItStop = true;
            dedectCrumble();
            Destroy(parentRigidbody);
            Destroy(parentScript);
            Destroy(col);
       
        
         
          
         
        }

        

    }

    void dedectCrumble()
    {
       
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
