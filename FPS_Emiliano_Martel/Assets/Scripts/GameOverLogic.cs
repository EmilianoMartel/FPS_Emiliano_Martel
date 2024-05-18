using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameOverLogic : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _winText;
    [SerializeField] private TMPro.TMP_Text _loseText;
    [SerializeField] private BoolDataSO _winData;

    private void Awake()
    {
        NullReferenceController();
        FinalShowLogic(_winData.boolData);
    }

    private void FinalShowLogic(bool isWin)
    {
        if (isWin)
        {
            _winText.gameObject.SetActive(true);
            _loseText.gameObject.SetActive(false);
        }
        else
        {
            _winText.gameObject.SetActive(false);
            _loseText.gameObject.SetActive(true);
        }
    }

    private void NullReferenceController()
    {
        if (!_winText)
        {
            Debug.LogError($"{name}: Win text is null.\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_loseText)
        {
            Debug.LogError($"{name}: Lose text is null.\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_winData)
        {
            Debug.LogError($"{name}: Win data is null.\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
    }
}
