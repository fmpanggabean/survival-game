using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

[RequireComponent(typeof(Movement))]
public class sprintingskill : MonoBehaviour
{
    [SerializeField] private float sprintMultiplier = 1.5f;
    [SerializeField] private float sprintDuration = 2f;
    [SerializeField] private float sprintCooldown = 5f;

    private Movement movement;
    private bool isSprinting = false;
    private bool canSprint = true;

    private void Awake()
    {
        movement = GetComponent<Movement>();
    }

    public void OnSprint(CallbackContext ctx)
    {
        if (ctx.started && canSprint)
        {
            StartCoroutine(SprintRoutine());
        }
    }

    private IEnumerator SprintRoutine()
    {
        isSprinting = true;
        canSprint = false;

        float originalSpeed = movement.speed;
        movement.speed *= sprintMultiplier;
        movement.SetDirection(movement.GetLastDirection());

        yield return new WaitForSeconds(sprintDuration);

        movement.speed = originalSpeed;
        movement.SetDirection(movement.GetLastDirection());

        isSprinting = false;

        yield return new WaitForSeconds(sprintCooldown);
        canSprint = true;
    }
}
