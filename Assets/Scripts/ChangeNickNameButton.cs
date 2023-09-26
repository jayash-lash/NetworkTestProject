using Unity.Netcode;
using UnityEngine;

public class ChangeNickNameButton : NetworkBehaviour
{
    [SerializeField] private GameObject _buttonHolder;

    [SerializeField] private GameObject _editNick;

    public void EnableEditPopUp()
    {
        _buttonHolder.SetActive(false);
        
        _editNick.SetActive(true);
    }
}