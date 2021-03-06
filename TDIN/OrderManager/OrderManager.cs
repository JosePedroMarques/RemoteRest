﻿using System;
using System.Collections;
using System.Linq;
using System.Text;


public class OrderManager:MarshalByRefObject, IOrderManager
{
    Hashtable orders;
    public event newOrderKitchenDelegate newOrderKitchenEvent;
    public event newOrderBarDelegate newOrderBarEvent;
    public event orderChangedDelegate orderChangedEvent;
    public event tableRemovedDelegate tableRemovedEvent;

    public OrderManager()
    {
        orders = new Hashtable();
    }
    public void addOrder(Order o)
    {
        
        orders.Add(o.Id, o);
        switch (o.CookDestination)
        {
            case CookType.Bar:
                if (newOrderBarEvent != null)
                     newOrderBarEvent(o);
                else
                    Console.WriteLine("No one in the Bar is listening!");
                break;
            case CookType.Kitchen:
                if (newOrderKitchenEvent != null)
                     newOrderKitchenEvent(o);
                else
                    Console.WriteLine("No one in the Kitchen is listening!");
                break;
            default:
                Console.WriteLine("OOPS");
                break;
        }
        
       
    }
    public void changeState(String orderID, OrderState newState)
    {
        if(orders.ContainsKey(orderID))
            ((Order)orders[orderID]).State= newState;
        if (orderChangedEvent != null)
            orderChangedEvent((Order)orders[orderID]);
    }

    public ArrayList getAllOrders()
    {
        return new ArrayList(this.orders.Values);
    }

    public Order getOrderFromID(string id)
    {
        return (Order)orders[id];
    }

    public ArrayList getOrdersFromTable(int id)
    {
        ArrayList al = new ArrayList();
        ICollection c = this.orders.Values;
        foreach (Order o in c)
        {
            if (o.TableID == id)
                al.Add(o);
        }

        return al;
    }

    public ArrayList getAllDestination(CookType ct)
    {
        ArrayList al = new ArrayList();
        ICollection c = this.orders.Values;
        foreach (Order o in c)
        {
            if (o.CookDestination == ct)
                al.Add(o);
        }
        return al;
    }

    public void removeTable(int tableId)
    {
        ArrayList ids = new ArrayList();
        ICollection c = this.orders.Values;
        foreach (Order o in c)
        {
            if (o.TableID == tableId)
                ids.Add(o.Id);

        }

        foreach (String id in ids)
        {
            orders.Remove(id);
        }

        if (tableRemovedEvent != null)
            tableRemovedEvent(tableId);
    }
}

