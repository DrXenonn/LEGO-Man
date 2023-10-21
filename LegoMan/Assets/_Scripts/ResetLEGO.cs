using UnityEngine;

public class ResetLEGO : MonoBehaviour
{
    [SerializeField] private GameEvent sellLego;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Brick"))
        {
            sellLego.Raise(other.transform, other.GetComponent<LegoDragAndDrop>().price);
        }
    }
}
