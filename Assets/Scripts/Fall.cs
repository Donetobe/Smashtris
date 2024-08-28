using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fall : MonoBehaviour
{
    public float fallSpeed = 1f;

    public float sidewaysSpeed = 1;

    private KeyCode moveRightKey = KeyCode.RightArrow; // Key to move right
    private KeyCode moveLeftKey = KeyCode.LeftArrow; // Key to move left
    [SerializeField] private float rotationAmmount = 90;

    private Rigidbody2D rb2d;
    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        

        rb2d.MovePosition(rb2d.position + new Vector2(0, -fallSpeed * Time.deltaTime));

        Vector2 newPosition = transform.position;

        // Move right
        if (Input.GetKeyDown(moveRightKey))
        {
            newPosition += Vector2.right * sidewaysSpeed;
        }
        // Move left
        if (Input.GetKeyDown(moveLeftKey))
        {
            newPosition += Vector2.left * sidewaysSpeed;
        }

        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.rotation *= Quaternion.Euler(0, 0, rotationAmmount);
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.rotation *= Quaternion.Euler(0, 0, -rotationAmmount);
        }

        // Apply the new position
        transform.position = newPosition;
    }
}
