using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 velocity;
    
    public HealthbarBehaviour Healthbar;
    public float Hitpoints = 5;
    public float MaxHitpoints = 5;
    public ProjectileBehaviour ProjectilePrefab;
    public Transform LaunchOffset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = new Vector2(moveInput.x, moveInput.y);
        controller.Move(move * speed * Time.deltaTime);

        transform.position += new Vector3(moveInput.x, 0, 0) * Time.deltaTime * speed;

        if (!Mathf.Approximately(0, moveInput.x))
            transform.rotation = moveInput.x > 0 ? Quaternion.Euler(0, 180, 0) : Quaternion.identity;

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            Instantiate(ProjectilePrefab, LaunchOffset.position, transform.rotation);
        }
    }

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
