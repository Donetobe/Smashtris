using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
                bool canCrumble = false;
                foreach (var item in fallList)
                {
                    Vector2 position1 = item.transform.position;
                    DestroyFall script = item.GetComponent<DestroyFall>();
                    Rigidbody2D rb2d = item.GetComponent<Rigidbody2D>();


                    if (position1.y - 1 > -17)
                    {
                

                        item.gameObject.layer = 6;
                        item.gameObject.tag = null;
                        script.didItStop = false;
                        canCrumble = true;
                    }

       



                }
                while (true)
                {
                    foreach (var item in fallList)
                    {
                        Vector2 position1 = item.transform.position;

                        if (canCrumble)
                        {
                            StartCoroutine(ExampleCoroutine());
                            position1.y -= 1;
                            Debug.Log("It fell");
                            item.transform.position = position1;

                        }

                    }
                }
               

                /*
                bool canCrumble = true;

                foreach (var item in fallList)
                {
                    Vector2 position1 = item.transform.position;

                    if (position1.y - 1 < -17)
                    {
                         
                        canCrumble = false;
                        loop = false;
                         
                    }
                  

                }
                

                foreach (var item in fallList)
                {
                    Vector2 position1 = item.transform.position;

                    if (canCrumble)
                    {
                        StartCoroutine(ExampleCoroutine());
                        position1.y -= 1;
                        Debug.Log("It fell");
                        item.transform.position = position1;
                        
                    }

                }
                position = fallList[fallList.Count - 1].gameObject.transform.position;
                */
            }
       


            Debug.Log("The ammount of blocks is " + ammountOfblocks + "And the weight is " + weight);

       
       
       
      
    }

    IEnumerator ExampleCoroutine()
    {
    
        
        yield return new WaitForSeconds(1);

     
    }
}
