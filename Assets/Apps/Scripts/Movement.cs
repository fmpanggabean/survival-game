using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class Movement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float finalSpeed;
    private Rigidbody2D rb2d;
    private Vector2 lastDirection;
    List<Buff> buffs = new List<Buff>();

    public float dashSpeed = 15f;
    public float dashTime = 0.2f;
    public float dashCooldown = 1f;
    public bool moveDash = false;
    private bool isDashing = false;
    private bool canDash = true;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        lastDirection = new Vector2();
    }

    private void Start()
    {
        buffs.Add(new Buff(1.1f, 30));
        buffs.Add(new Buff(1.1f, 30));
    }

    public void SetDirection(Vector2 direction)
    {
        finalSpeed = speed * GetAllBuffModifier();
        rb2d.velocity = direction * finalSpeed;
    }

    private float GetAllBuffModifier()
    {
        float modifier = 1;
        foreach(Buff b in buffs)
        {
            modifier *= b.modifier;
        }
        return modifier;
    }

    public void SetDirection(CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed || ctx.phase == InputActionPhase.Canceled)
        {
            Vector2 direction = ctx.ReadValue<Vector2>();

            if (ctx.phase == InputActionPhase.Performed)
            {
                lastDirection = direction;

                canDash = true;
                moveDash = true;
            }
            else if(ctx.phase == InputActionPhase.Canceled)
            {
                canDash = false;
                moveDash = false;
            }

            SetDirection(direction);
        }

    }

    public Vector2 GetLastDirection() => lastDirection;

    public void OnDash(InputAction.CallbackContext ctx)
    {
        if (ctx.started && canDash)
        {
            StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        canDash = false;

        rb2d.velocity = lastDirection * dashSpeed;

        yield return new WaitForSeconds(dashTime);
        if (moveDash == true)
        {
            rb2d.velocity = lastDirection * finalSpeed;
        }
        else if (moveDash == false)
        {
            rb2d.velocity = lastDirection * 0f;
        }

        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
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