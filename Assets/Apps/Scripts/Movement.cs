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

    private bool canDash = true;
    private InputAct act;
    private Rigidbody2D rb2d;
    private Vector2 lastDirection;
    List<Buff> buffs = new List<Buff>();

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        lastDirection = new Vector2();

        act = new InputAct();
    }

    private void OnEnable()
    {
        act.Gameplay.Enable();
        act.Gameplay.Dash.performed += Dash_performed;
    }

    private void Dash_performed(CallbackContext obj)
    {
        if(canDash)
        {
            float currSpd = speed;
            speed = 15f;
            Debug.Log("Dashing");
            StartCoroutine(dashing(currSpd));
            canDash = false;
        }
    }

    IEnumerator dashing(float originSpd)
    {
        yield return new WaitForSeconds(0.5f);
        speed = originSpd;
        yield return new WaitForSeconds(1f);
        canDash = true;
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