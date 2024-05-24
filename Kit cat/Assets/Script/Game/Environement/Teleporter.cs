using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public Player_mouvement PlayMouv;
    public Transform Destination;
    /*public void Awake()
    {
        PlayMouv = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_mouvement>();
    }*/

    public void OnTriggerEnter2D(Collider2D collision)
    {
        print("hi");
        if (collision.CompareTag("Player"))
        {
            PlayMouv.Teleport(Destination);
        }
    }
}

