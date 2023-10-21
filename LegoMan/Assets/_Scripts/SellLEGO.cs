using UnityEngine;

public class SellLEGO : MonoBehaviour
{
    [SerializeField] private Transform legoShop;
    [SerializeField] private Transform legoSell;
    private bool _isShopActivated;

    private void Update()
    {
        if (GameManager.Instance.isHolding)
        {
            ActivateShop();
        }
        else
        {
            DeActivateShop();
        }
    }

    private void ActivateShop()
    {
        if (_isShopActivated) return;
        legoSell.gameObject.SetActive(true);
        legoShop.position -= new Vector3(0, 10, 0);
        _isShopActivated = true;
    }

    private void DeActivateShop()
    {
        if (!_isShopActivated) return;
        legoShop.position += new Vector3(0, 10, 0);
        legoSell.gameObject.SetActive(false);
        _isShopActivated = false;
    }

    public void SellLego(Component sender, object price)
    {
        GameManager.Instance.balance += (int)price;
        Destroy(sender.gameObject);
    }
}
