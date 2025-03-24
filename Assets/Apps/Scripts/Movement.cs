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
    private Vector2 dashDirection;
    private bool isDashing;
    private bool canDash;
    List<Buff> buffs = new List<Buff>();

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        lastDirection = new Vector2();
        isDashing = false;
        canDash = true;
    }

    private void Start()
    {
        buffs.Add(new Buff(1.1f, 30));
        buffs.Add(new Buff(1.1f, 30));
    }

    public void PerformDash(CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed && canDash && !isDashing)
        {
            dashDirection = lastDirection.normalized;
            if (dashDirection.magnitude < 0.1f) return;
            StartCoroutine(Dash(0.2f, 10f, 1f));
        }
    }

    private IEnumerator Dash(float dashDuration, float dashSpeed, float dashCooldown)
    {
        isDashing = true;
        canDash = false;
        float startTime = Time.time;
        while (Time.time < startTime + dashDuration)
        {
            transform.position += new Vector3(dashDirection.x, dashDirection.y, 0) * dashSpeed * Time.deltaTime;
            yield return null;
        }
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
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
            }

            SetDirection(direction);
        }

    }

    public Vector2 GetLastDirection() => lastDirection;
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