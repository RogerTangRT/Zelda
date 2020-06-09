using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IButtonClick
{
    event EventHandler clickDown;
    event EventHandler clickUp;
}

public class ActionButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler, IButtonClick
{
    public event EventHandler clickDown;
    public event EventHandler clickUp;

    [HideInInspector]
    public bool Pressed;

    void OnClickDown(EventArgs e)
    {
        if (clickDown != null) 
            clickDown(this, e);
    }
    void OnClickUp(EventArgs e)
    {
        if (clickUp != null)
            clickUp(this, e);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
        OnClickDown(new EventArgs());
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
        OnClickUp(new EventArgs());
    }

}
