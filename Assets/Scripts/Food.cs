using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class Food : MonoBehaviour
{
    public delegate void FOnFoodConsumed(Food pile, int remainingQuantity);
    public event FOnFoodConsumed OnFoodConsumed;

    private int quantity = 100;
    [SerializeField] private int consumptionRate = 10;
    [SerializeField] private Slider mBar;
    private void Start()
    {
        GameManager.instance.RegisterFoodPile(this, quantity);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            UpdateQuantity();
            OnFoodConsumed?.Invoke(this, quantity);
        }
    }

    private void UpdateQuantity()
    {
        quantity -= consumptionRate;
        mBar.value = (quantity < 0)? 0 : quantity;
    }
}
