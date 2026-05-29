using UnityEngine;
using UnityEngine.InputSystem;
public class Character2DContoller : MonoBehaviour
{
    public HealthbarBehaviour Healthbar;
    public float Hitpoints = 5;
    public float MaxHitpoints = 5;
    public float movementSpeed = 1f;
    public float movementH = 0;
    public float movementV = 0;
    
    private Rigidbody2D rb;
    private bool isGrounded = true;

    public ProjectileBehaviour ProjectilePrefab;
    public Transform LaunchOffset;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Hitpoints = MaxHitpoints;
        Healthbar.SetHealth(Hitpoints, MaxHitpoints);
    }

    private void Update()
    {
        if (Keyboard.current.leftArrowKey.isPressed && Keyboard.current.upArrowKey.isPressed || Keyboard.current.aKey.isPressed && Keyboard.current.wKey.isPressed)
        {
            movementH = -1;
            movementV = 1;
        }

        else if (Keyboard.current.leftArrowKey.isPressed && Keyboard.current.downArrowKey.isPressed || Keyboard.current.aKey.isPressed && Keyboard.current.sKey.isPressed)
        {
            movementH = -1;
            movementV = -1;
        }

        else if (Keyboard.current.rightArrowKey.isPressed && Keyboard.current.upArrowKey.isPressed || Keyboard.current.dKey.isPressed && Keyboard.current.wKey.isPressed)
        {
            movementH = 1;
            movementV = 1;
        }

        else if (Keyboard.current.rightArrowKey.isPressed && Keyboard.current.downArrowKey.isPressed || Keyboard.current.dKey.isPressed && Keyboard.current.sKey.isPressed)
        {
            movementH = 1;
            movementV = -1;
        }

        else if (Keyboard.current.leftArrowKey.isPressed || Keyboard.current.aKey.isPressed)
        {
            movementH  = -1f;
            movementV = 0;
        }

        else if (Keyboard.current.rightArrowKey.isPressed || Keyboard.current.dKey.isPressed)
        {
            movementH = 1f;
            movementV = 0;
        }
        else if (Keyboard.current.upArrowKey.isPressed || Keyboard.current.wKey.isPressed)
        {
            movementV  = 1f;
            movementH = 0;
        }
        
        else if (Keyboard.current.downArrowKey.isPressed || Keyboard.current.sKey.isPressed)
        {
            movementV = -1f;
            movementH = 0;
        }
        else
        {
            movementH  = 0f;
            movementV = 0f;
        }

        Vector2 move = new Vector2(movementH, movementV) * movementSpeed * Time.deltaTime;
        transform.position += new Vector3(move.x, move.y, 0);

        if(!Mathf.Approximately(0,movementH))
            transform.rotation = movementH > 0 ? Quaternion.Euler(0,180,0) : Quaternion.identity;

        if(Mouse.current.leftButton.wasPressedThisFrame)
        {
            Instantiate(ProjectilePrefab, LaunchOffset.position, transform.rotation);
        }

    }
    // Rigidbody tem um valor de update diferente do normal

    public void TakeHit(float damage)
    {
        Hitpoints -= damage;
        Healthbar.SetHealth(Hitpoints, MaxHitpoints);
        if (Hitpoints <= 0)
        {
            Destroy(gameObject);
        }
    }
}
