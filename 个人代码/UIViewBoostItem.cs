using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIViewBoostItem : MonoBehaviour
{
    public enum BoostUIType
    {
        GameStartMenu,
        GameUI,
        BackpackUI
    }

    #region BoostUI

    [SerializeField] private GameObject click_Btn;
    [SerializeField] private Image icon_Img;
    [SerializeField] private Text count_Txt;
    [SerializeField] private GameObject plus_Obj;
    [SerializeField] private GameObject lock_Obj;
    [SerializeField] private GameObject selected_Obj;

    #endregion

    public int ItemId { get; private set; }

    private BackpackItem currentBackpackItem; // 本地存储的背包数据
    private BoostProduct currentBoostProduct; // 道具配置数据

    private BoostUIType currentBoostUIType;

    public void ShowItem(int itemId, BoostUIType UIType)
    {
        ItemId = itemId;
        currentBoostUIType = UIType;

        InitBoostData();
        AddEvents();
        RefreshData();
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }

    private void InitBoostData()
    {
        currentBackpackItem = User.Instance.getBackpackItem(ItemId);
        currentBoostProduct = User.Instance.BoostProducts.Find(i => i.id == ItemId);
    }

    private void AddEvents()
    {
        UIEventListener.Get(click_Btn).AddClick(OnClidkBoostBtn);
        //EventDispatcher.AddListener(GCEventType.UpdateBoostItem, this, "RefreshData");
    }

    public void HideItem()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        RemoveEvents();
    }

    private void RemoveEvents()
    {
        UIEventListener.Get(click_Btn).RemoveClick(OnClidkBoostBtn);
        //EventDispatcher.RemoveListener(GCEventType.UpdateBoostItem, this, "RefreshData");
    }

    public void RefreshData()
    {
        UpdateUI();
    }

    private void RefreshData(int id)
    {
        if (id == ItemId)
        {
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        bool isLocked = currentBackpackItem.isLocked;
        bool needPlus = currentBackpackItem.count <= 0;
        lock_Obj.SetActive(isLocked);
        plus_Obj.SetActive(needPlus);
        icon_Img.sprite = currentBoostProduct.icon;
        count_Txt.text = currentBackpackItem.count.ToString();
        count_Txt.gameObject.SetActive(!needPlus && !isLocked);
        selected_Obj.SetActive(false);
    }

    private void OnClidkBoostBtn(GameObject obj)
    {
        SoundManager.Instance.PlayOneShot(AudioType.Audio_UI_Click);
        if (currentBackpackItem.isLocked)
        {
            // TODO:弹窗提示 未解锁
            OnClickBoostItemIsLockedState();
        }
        else if (currentBackpackItem.count <= 0)
        {
            // TODO:弹窗提示购买，或直接跳商店，注意：购买后，返回当前界面，需要刷新这个item
            OnClickBoostItemIsNull();
        }
        else if (currentBackpackItem.count > 0)
        {
            switch (currentBoostUIType)
            {
                case BoostUIType.BackpackUI:
                    OnClickBoostItemInBackpackUI();
                    break;
                case BoostUIType.GameUI:
                    OnClickBoostItemInGameUI();
                    break;
                case BoostUIType.GameStartMenu:
                    OnClickBoostItemInGameStartMenu();
                    break;
            }
        }
    }

    private void OnClickBoostItemIsLockedState()
    {
        GUIManager.Instance.OpenUI<UICtrlScreenTips>("Warning|Unlocked At LV" + currentBoostProduct.unlockLevel);
    }

    private void OnClickBoostItemIsNull()
    {
        GUIManager.Instance.OpenUI<UICtrlScreenTips>("Warning|You Have No Boost!");
    }

    private void OnClickBoostItemInBackpackUI()
    {
        // TODO:如果是在背包中打开，则点击后弹窗为 简介、购买等

    }

    private void OnClickBoostItemInGameUI()
    {
        // TODO:局内点击，
        DebugView.log("选中了道具:" + currentBoostProduct.name + "，祝游戏愉快！");

    }

    private void OnClickBoostItemInGameStartMenu()
    {
        // TODO:显示选中效果，开局后调用使用效果，消耗道具
        bool isSelected = selected_Obj.activeSelf;
        if (isSelected)
        {
            User.Instance.SelectedBoostList.Remove(currentBoostProduct.boostNewType);
        }
        else
        {
            User.Instance.SelectedBoostList.Add(currentBoostProduct.boostNewType);

        }
        selected_Obj.SetActive(!isSelected);
        count_Txt.gameObject.SetActive(isSelected);
    }



}
