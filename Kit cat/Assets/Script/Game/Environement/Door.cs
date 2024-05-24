using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public BoxCollider2D col;
    public void Open()
    {
        col.isTrigger = true;
    }
}