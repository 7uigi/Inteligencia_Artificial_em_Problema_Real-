using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public Rigidbody2D myRigidBody;
    public float moveSpeed;
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
