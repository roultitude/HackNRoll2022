using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float gameTimer = 300;
    public static GameManager instance;
    public TextMeshProUGUI timerText, achievedText;
    PhotonView pv;
    [SerializeField]
    GameObject winImage, loseImage, tutorialImage,recipeImage;
    bool isMaster;
    int money = 0;
    bool win = false;
    bool lose = false;
    private void Awake()
    {
        if (instance && this != instance)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            pv = GetComponent<PhotonView>();
            isMaster = PhotonNetwork.IsMasterClient;
        }
        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            toggleRecipe();
        }
        if (!isMaster) return;
        if (gameTimer > 0)
        {
            gameTimer -= Time.deltaTime;
        }
        
        
    }

    private void FixedUpdate()
    {
        if (!isMaster) return;
        float secondsRemainder = Mathf.Floor((gameTimer % 60) * 100) / 100.0f;
        int minutes = ((int)(gameTimer/ 60)) % 60;
        pv.RPC("updateTimerText", RpcTarget.All, System.String.Format("{0:00}:{1:00.00}", minutes, secondsRemainder));
        if (money > 1000 && !win)
        {
            pv.RPC("winRPC", RpcTarget.All);
            win = true;
        }
        if (gameTimer < 0 && !lose)
        {
            pv.RPC("loseRPC", RpcTarget.All);
            lose = true;
        }
    }

    public void changeMoney(int change)
    {
        if (!isMaster) return;
        money += change;
        pv.RPC("updateMoneyText", RpcTarget.All, money);
    }
    [PunRPC]
    public void updateTimerText(string time)
    {
        timerText.text = time;
    }
    [PunRPC]
    public void updateMoneyText(int money)
    {
        achievedText.text = "ACHIEVED: $" + money.ToString();
    }
    [PunRPC]
    public void winRPC()
    {
        winImage.SetActive(true);
    }
    [PunRPC]
    public void loseRPC()
    {
        loseImage.SetActive(true);
    }
    public void closeTutorialImage()
    {
        tutorialImage.SetActive(false);
    }

    public void toggleRecipe()
    {
        recipeImage.SetActive(!recipeImage.activeInHierarchy);
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LoadLevel(0);
    }
}
