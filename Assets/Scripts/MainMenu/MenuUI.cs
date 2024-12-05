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

    [SerializeField] private GameObject fullRoomError;
    [SerializeField] private GameObject roomNotExistError;
    [SerializeField] private GameObject roomAlreadyExistsError;
    [SerializeField] private Button closeErrorButton;
    [SerializeField] private Button closeErrorButton2;
    [SerializeField] private Button closeErrorButton3;

    private void Awake()
    {
        nicknameSucessButton.onClick.AddListener(NickNameJoined);
        createButton.onClick.AddListener(CreateRoom);
        joinButton.onClick.AddListener(JoinRoom);
        closeErrorButton.onClick.AddListener(CloseErrorMessage);
        closeErrorButton2.onClick.AddListener(CloseErrorMessage);
        closeErrorButton3.onClick.AddListener(CloseErrorMessage);
    }

    private void OnDestroy()
    {
        nicknameSucessButton.onClick.RemoveAllListeners();
        createButton.onClick.RemoveAllListeners();
        joinButton.onClick.RemoveAllListeners();
        closeErrorButton.onClick.RemoveAllListeners();
        closeErrorButton2.onClick.RemoveAllListeners();
        closeErrorButton3.onClick.RemoveAllListeners();
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

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log($"OnCreateRoomFailed - Return Code: {returnCode}, Message: {message}");

        roomAlreadyExistsError.SetActive(true);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log($"OnJoinRoomFailed - Return Code: {returnCode}, Message: {message}");

            roomNotExistError.SetActive(true);
    }

    public void CloseErrorMessage()
    {
        roomAlreadyExistsError.SetActive(false);
        fullRoomError.SetActive(false);
        roomNotExistError.SetActive(false);
    }

    private void Update()
    {
        if (roomAlreadyExistsError.activeSelf || fullRoomError.activeSelf || roomNotExistError.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseErrorMessage();
            }
        }
    }
}
