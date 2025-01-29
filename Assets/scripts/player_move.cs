using System;
using UnityEngine;
using static UnityEditor.Progress;

public class player_move : MonoBehaviour
{
    [SerializeField] int speed;

    Rigidbody2D body;

    [SerializeField]

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
