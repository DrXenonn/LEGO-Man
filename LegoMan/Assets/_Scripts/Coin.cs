using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int value;
    [SerializeField] private GameEvent trade;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            trade.Raise(value);
            Destroy(gameObject);
        }
    }
}
