using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{

    [SerializeField] Transform PlayerHead;
    [SerializeField] Transform PlayerGrabPosition;
    [SerializeField] float rayDistance;
    public GameObject objectAttrapper;

    public void Update()
    {

        RaycastHit2D hitInfo = Physics2D.Raycast(PlayerGrabPosition.position, transform.right, rayDistance);


        if (hitInfo.collider != null && hitInfo.collider.gameObject.CompareTag("Object"))
        {
                if (Input.GetButtonDown("Action") && objectAttrapper == null)
                {
                    print("attraper");
                    objectAttrapper = hitInfo.collider.gameObject;
                    objectAttrapper.GetComponent<Rigidbody2D>().isKinematic = true;
                    objectAttrapper.transform.SetParent(transform);
                    objectAttrapper.transform.localPosition = new Vector2(PlayerHead.localPosition.x * objectAttrapper.transform.lossyScale.x, objectAttrapper.transform.localPosition.y);
                    
                }

                else if (Input.GetButtonDown("Action") && objectAttrapper)
                {
                    print("lacher");
                    objectAttrapper.GetComponent<Rigidbody2D>().isKinematic = false;
                    objectAttrapper.transform.SetParent(null);
                    objectAttrapper = null;
                }
        }
    }




}
