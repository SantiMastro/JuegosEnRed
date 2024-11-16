using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class MenuUI : MonoBehaviourPunCallbacks
{
    [SerializeField] private Button nicknameSucessButton;
    [SerializeField] private Button createButton;
    [SerializeField] private Button joinButton;
    [SerializeField] private TMPro.TMP_InputField createInput;
    [SerializeField] private TMPro.TMP_InputField joinInput;
    [SerializeField] private TMPro.TMP_InputField nicknameInput;

    [SerializeField] private GameObject _screen1;
    [SerializeField] private GameObject _screen2;

    private void Awake()
    {
        nicknameSucessButton.onClick.AddListener(NickNameJoined);
        createButton.onClick.AddListener(CreateRoom);
        joinButton.onClick.AddListener(JoinRoom);
    }

    private void OnDestroy()
    {
        nicknameSucessButton.onClick.RemoveAllListeners();
        createButton.onClick.RemoveAllListeners();
        joinButton.onClick.RemoveAllListeners();
    }

    private void CreateRoom()
    {
        RoomOptions roomConfiguration = new RoomOptions();
        roomConfiguration.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(createInput.text, roomConfiguration);
        PhotonNetwork.NickName = nicknameInput.text;
    }

    private void JoinRoom()
    {
        PhotonNetwork.JoinRoom(joinInput.text);
        PhotonNetwork.NickName = nicknameInput.text;
    }

    private void NickNameJoined()
    {
        _screen1.SetActive(false);
        _screen2.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Level");
    }
}
