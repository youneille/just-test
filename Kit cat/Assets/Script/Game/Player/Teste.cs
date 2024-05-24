
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Teste : MonoBehaviour
{
    //Classe public
    public float movingSpeed = 5;
    [SerializeField] int jumpPower = 5;
    public Transform groundCheck;
    public LayerMask groundLayer;

    //Classe private
    private Rigidbody2D rb;

    //Fonctions
    Vector2 CheckpointPos = new Vector2(5, 5);
    bool isGrounded;





    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        CheckpointPos = transform.position;
    }

    //Regarde si le joueur rentre en collision avec une zone de Respawn
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn"))
        {
            Reset();
        }
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCapsule(groundCheck.position, new Vector2(0.5f, 0.1f), CapsuleDirection2D.Horizontal, 0, groundLayer);
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }


        float horizontalMouvement = Input.GetAxis("Horizontal") * movingSpeed;
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
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref targetVelocity, .0f);
        FlipCharacter();
    }
    public void FlipCharacter()
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


    public void Reset()
    {
        transform.position = new Vector2(CheckpointPos.x, CheckpointPos.y);
    }
}