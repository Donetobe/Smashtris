using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrumbleDetector : MonoBehaviour
{
    bool didItCrumble = false;
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


    public void CheckIfCrumble(int weight)
    {
        int ammountOfblocks = 0;
        Vector2 position = transform.position;
        

        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, 100f, mask);

        while (hit.collider != null)
        {

        


            position.y = hit.collider.transform.position.y - 1;
            
            hit = Physics2D.Raycast(position, Vector2.down, 0.1f, mask);
            ammountOfblocks++;
        }

        if (ammountOfblocks - weight < weight)
        {
            didItCrumble = true;
        }


        Debug.Log("The ammount of blocks " + ammountOfblocks);
    }
}
