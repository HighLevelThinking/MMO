using System;
using UnityEngine;
using static UnityEditor.Progress;

public class player_move : MonoBehaviour
{
    [SerializeField] int speed;

    Rigidbody2D body;

    public int[,] inventory = new int[23,2]; // 9 x 3 invetory

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        body.linearVelocityX = Input.GetAxis("Horizontal") * speed;
        body.linearVelocityY = Input.GetAxis("Vertical") * speed;
    }

    public void giveItem(int item)
    {
        for (int i = 0; i < inventory.GetLength(0); i++)
        {
            if (inventory[i,0] == item)
            {
                inventory[i,1] ++;
                break;
            }
        }
        for (int i = 0; i < inventory.GetLength(0); i++)
        {
            if (inventory[i, 0] == 0)
            {
                inventory[i, 0] = item;
                inventory[i, 1]++;
                Debug.Log(i);
                break;
            }
        }
    }
}
