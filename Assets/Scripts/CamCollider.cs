using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamCollider : MonoBehaviour
{
    [SerializeField] CameraController camController;
    [SerializeField] int camIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() == camController.player)
        {
            camController.changeCam(camIndex);
        }
    }
}
