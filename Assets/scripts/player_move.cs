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
        for (int i = 0; i < inventory.Length; i++)
        {
            Debug.Log(inventory[i,0]);
            Debug.Log(inventory[i, 1]);
        }
    }

    public void giveItem(int item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i,0] == item)
            {
                inventory[i,1] ++;
                break;
            }
        }
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i, 0] == item)
            {
                inventory[i, 0] = item;
                inventory[i, 1]++;
                break;
            }
        }
    }
}
