using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    public float fallSpeed = 1f;

    public float sidewaysSpeed = 1;

  
    private List<Vector2> childPositions = new List<Vector2>();
    private Vector2 newPosition;

    private KeyCode moveRightKey = KeyCode.RightArrow; // Key to move right
    private KeyCode moveLeftKey = KeyCode.LeftArrow; // Key to move left
    [SerializeField] private float rotationAmmount = 90;

    private Rigidbody2D rb2d;
    LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        mask = LayerMask.GetMask("Default");
    }


    // Update is called once per frame
    void Update()
    {
        MovePlayer();
     

    }

    void MovePlayer()
    {

        newPosition = rb2d.position;



        // Move right
        if (Input.GetKeyDown(moveRightKey))
        {
            bool canMove = true;
            newPosition += Vector2.right * sidewaysSpeed;

            foreach (Transform child in transform)
            {
                Vector2 childPosition = child.position;

                RaycastHit2D hit = Physics2D.Raycast(childPosition, Vector2.right, 1.5f, mask);

                float distance = Mathf.Abs(hit.point.x - childPosition.x);

                
                if (distance == 1)
                {
                    Debug.Log("RAYHIT");
                    
                }
            }
            if (canMove)
            {
                rb2d.position = newPosition;
            }

            checkIfInbounds();
        }
        // Move left
        if (Input.GetKeyDown(moveLeftKey))
        {
            bool canMove = true;
            newPosition += Vector2.left * sidewaysSpeed;
            
            foreach (Transform child in transform)
            {
                Vector2 childPosition = child.position;

                RaycastHit2D hit = Physics2D.Raycast(childPosition, Vector2.left, 1.5f, mask);

                float distance = Mathf.Abs(hit.point.x - childPosition.x);

                
                if (distance == 1)
                {
                    Debug.Log("RAYHIT");
                    
                }
                
            }
            if (canMove)
            {
                rb2d.position = newPosition;
            }
          
            checkIfInbounds();
        }
        // Rotate Up
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.rotation *= Quaternion.Euler(0, 0, rotationAmmount);
            rb2d.position = newPosition;
            checkIfInbounds();
        }
        // Rotate Down
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.rotation *= Quaternion.Euler(0, 0, -rotationAmmount);
            rb2d.position = newPosition;
            checkIfInbounds();
        }

     
    }

    void checkIfInbounds()
    {

        foreach (Transform child in transform)
        {
            // Get the world position of each child object
            Vector2 childPosition = child.position;

      


            if (childPosition.x > 9)
            {
                newPosition += Vector2.left * sidewaysSpeed;
                transform.position = newPosition;
            }
            else if (childPosition.x < -9)
            {
                newPosition += Vector2.right * sidewaysSpeed;
                transform.position = newPosition;
            }
       

        }
        
    }


}
