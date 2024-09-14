using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Work : MonoBehaviour
{
    public static event Action Completed;
    
    public Order[] Orders => availableOrders.ToArray();
    
    [SerializeField] private MailBox[] deliveryTargets;
    private List<Order> availableOrders; 

    private void Awake()
    {
        GenerateOrders();
    }

    private void GenerateOrders()
    {
        availableOrders = new List<Order>();
        
        for (int i = 0; i < deliveryTargets.Length; i++)
        {
            Order order = new Order(deliveryTargets[i]);
            availableOrders.Add(order);
            order.Completed += OnOrderCompleted;
        }
    }

    private void OnOrderCompleted(Order order)
    {
        availableOrders.Remove(order);

        if (availableOrders.Count == 0)
        {
            Completed?.Invoke();
        }
    }
    
    public Order GetRandomOrder()
    {
        if (availableOrders.Count > 0)
        {
            int randomIndex = Random.Range(0, availableOrders.Count);
            Order order = availableOrders[randomIndex];
            return order;
        }
        else
        {
            Debug.Log("No available orders!");
            return null;
        }
    }
}

public class Order
{
    public event Action<Order> Completed;
    public MailBox DeliveryTarget { get; private set; }

    public Order(MailBox deliveryTarget)
    {
        DeliveryTarget = deliveryTarget;
        DeliveryTarget.PackageDelivered += CompleteOrder;
    }
    
    public void CompleteOrder()
    {
        DeliveryTarget.PackageDelivered -= CompleteOrder;
        Completed?.Invoke(this);
    }
    public override string ToString()
    {
        return $"Order to {DeliveryTarget}";
    }
}
