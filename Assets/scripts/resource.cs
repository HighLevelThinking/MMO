using UnityEngine;

public class resource : MonoBehaviour
{

    [SerializeField] GameObject player;

    public int type;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)){
            player.GetComponent<player_move>().giveItem(type);
            //Destroy(this.gameObject); comment out untill respawn
        }
    }
}
