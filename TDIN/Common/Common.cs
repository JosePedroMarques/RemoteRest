﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class Order
{
    int id;
    static int ID = 0;
    public Order()
    {
        id = ID++;
        state = OrderState.Waiting;
        desc = "default order";
        tableID = 0;
        qt = 10;
        cookDestination = CookType.Bar;
        price = 10;

    }

  
    public int Id
    {
        get { return id; }
        set { id = value; }
    }
    OrderState state;

    public OrderState State
    {
        get { return state; }
        set { state = value; }
    }
    String desc;

    public String Description
    {
        get { return desc; }
        set { desc = value; }
    }
    int qt;

    public int Qt
    {
        get { return qt; }
        set { qt = value; }
    }
    int tableID;

    public int TableID
    {
        get { return tableID; }
        set { tableID = value; }
    }
    CookType cookDestination;

    public CookType CookDestination
    {
        get { return cookDestination; }
        set { cookDestination = value; }
    }
    double price;

    public double Price
    {
        get { return price; }
        set { price = value; }
    }

    public override string ToString()
    {
        return "Order id: " + id + "\nDescription: " + desc + "\nState : " + state + "\nQuantity: " + qt + "\nPrice :" + price + "\nTable : " + tableID + "\nDestination: " + cookDestination + "\n";
    }
}

public enum CookType
{
    Kitchen, Bar
}

public enum OrderState
{
    Waiting, InProgress, Ready
}

public interface IOrderManager
{
    event newOrderBarDelegate newOrderBarEvent;
    event newOrderKitchenDelegate newOrderKitchenEvent;
    event orderChangedDelegate orderChangedEvent;
    void addOrder(Order o);
    void changeState(int orderID, OrderState newState);
}
public delegate void newOrderBarDelegate(Order o);
public delegate void orderChangedDelegate(Order o);
public delegate void newOrderKitchenDelegate(Order o);


public class Repeater : MarshalByRefObject
{
    public event newOrderBarDelegate newOrderBar;
    public event newOrderKitchenDelegate newOrderKitchen;
    public event orderChangedDelegate orderChanged;
    public override object InitializeLifetimeService()
    {
        return null;
    }

    public void newOrderBarRepeater(Order o)
    {
        if (newOrderBar != null)
            newOrderBar(o);
    }
    public void newOrderKitchenRepeater(Order o)
    {
        if (newOrderKitchen != null)
            newOrderKitchen(o);
    }
    public void orderChangedRepeater(Order o)
    {
        if (orderChanged != null)
            orderChanged(o);
    }

}