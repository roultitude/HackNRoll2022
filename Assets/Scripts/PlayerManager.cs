using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView pv;
    Transform spawnTransform;


    private void Awake()
    {
        pv = GetComponent<PhotonView>();
        
    }


    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            spawnTransform = GameObject.Find("Player1").transform;
        }
        else
        {
            spawnTransform = GameObject.Find("Player2").transform;
        }
        if (pv.IsMine)
        {
            CreateController();
        }
        
    }


    void CreateController()
    {
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerPrefab"), spawnTransform.position, Quaternion.identity);
    }
}
