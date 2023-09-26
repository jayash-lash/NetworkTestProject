using TMPro;
using UnityEngine;

public class UINicknameControl : MonoBehaviour
{
    [SerializeField] private GameObject _buttonHolder;
    [SerializeField] private GameObject _editPopUp;
    [SerializeField] private TMP_InputField _playerNameInputField;

    public void SaveNickname()
    {
        _editPopUp.SetActive(false);
        _buttonHolder.SetActive(true);
    }
    
    public string GetPlayerName()
    {
        return _playerNameInputField.text;
    }
    public string GetDefaultPlayerName()
    {
        return "DefaultPlayerName";
    }
}