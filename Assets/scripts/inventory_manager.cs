using TMPro;
using UnityEngine;

public class inventory_manager : MonoBehaviour
{

    public int[,] inventory = new int[27, 2]; // 9 x 3 invetory

    public bool inventory_showing = false;

    [SerializeField] GameObject cell_owner;
    [SerializeField] GameObject cell;

    [SerializeField] Sprite rock;

    float height;
    float width;

    [SerializeField] Vector2 pos_scale;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < inventory.GetLength(0); i++)
        {
            GameObject current_cell = Instantiate(cell, cell_owner.transform);
            current_cell.transform.position = new Vector3((int)i%9,-(int)i/9);
            current_cell.transform.Find("item").GetComponent<SpriteRenderer>().sprite = inventory[i,0] == 1 ? rock : null;
            Camera cam = Camera.main;
            height = 2f * cam.orthographicSize;
            width = height * cam.aspect;
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
                Transform current_cell = cell_owner.transform.GetChild(i);
                current_cell.Find("item").GetComponent<SpriteRenderer>().sprite = inventory[i, 0] == 1 ? rock : null;
                GameObject counter = current_cell.Find("Canvas").Find("Count").gameObject;
                counter.GetComponent<TextMeshProUGUI>().text = inventory[i, 1].ToString();
                counter.transform.position = new Vector3(-width / pos_scale.x + (int)i % 9, height / pos_scale.y - (int)i / 9,10);
            }
        }
        cell_owner.transform.position = new Vector3(-width/pos_scale.x, height/ pos_scale.y);
    }

    public void giveItem(int item)
    {
        for (int i = 0; i < inventory.GetLength(0); i++)
        {
            if (inventory[i, 0] == item)
            {
                inventory[i, 1]++;
                return;
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
