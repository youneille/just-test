using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public Door door;
    public void Awake()
    {
        door = GameObject.FindGameObjectWithTag("Door").GetComponent<Door>();
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            door.Open();
        }
    }
}