using UnityEngine;
public interface IUICtrlBase
{   
    void Init(GameObject uiroot);
    void Open(object args);
    void Destroy();
    void Show();
    void Hide();
    UIViewBase View();

}