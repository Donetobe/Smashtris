using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CrumbleDetector : MonoBehaviour
{
    bool didItCrumble = false;
    private List<GameObject> fallList = new List<GameObject>();
    LayerMask mask;
    
    // Start is called before the first frame update
    void Start()
    {
        mask = LayerMask.GetMask("ground");
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }


    public void CheckIfCrumble(int weight, Vector2 Pos)
    {
        int ammountOfblocks = 0;
        Vector2 position = transform.position;
        bool loop = true;
        while (loop)
        {
            RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, 100f, mask);



            while (hit.collider != null)
            {
                fallList.Add(hit.collider.gameObject);



                position.y = hit.collider.transform.position.y - 0.9f;

                hit = Physics2D.Raycast(position, Vector2.down, 0.1f, mask);
                ammountOfblocks++;
            }
            
            if (ammountOfblocks - weight < weight)
            {
                foreach (GameObject item in fallList)
                {

                }

       
                
            }
            else
            {
                loop = false;
                break;
            }


            Debug.Log("The ammount of blocks is " + ammountOfblocks + "And the weight is " + weight);
            
        }
       
      
    }

    IEnumerator ExampleCoroutine()
    {
    
        
        yield return new WaitForSeconds(1);

     
    }

    
}
