using System;
using UnityEngine;
using static UnityEditor.Progress;

public class player_move : MonoBehaviour
{
    [SerializeField] int speed;

    Rigidbody2D body;

    bool canmove = true;
    KeyCode[] menu_keys = { KeyCode.E };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canmove) 
        {
            body.linearVelocityX = Input.GetAxis("Horizontal") * speed;
            body.linearVelocityY = Input.GetAxis("Vertical") * speed;
        }
        canmove = true;
        foreach (var key in menu_keys)
        {
            if (Input.GetKeyDown(key))
            {
                canmove = false;
                break;
            }
        }
    }

    
}
