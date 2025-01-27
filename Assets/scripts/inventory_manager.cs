using UnityEngine;

public class inventory_manager : MonoBehaviour
{

    public int[,] inventory = new int[27, 2]; // 9 x 3 invetory

    public bool inventory_showing = false;

    [SerializeField] GameObject cell_owner;
    [SerializeField] GameObject cell;

    [SerializeField] Sprite rock;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < inventory.GetLength(0); i++)
        {
            GameObject current_cell = Instantiate(cell, cell_owner.transform);
            current_cell.transform.position = new Vector3((int)i%9,-(int)i/9);
            current_cell.transform.Find("item").GetComponent<SpriteRenderer>().sprite = inventory[i,0] == 1 ? rock : null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            inventory_showing = !inventory_showing;
            cell_owner.SetActive(inventory_showing);
        }
        if (inventory_showing)
        {
            for (int i = 0; i < inventory.GetLength(0); i++)
            {
                GameObject current_cell = cell_owner.transform.GetChild(i).gameObject;
                current_cell.transform.Find("item").GetComponent<SpriteRenderer>().sprite = inventory[i, 0] == 1 ? rock : null;
            }
        }
        cell_owner.transform.position = new Vector3(-(Screen.width / 2), -(Screen.height / 2));
    }

    public void giveItem(int item)
    {
        for (int i = 0; i < inventory.GetLength(0); i++)
        {
            if (inventory[i, 0] == item)
            {
                inventory[i, 1]++;
                break;
            }
        }
        for (int i = 0; i < inventory.GetLength(0); i++)
        {
            if (inventory[i, 0] == 0)
            {
                inventory[i, 0] = item;
                inventory[i, 1]++;
                break;
            }
        }
    }
}
