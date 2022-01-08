using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController player;
    public Cinemachine.CinemachineVirtualCamera[] cameras;
    public int currentCam = 0;
    public void changeCam(int id)
    {
        if (id == currentCam) return;
        cameras[currentCam].Priority = 0;
        cameras[id].Priority = 1;
        if (currentCam == 0 && id == 1)
        {
            player.invertControls();
        }
        else if (currentCam == 1 && id == 0)
        {
            player.invertControls();
        }
        currentCam = id;
    }
}
