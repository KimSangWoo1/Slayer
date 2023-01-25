using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LockUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField]
    private Image _backgroundImage;
    [SerializeField]
    private Image _mainImage;
    [SerializeField]
    private TextMeshProUGUI _lockText;
    [SerializeField]
    protected Button _btn;

    private void OnDestroy()
    {
        _btn.onClick.RemoveAllListeners();
    }

    public void Set(bool isLock, string lockText, string lockNotice)
    {
        if (isLock)
        {
            _lockText.text = lockText;
            _btn.onClick.AddListener(()=>OnLockClick(lockNotice));
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnLockClick(string lockNotice)
    {
        UIManager.Instance.OpenPopUp(ePopUpType.Notice, lockNotice);
    }
}
