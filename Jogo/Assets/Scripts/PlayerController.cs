using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private CharacterController controller;
    private Vector2 moveInput;
    private Vector2 velocity;

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
    }
}
