using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float sprintMultiplier = 1.5f;
    public KeyCode sprintKey = KeyCode.LeftShift;

    private Rigidbody2D rb;
    private Vector2 movement;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement.Normalize();

    void FixedUpdate()
    {
        float currentSpeed = moveSpeed;

        
        if (Input.GetKey(sprintKey))
        {
            currentSpeed *= sprintMultiplier;
        }
        rb.velocity = movement * currentSpeed;
    }
}
