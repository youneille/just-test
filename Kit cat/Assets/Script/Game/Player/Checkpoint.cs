using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Checkpoint : MonoBehaviour
{
    public Player_mouvement PlayMouv;
    private void Awake()
    {
        PlayMouv = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_mouvement>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) 
        {
            PlayMouv.UpdateCheckpoint(transform.position);
        }
    }


}
