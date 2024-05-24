using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuivieCamera : MonoBehaviour
{
    public Transform Position;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Position.position.x,Position.position.y,-1); 
    }
}
