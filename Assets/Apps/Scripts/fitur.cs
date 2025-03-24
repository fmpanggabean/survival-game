using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FootstepSound : MonoBehaviour
{
    public AudioSource audioSource; // Assign an AudioSource component
    public AudioClip[] grassSteps;
    public AudioClip[] woodSteps;
    public AudioClip[] stoneSteps;

    public float stepInterval = 0.5f; // Time between steps
    private bool canPlayStep = true;

    private void Update()
    {
        if (IsWalking() && canPlayStep)
        {
            PlayFootstep();
            StartCoroutine(FootstepCooldown());
        }
    }

    private bool IsWalking()
    {
        return Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
    }

    private void PlayFootstep()
    {
        string groundTag = GetGroundTag();
        AudioClip[] selectedClips = null;

        switch (groundTag)
        {
            case "Grass": selectedClips = grassSteps; break;
            case "Wood": selectedClips = woodSteps; break;
            case "Stone": selectedClips = stoneSteps; break;
            default: return;
        }

        if (selectedClips != null && selectedClips.Length > 0)
        {
            audioSource.PlayOneShot(selectedClips[Random.Range(0, selectedClips.Length)]);
        }
    }

    private string GetGroundTag()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f))
        {
            return hit.collider.tag;
        }
        return "";
    }

    private IEnumerator FootstepCooldown()
    {
        canPlayStep = false;
        yield return new WaitForSeconds(stepInterval);
        canPlayStep = true;
    }
}