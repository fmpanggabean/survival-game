using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Life : MonoBehaviour
{
    public TMP_Text lifeDisplay;
    int life;
    // Start is called before the first frame update
    void Start()
    {
        life = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))//anggap sudah ada enemy
        {
            life--;
            lifeDisplay.text = "Life: " + life;
            if(life <= 0)
            {
                SceneManager.LoadScene("GameOver");//anggap sudah ada scene GameOver
            }
        }
    }
}
