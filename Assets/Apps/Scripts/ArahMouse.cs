using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArahMouse : MonoBehaviour
{
    public GameObject BulletPrefab;
    public Transform Aim;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GameObject Bullet = Instantiate(BulletPrefab, Aim.position, Aim.rotation);
            Rigidbody2D RbBullet = Bullet.GetComponent<Rigidbody2D>();
            RbBullet.AddForce(Aim.right * speed, ForceMode2D.Impulse);
        }


    }
}
