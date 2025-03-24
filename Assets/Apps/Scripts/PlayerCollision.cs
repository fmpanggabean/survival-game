using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private CameraShake cameraShake;

    void Start()
    {
        cameraShake = FindObjectOfType<CameraShake>();
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            cameraShake.Shake();  // Trigger Camera Shake
            Debug.Log("Player hit enemy!");
        }
    }
}
