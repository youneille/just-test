using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chatCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D colision)
    {
        if (colision.CompareTag("Player"))
        {
            print("colision");
            GameObject.FindObjectOfType<chat>().spawnCat();
            Destroy(gameObject);
        }
    }
}
