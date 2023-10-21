using System;
using UnityEngine;

public class LegoDragAndDrop : MonoBehaviour
{
    private enum LegoType
    {
        Odd,
        Even,
        Four
    }

    [SerializeField] private LegoType legoType;
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private GameEvent sell;
    public int price;
    private Camera _camera;
    private Vector3 _mouseOffset;
    private GameObject _grids;
    private GridManager _gridManager;
    private Vector3 _initialPosition;
    private CapsuleCollider2D _playerCapsuleCollider;

    private bool _isDragging;

    public void Init(GameObject gridManager, Camera mainCam, CapsuleCollider2D playersCollider)
    {
        _grids = gridManager;
        _camera = mainCam;
        _playerCapsuleCollider = playersCollider;
        _gridManager = _grids.GetComponent<GridManager>();
        
        OnMouseUp();
        OnMouseDown();
    }
    
    private void OnMouseDown()
    {
        GameManager.Instance.isHolding = true;
        _isDragging = true;
        var position = transform.position;
        _grids!.SetActive(true);
        _initialPosition = position;
        _gridManager.SelectLegoPiece(gameObject, legoType.ToString(), _initialPosition);
        Physics2D.IgnoreCollision(boxCollider2D, _playerCapsuleCollider, true);
        _mouseOffset = position - GetMousePos();
    }

    private void Update()
    {
        if (!_isDragging) return;
        transform.position = GetMousePos() + _mouseOffset;
    }

    public void OnMouseUp()
    {
        GameManager.Instance.isHolding = false;
        _isDragging = false;
        Physics2D.IgnoreCollision(boxCollider2D, _playerCapsuleCollider, false);


        
        var bounds1 = boxCollider2D.bounds;
        var bounds = bounds1;
        var newCenter = new Vector3(bounds.center.x, bounds.center.y - 0.015f, 0);
        var size =
            bounds1.size.x > 3.5f
                ? new Vector3(bounds.size.x * 0.575f, bounds.size.y * 0.475f, 0)
                : new Vector3(bounds.size.x * 0.75f, bounds.size.y * 0.475f, 0);

        var otherColliders = Physics2D.OverlapBoxAll(newCenter, size, 0f);
        
        _grids!.SetActive(false);

        foreach (var otherCollider in otherColliders)
        {
            if (otherCollider.CompareTag("Sell"))
            {
                sell.Raise(gameObject.transform ,price);
            }
            
            if (otherCollider.CompareTag("Ground") || otherCollider.CompareTag("Player") ||
                (otherCollider.CompareTag("Brick") && otherCollider.gameObject != gameObject))
            {
                transform.position = _initialPosition;
                break;
            }

            _gridManager.SnapToGrid();
        }
    }

    private Vector3 GetMousePos()
    {
        var mousePos = _camera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }
    
}