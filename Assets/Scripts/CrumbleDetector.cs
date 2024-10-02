using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CrumbleDetector : MonoBehaviour
{
    bool didItCrumble = false;
    private List<GameObject> fallList = new List<GameObject>();
    private ScoreManager scoreManager;
    LayerMask mask;
    LayerMask mask1;

    // Start is called before the first frame update
    void Start()
    {
        mask = LayerMask.GetMask("ground");
        mask1 = LayerMask.GetMask("Default", "ground");
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
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
            fallList.Clear();
            RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, 100f, mask);



            while (hit.collider != null)
            {
                fallList.Add(hit.collider.gameObject);



                position.y = hit.collider.transform.position.y - 0.9f;

                hit = Physics2D.Raycast(position, Vector2.down, 0.1f, mask);
                ammountOfblocks++;
                if (hit.collider == null)
                {
                    break;
                }
            }
            
            if (ammountOfblocks - weight < weight && fallList[fallList.Count - 1].gameObject.transform.position.y != -16.5f)
            {
               

                while (true)
                {
                    foreach (GameObject item in fallList)
                    {
                        Vector2 tilePos = item.transform.position;
                        tilePos.y -= 1;
                        item.transform.position = tilePos;
                       
                    }
                    position = fallList[fallList.Count - 1].gameObject.transform.position;
                    Debug.Log($"The last item pos in list is {position}");
                    position.y--;
                    hit = Physics2D.Raycast(position, Vector2.down, 0.1f, mask1);

                    if (hit.collider == null)
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
                int bloksToCrush = ammountOfblocks;
                while (true)
                {
                    if (hit.collider == null)
                    {
                        foreach (GameObject item in fallList)
                        {
                            Vector2 tilePos = item.transform.position;
                            tilePos.y -= 1;
                            item.transform.position = tilePos;

                        }
                        position = fallList[fallList.Count - 1].gameObject.transform.position;

                        position.y--;
                        hit = Physics2D.Raycast(position, Vector2.down, 0.1f, mask1);
                    }
                    else if (hit.collider.gameObject.layer == 3 && bloksToCrush >= 0)
                    {
                        scoreManager.Score += 1;
                        scoreManager.UpdateHighScore(scoreManager.Score);
                        Destroy(hit.collider.gameObject);
                         bloksToCrush--;
                        foreach (GameObject item in fallList)
                        {
                            Vector2 tilePos = item.transform.position;
                            tilePos.y -= 1;
                            item.transform.position = tilePos;
                         
                        }
                        position = fallList[fallList.Count - 1].gameObject.transform.position;
                       
                        position.y--;
                        hit = Physics2D.Raycast(position, Vector2.down, 0.1f, mask1);
                    }
        
                    else if (hit.collider.gameObject.layer == 0)
                    {
                        loop = false;
                        break;
                    }
                    else
                    {
                        loop = false;
                        break;
                    }
                    weight = ammountOfblocks;
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
