using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player_mouvement : MonoBehaviour
{
    //Classe public
    public Animator anim;
    public float movingSpeed = 5;
    public int jumpPower = 5;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Transform Backup;
    public Door door;
    public GameObject objectAttrapper;
    [SerializeField] Transform PlayerGrabPosition;
    [SerializeField] float rayDistance;
    //Classe private
    private Rigidbody2D rb;
    //Fonctions
    Vector2 CheckpointPos = new(5, 5);
    bool isGrounded;





    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CheckpointPos = transform.position;
        //door = GameObject.FindGameObjectWithTag("Door").GetComponent<Door>();
    }


//Regarde si le joueur rentre en collision avec une zone de Respawn
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn"))
        {
            Reset();
        }

        if (collision.CompareTag("Key"))
        {
            door.Open();
        }

        if (collision.CompareTag("Checkpoint"))
        {
            UpdateCheckpoint(transform.position);
        }

       /* if (collision.CompareTag("Teleporter"))
        {
            Teleport();
        }*/
    }

// Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.5f, 0.1f), CapsuleDirection2D.Horizontal, 0, groundLayer);
     /*   if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        } */

        float horizontalMouvement = Input.GetAxis("Horizontal") * movingSpeed;

        if (Mathf.Abs(horizontalMouvement) > 0) { anim.SetBool("iswalking", true); Debug.Log("move"); }
        else { anim.SetBool("iswalking", false); }
        

        RaycastHit2D hitInfo = Physics2D.Raycast(PlayerGrabPosition.position, transform.right, rayDistance);


        if (hitInfo.collider != null && hitInfo.collider.gameObject.CompareTag("Object"))
        {
            if (Input.GetButtonDown("Action") && objectAttrapper == null)
            {
                print("attraper");
                objectAttrapper = hitInfo.collider.gameObject;
                objectAttrapper.GetComponent<Rigidbody2D>().isKinematic = true;
                objectAttrapper.transform.SetParent(transform); //definir une velocite et la mettre a jour a chaque update, évite le traversage de mur, la vélocité doit etre identique a celle du joueur
                objectAttrapper.transform.localPosition = new Vector2(Backup.localPosition.x * objectAttrapper.transform.lossyScale.x, objectAttrapper.transform.localPosition.y);

            }

            else if (Input.GetButtonDown("Action") && objectAttrapper)
            {
                print("lacher");
                objectAttrapper.GetComponent<Rigidbody2D>().isKinematic = false;
                objectAttrapper.transform.SetParent(null);
                objectAttrapper = null;
            }
        }
        MovePlayer(horizontalMouvement);
    }
    public void UpdateCheckpoint(Vector2 Pos)
    {
        CheckpointPos = new Vector2(Pos.x, Pos.y);
        Debug.Log(CheckpointPos);
    }

    void MovePlayer(float _horizontalMovement)
    {
        Vector3 targetVelocity = new Vector2(_horizontalMovement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref targetVelocity, 0f);
        FlipCharacter();
    }
    public void FlipCharacter()
    {
        if (objectAttrapper == null)
        {
            if (rb.velocity.x < 0)
            {
                transform.localRotation = Quaternion.Euler(0, 180, 0);
            }
            else if (rb.velocity.x > 0)
            {
                transform.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    public void Teleport(Transform Positionement)
    {
        transform.position = new Vector2(Positionement.position.x, Positionement.position.y);
    }

    public void Reset()
    {
        transform.position = new Vector2(CheckpointPos.x, CheckpointPos.y);
    }
}