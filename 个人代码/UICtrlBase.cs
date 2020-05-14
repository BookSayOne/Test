using UnityEngine;
using System;
public class UICtrlBase<T>:IUICtrlBase
                        where T:UIViewBase
{
    public T uiview;
    public UICtrlBase()
    {
        
    }
    public void Init(GameObject uiroot)
    {
        uiview = uiroot.GetComponent<T>();
    }
    public virtual void Open(object args){}

    public virtual void Destroy()
    {
        GameObject.DestroyImmediate(uiview.gameObject);
    }
    public virtual void Show()
    {
        uiview.gameObject.SetActive(true);
    }
    public virtual void Hide()
    {
        uiview.gameObject.SetActive(false);
    }
    public UIViewBase View()
    {
        return uiview;
    }
    public void AddUIEvent(GameObject obj, UIEventListener.VoidDelegate del, string uievent = "onClick")
    {
        switch(uievent)
        {
            case "onClick":
                UIEventListener.Get(obj).AddClick(del);
                break;
            case "onUp":
                UIEventListener.Get(obj).onUp += del;
                break;
            
            case "onEnter":
                UIEventListener.Get(obj).onEnter += del;
                break;
            
            default:
                break;

        }
        
    }
    public void AddUIEvent(GameObject obj, UIEventListener.VoidDelgateWithEventdata del, string uievent = "onClick")
    {
        switch (uievent)
        {
            case "onDraging":
                UIEventListener.Get(obj).onDraging += del;
                break;
            case "onBegin":
                UIEventListener.Get(obj).onBegin += del;
                break;
            case "onEnd":
                UIEventListener.Get(obj).onEnd += del;
                break;
            case "onDown":
                UIEventListener.Get(obj).onDown += del;
                break;
            default:
                break;

        }

    }
    public void RemoveUIEvent(GameObject obj, UIEventListener.VoidDelegate del, string uievent = "onClick")
    {
#pragma warning disable RECS0020 // Delegate subtraction has unpredictable result
        switch (uievent)
        {
            case "onClick":

                UIEventListener.Get(obj).RemoveClick(del);
                break;
            case "onUp":
                UIEventListener.Get(obj).onUp -= del;
                break;
            case "onEnter":
                UIEventListener.Get(obj).onEnter -= del;
                break;

            default:
                break;

        }
#pragma warning restore RECS0020 // Delegate subtraction has unpredictable result
    }


    public void RemoveUIEvent(GameObject obj, UIEventListener.VoidDelgateWithEventdata del, string uievent = "onClick")
    {
#pragma warning disable RECS0020 // Delegate subtraction has unpredictable result
        switch (uievent)
        {
            case "onBegin":
                UIEventListener.Get(obj).onBegin -= del;
                break;
            case "onDraging":
                UIEventListener.Get(obj).onDraging -= del;
                break;
            case "onEnd":
                UIEventListener.Get(obj).onEnd -= del;
                break;
            case "onDown":
                UIEventListener.Get(obj).onDown -= del;
                break;
            default:
                break;

        }
#pragma warning restore RECS0020 // Delegate subtraction has unpredictable result
    }
    public void AddEvent(string eventname, string cbname)
    {
        EventDispatcher.AddListener(eventname, this, cbname);
    }
    public void RemoveEvent(string eventname, string cbname)
    {
        EventDispatcher.RemoveListener(eventname, this, cbname);
    }
}