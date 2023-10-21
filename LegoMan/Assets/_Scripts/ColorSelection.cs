using System;
using UnityEngine;

public class ColorSelection : MonoBehaviour
{
    [SerializeField] private Color color;

    private void Awake()
    {
        if (TryGetComponent(out SpriteRenderer r))
        {
            color = r.color;
        }
    }

    public Color GetColor()
    {
        return color;
    }
}
