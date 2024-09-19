using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;


public class DestroyFall : MonoBehaviour
{
    private Quaternion initialRotation;
 

    GameObject parentObject;
    Rigidbody2D parentRigidbody;
    Fall parentScript;
    Collider2D col;

    public bool didItStop = false;

    LayerMask mask;

    private void Start()
    {
      
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

            parentScript.SpawnPiece();



           
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
                       
                        Vector2 temp = new Vector2(Mathf.Round(child1.transform.position.x * 10.0f) * 0.1f, Mathf.Round(child1.transform.position.y * 10.0f) * 0.1f);
                        child1.transform.position = temp;
                    }



              
            }

            if (this.gameObject.transform.position.y >= 15)
            {
                SceneManager.LoadScene(0);
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
     
       Vector2 OGPos = transform.position;
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

     

      
            crumbleManager.CheckIfCrumble(weight, OGPos);
       
       

        Debug.DrawRay(position, Vector2.up, Color.white, 4f);

      
        
    }

}
