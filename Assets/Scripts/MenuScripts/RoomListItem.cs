using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomListItem : MonoBehaviour
{
    [SerializeField] TMP_Text text;

    public RoomInfo info;

    public void Setup(RoomInfo _info)
    {
        info = _info; //store room information into instance, for use on click
        text.text = info.Name;
    }

    public void OnClick()
    {
        PhotonLauncher.Instance.JoinRoom(info);
    }
}
