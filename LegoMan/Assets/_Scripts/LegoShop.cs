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
    private LegoDragAndDrop _instantiatedDragAndDrop;

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
        _instantiatedDragAndDrop.OnMouseUp();
        _instantiatedLego = null;
    }

    private void OnMouseDrag()
    {
        if (_instantiatedLego == null) return;
        _instantiatedDragAndDrop.OnMouseDrag();
    }

    private void BuyLego()
    {
        _instantiatedLego = Instantiate(lego, parent);
        _instantiatedDragAndDrop = _instantiatedLego.GetComponent<LegoDragAndDrop>();
        var mousePosition = Camera.main!.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;
        _instantiatedLego.transform.position = mousePosition;
        _instantiatedDragAndDrop.Init(_gridManager, _mainCam, _playersCollider);
        GameManager.Instance.balance -= price;
    }
}