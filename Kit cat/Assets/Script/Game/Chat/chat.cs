using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;

public class chat : MonoBehaviour
{

    [SerializeField] GameObject[] chatPrefab;
    [SerializeField] GameObject[] spawnsChat;
    [SerializeField] float nbrChat;
    int currentSpawn = 0;
    public GameObject canvas;

    // Start is called before the first frame update
    void Start()
    {
        spawnCat();

    }
    public void AfficherCanevas()
    {
        bool isEnable = true;   
        canvas.SetActive(isEnable);
        
        
    }

    public void spawnCat()
    {
        Vector3 spawnPosition = spawnsChat[currentSpawn].transform.position;
        GameObject gameObject = Instantiate(chatPrefab[Random.Range(0, chatPrefab.Length)], spawnPosition, Quaternion.identity);
        currentSpawn = (currentSpawn + 1) % spawnsChat.Length;
        AfficherCanevas();  
    }


    // Update is called once per frame
}
