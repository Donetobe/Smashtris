using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrumbleDetector : MonoBehaviour
{
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


    void dedectCrumble()
    {

        Vector2 position = transform.position;
        position.y -= 1;

        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.down, 100f, mask);
        Debug.DrawRay(position, Vector2.down, Color.white, 4f);
    }
}
