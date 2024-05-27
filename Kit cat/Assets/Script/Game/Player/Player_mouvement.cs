using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    // Move player in 2D space
    public float maxSpeed = 3.4f;
    public float jumpHeight = 6.5f;
    public float gravityScale = 1.5f;

    // Animation and controls parameters
    bool facingRight = true;
    float moveDirection = 0;
    bool isGrounded = false;
    [SerializeField] Door door;
    [SerializeField] private float rayDistance;

    // Components
    Transform t;
    Rigidbody2D r2d;
    CapsuleCollider2D mainCollider;
    private Animator animController;
    [SerializeField] Transform PlayerGrabPosition;
    public GameObject objectAttrapper;
    public Transform Backup;
    [SerializeField] Vector2 CheckpointPos = new(5, 5);

    // Start is called before the first frame update
    void Start()
    {
        t = GetComponent<Transform>();
        r2d = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<CapsuleCollider2D>();
        animController = GetComponent<Animator>();

        r2d.freezeRotation = true;
        r2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        r2d.gravityScale = gravityScale;
        facingRight = t.localScale.x > 0;
    }

    // Update is called once per frame
    void Update()
    {

        // Movement controls
        if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && (isGrounded || Mathf.Abs(r2d.velocity.x) > 0.01f))
        {
            moveDirection = Input.GetKey(KeyCode.A) ? -1 : 1;
        }
        else
        {
            if (isGrounded || r2d.velocity.magnitude < 0.01f)
            {
                moveDirection = 0;
            }
        }

        // Change facing direction
        if (moveDirection != 0)
        {
            animController.SetBool("iswalking", true);
            if (moveDirection > 0 && !facingRight)
            {
                facingRight = true;
                t.localScale = new Vector3(Mathf.Abs(t.localScale.x), t.localScale.y, transform.localScale.z);
            }

            if (moveDirection < 0 && facingRight)
            {
                facingRight = false;
                t.localScale = new Vector3(-Mathf.Abs(t.localScale.x), t.localScale.y, t.localScale.z);
            }
        }
        else
            animController.SetBool("iswalking", false);
        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            r2d.velocity = new Vector2(r2d.velocity.x, jumpHeight);
            animController.SetTrigger("Jump");
        }

        RaycastHit2D hitInfo = Physics2D.Raycast(PlayerGrabPosition.position, transform.right, rayDistance);


        if (hitInfo.collider != null && hitInfo.collider.gameObject.CompareTag("Object"))
        {
            if (Input.GetButtonDown("Action") && objectAttrapper == null)
            {
                print("attraper");
                objectAttrapper = hitInfo.collider.gameObject;
                objectAttrapper.GetComponent<Rigidbody2D>().isKinematic = true;
                objectAttrapper.transform
                    .SetParent(transform); //definir une velocite et la mettre a jour a chaque update, ?vite le traversage de mur, la v?locit? doit etre identique a celle du joueur
                objectAttrapper.transform.localPosition = new Vector2(
                    Backup.localPosition.x * objectAttrapper.transform.lossyScale.x,
                    objectAttrapper.transform.localPosition.y);

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

    void FixedUpdate()
    {
        Bounds colliderBounds = mainCollider.bounds;
        float colliderRadius = mainCollider.size.x * 0.4f * Mathf.Abs(transform.localScale.x);
        Vector3 groundCheckPos = colliderBounds.min + new Vector3(colliderBounds.size.x * 0.5f, colliderRadius * 0.9f, 0);
        // Check if player is grounded
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckPos, colliderRadius);
        //Check if any of the overlapping colliders are not player collider, if so, set isGrounded to true
        isGrounded = false;
        if (colliders.Length > 0)
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i] != mainCollider)
                {
                    isGrounded = true;
                    break;
                }
            }
        }

        // Apply movement velocity
        r2d.velocity = new Vector2((moveDirection) * maxSpeed, r2d.velocity.y);

        // Simple debug
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(0, colliderRadius, 0), isGrounded ? Color.green : Color.red);
        Debug.DrawLine(groundCheckPos, groundCheckPos - new Vector3(colliderRadius, 0, 0), isGrounded ? Color.green : Color.red);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Respawn"))
        {
            transform.position = new Vector2(CheckpointPos.x, CheckpointPos.y);
        }

        if (collision.CompareTag("Key"))
        {
            door.Open();
        }

        if (collision.CompareTag("Checkpoint"))
        {
            CheckpointPos = transform.position;
        }
    }
}