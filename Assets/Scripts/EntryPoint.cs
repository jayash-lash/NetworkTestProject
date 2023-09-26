using Unity.Netcode;
using UnityEngine;

public class EntryPoint : MonoBehaviour
{
    public static EntryPoint Instance { get; private set; }
    [SerializeField] private UINicknameControl _uiNicknameControl;
    
    public UINicknameControl UIControl => _uiNicknameControl;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
    }
  
    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
    }
}