using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{

    public Rigidbody2D myRigidBody;
    public float moveSpeed;
    public ProjectileBehaviour ProjectilePrefab;
    public Transform LaunchOffset;
    Vector2 moveDir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        InputManagment();

        if (!Mathf.Approximately(0, moveSpeed))
            transform.rotation = moveSpeed > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Instantiate(ProjectilePrefab, LaunchOffset.position, transform.rotation);
        }

    }

    void FixedUpdate()
    {
        Move();
    }

    void InputManagment()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDir = new Vector2(moveX, moveY).normalized;
    }

    void Move()
    {
        myRigidBody.linearVelocity = new Vector2(moveDir.x * moveSpeed, moveDir.y * moveSpeed);
    }
}
