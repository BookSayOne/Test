using UnityEngine;

public class UIViewBase : MonoBehaviour
{
    public int zOrder = 0;
    public ENGUILayer uiLayer = ENGUILayer.Main;
    public UIViewBase()
    {

    }
    private void Awake()
    {
        transform.SetSiblingIndex(zOrder);
    }
}
