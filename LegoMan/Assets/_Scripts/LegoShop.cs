using System;
using System.Linq;
using UnityEngine;

public class LegoShop : MonoBehaviour
{
    [SerializeField] private GameObject lego;
    [SerializeField] private int price;
    [SerializeField] private Transform parent;

    private GameObject _instantiatedLego;
    private Camera _mainCam;
    private GameObject _gridManager;
    private CapsuleCollider2D _playersCollider;

    private void Awake()
    {
        _mainCam = Camera.main;
        _gridManager = GameObject.FindWithTag("GridManager").gameObject;
        _playersCollider = GameObject.FindWithTag("Player").GetComponent<CapsuleCollider2D>();
    }

    private void OnMouseDown()
    {
        if (!(GameManager.Instance.balance >= price)) return;

        BuyLego();
    }

    private void OnMouseUp()
    {
        if (_instantiatedLego == null) return;
        _instantiatedLego.GetComponent<LegoDragAndDrop>().OnMouseUp();
        _instantiatedLego = null;
    }

    private void BuyLego()
    {
        _instantiatedLego = Instantiate(lego, parent);
        var mousePosition = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        _instantiatedLego.transform.position = mousePosition;
        _instantiatedLego.GetComponent<LegoDragAndDrop>().Init(_gridManager, _mainCam, _playersCollider);
        GameManager.Instance.balance -= price;
    }
}