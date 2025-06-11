using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1; 

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerCoinCollector coinCollector = other.GetComponent<PlayerCoinCollector>();
            if (coinCollector != null)
            {
                coinCollector.AddCoins(coinValue);
            }

            Destroy(gameObject); 
    }
}
