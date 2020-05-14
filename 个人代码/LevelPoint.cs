using System.Collections;
using System.Collections.Generic;
using SweetSugar.Scripts.Level;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelPoint : MonoBehaviour
{
    private Vector3 _originalScale;
    private bool _isScaled;
    public bool IsLocked;
    private int StarsCount;

    public float OverScale = 1.05f;
    public float ClickScale = 0.95f;

    public int Number;
    public Transform Unlock;
    public Transform Lock;
    public Transform StarsHoster;
    public Transform Star1;
    public Transform Star2;
    public Transform Star3;
    public TextMeshPro text;
    public ParticleSystem particle;

    public void Awake()
    {
        _originalScale = transform.localScale;
    }

    #region Enable click

    public void OnMouseEnter()
    {
        Scale(OverScale);
    }

    public void OnMouseDown()
    {
        Scale(ClickScale);
    }

    public void OnMouseExit()
    {
        ResetScale();
    }

    private void Scale(float scaleValue)
    {
        if (IsLevelLocked())
            return;
        transform.localScale = _originalScale * scaleValue;
        _isScaled = true;
    }

    public void OnDisable()
    {
        ResetScale();
    }

    public void OnMouseUpAsButton()
    {
        ResetScale();
        if (EventSystem.current.IsPointerOverGameObject(-1))
        {
            return;
        }
        if (!IsLevelLocked())
        {
            //SoundBase.Instance.PlayOneShot(SoundBase.Instance.click);
            SoundManager.Instance.PlayOneShot(AudioType.Audio_UI_Click);
            MapManager.Instance.OpenMenuPlay(Number);
        }
        Platform.add(LogEvent.HomePage_Level_Click, true, "Level", Number.ToString());
        Platform.addAppCenterEvent(LogEvent.Homepage_Behavior_Analysis,
            LogEvent.HomePage_Level_Click, Number.ToString());
    }
    private void ResetScale()
    {
        if (_isScaled)
            transform.localScale = _originalScale;
    }

    #endregion

    public void UpdateState()
    {
        StarsCount = User.Instance.getLvlStar(Number);
        bool isLocked = IsLevelLocked();
        UpdateStars(isLocked ? 0 : StarsCount);
        IsLocked = isLocked;
        Lock.gameObject.SetActive(isLocked);
        Unlock.gameObject.SetActive(!isLocked);
        text.text = "" + Number;
        text.gameObject.SetActive(!isLocked);
    }
    public void Play()
    {
        particle.gameObject.SetActive(true);
        particle.Play();
    }
    public void Stop()
    {
        particle.Stop();
        particle.gameObject.SetActive(false);
    }
    public bool IsLevelLocked()
    {
        return Number > 1 && User.Instance.getLvlStar(Number - 1) == 0;
    }
    public void UpdateStars(int starsCount)
    {
        Star1?.gameObject.SetActive(starsCount >= 1);
        Star2?.gameObject.SetActive(starsCount >= 2);
        Star3?.gameObject.SetActive(starsCount >= 3);
    }
}
