using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonContoller : MonoBehaviour
{
    public void OnCreateRoomButton()
    {
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 2 });
    }

    public void OnJoinInRoomButton()
    {
        PhotonNetwork.JoinRandomRoom();
    }
}
