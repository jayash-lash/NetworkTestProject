using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private TMP_Text _playerNameText;
    private Camera _mainCamera;
    private Vector3 _mouseInput = Vector3.zero;
    private NetworkVariable<FixedString64Bytes> _playerNetworkName = new NetworkVariable<FixedString64Bytes>();
    
    private void Initialize()
    {
        _mainCamera = Camera.main;
        _playerNetworkName.OnValueChanged += OnNickNameUpdated;
    }

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        Initialize();
        
        if (IsOwner)
        {
            var playerName = EntryPoint.Instance.UIControl.GetPlayerName();

            if (string.IsNullOrEmpty(playerName))
            {
                playerName = "player" + NetworkObject.OwnerClientId;
            }

            SetPlayerNameServerRpc(new FixedString64Bytes(playerName));
        }
        else if (IsClient)
        {
            _playerNameText.text = _playerNetworkName.Value.ToString();
        }
    }

    private void Update()
    {
        if (!IsOwner || !Application.isFocused) return;

        // Movement
        _mouseInput.x = Input.mousePosition.x;
        _mouseInput.y = Input.mousePosition.y;
        _mouseInput.z = 0f;

        var mouseWorldCoordinates = _mainCamera.ScreenToWorldPoint(_mouseInput);
        mouseWorldCoordinates.z = 0f;
        transform.position = Vector3.MoveTowards(transform.position, mouseWorldCoordinates, Time.deltaTime * _speed);

        // Rotate
        if (mouseWorldCoordinates != transform.position)
        {
            var targetDirection = mouseWorldCoordinates - transform.position;
            targetDirection.z = 0;
            transform.up = targetDirection;
        }
    }
    
    [ServerRpc]
    private void SetPlayerNameServerRpc(FixedString64Bytes newName)
    {
        _playerNetworkName.Value = newName;
    }

    private void OnNickNameUpdated(FixedString64Bytes oldNick, FixedString64Bytes newNick)
    {
        _playerNameText.text = newNick.ToString();
    }
}
