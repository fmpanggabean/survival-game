using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour, IPoolObject
{
    public int hp = 100;
    [SerializeField] private Vector3 position;
    private Movement movement;

    public void Activate()
    {
        transform.localPosition = Vector2.zero;
        gameObject.SetActive(true);
        Debug.Log("active");
    }

    public void Deactivate()
    {
        transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        movement = GetComponent<Movement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hp <= 0)
        {
            hp = 100;
            Deactivate();
        }
        else if(hp > 0)
        {
            transform.localScale = Vector3.MoveTowards(transform.localScale, new Vector3(0.2f, 0.2f, 0.2f), Time.deltaTime * 1.5f);
            position = GameObject.Find("Player").transform.position;
            movement.lastDirection = (position - transform.position).normalized;
            if (Vector2.Distance(transform.position, position) > 2f)
            {
                movement.SetDirection((position-transform.position).normalized);
            }
            else
            {
                movement.SetDirection(Vector2.zero);
            }
            //transform.position = Vector3.MoveTowards(transform.position, position , Time.deltaTime * 5f);
        }
    }
}
