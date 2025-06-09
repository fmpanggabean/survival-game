using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (target != null)
        {
            offset = transform.position - target.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
}
