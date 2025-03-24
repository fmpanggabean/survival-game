using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Movement : MonoBehaviour
{
    [Header("Base Movement")]
    [SerializeField] private float baseSpeed = 5f;
    [SerializeField] private float acceleration = 20f;

    [Header("Dash Settings")]
    [SerializeField] private float dashSpeedMultiplier = 1.5f;
    [SerializeField] private float dashDuration = 0.5f;
    [SerializeField] private float dashCooldown = 1f;

    private Rigidbody2D rb2d;
    private Vector2 moveInput;
    private Vector2 currentVelocity;

    private float currentMaxSpeed;
    private bool isDashing = false;
    private bool canDash = true;

    List<Buff> buffs = new List<Buff>();

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        currentVelocity = Vector2.zero;
    }

    private void Start()
    {
        currentMaxSpeed = baseSpeed;
        buffs.Add(new Buff(1.1f, 30));
        buffs.Add(new Buff(1.1f, 30));
    }

    void Update()
    {

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Dash();
        }


        Vector2 targetVelocity = moveInput.normalized * currentMaxSpeed * GetAllBuffModifier();
        currentVelocity = Vector2.MoveTowards(currentVelocity, targetVelocity, acceleration * Time.deltaTime);
        rb2d.velocity = currentVelocity;


        if (moveInput == Vector2.zero && currentVelocity.magnitude < 0.1f)
        {
            currentVelocity = Vector2.zero;
            rb2d.velocity = Vector2.zero;
        }
    }

    public void SetDirection(Vector2 direction)
    {
        moveInput = direction;
    }

    public void SetDirection(CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed || ctx.phase == InputActionPhase.Canceled)
        {
            moveInput = ctx.ReadValue<Vector2>();
        }
    }

    private float GetAllBuffModifier()
    {
        float modifier = 1f;
        foreach (Buff b in buffs)
        {
            modifier *= b.modifier;
        }
        return modifier;
    }

    public void Dash()
    {
        if (canDash)
        {
            Debug.Log("Dash (speed boost) triggered!");
            StartCoroutine(DashBoostCoroutine());
        }
        else
        {
            Debug.Log("Dash on cooldown.");
        }
    }

    private IEnumerator DashBoostCoroutine()
    {
        canDash = false;
        isDashing = true;

        float originalSpeed = baseSpeed;
        currentMaxSpeed = baseSpeed * dashSpeedMultiplier;
        Debug.Log($"Speed boosted to {currentMaxSpeed}");

        yield return new WaitForSeconds(dashDuration);

        currentMaxSpeed = baseSpeed;
        isDashing = false;
        Debug.Log("Dash boost ended.");

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
        Debug.Log("Dash ready again.");
    }

    public void SetSpeed(float newSpeed)
    {
        baseSpeed = newSpeed;
        currentMaxSpeed = baseSpeed;
        Debug.Log($"Base speed changed to: {baseSpeed}");
    }

    public void OnDash(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            Dash();
        }
    }

    public Vector2 GetLastDirection()
    {
        return moveInput;
    }
}

public class Buff
{
    public float modifier;
    public float duration;

    public Buff(float modifier, float duration)
    {
        this.modifier = modifier;
        this.duration = duration;
    }
}
