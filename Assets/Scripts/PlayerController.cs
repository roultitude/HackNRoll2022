using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float moveSpeed, rotateSpeed;
    Vector3 moveDir;
    Vector3 prevMoveDir;
    Rigidbody rb;
    PhotonView PV;
    public Counter currentCounter;
    public ItemScript currentItem;
    public Transform holdPoint;
    public bool inverted;
    public AudioSource pickupSound, recipeSound, startCookSound,trashSound;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        PV = GetComponent<PhotonView>();
        if (!PV.IsMine)
        {
            //destroy camera
            Destroy(rb);
            return;
        }
        FindObjectOfType<CameraController>().player = this;
        inverted = false;
    }

    private void Update()
    {
        if (!PV.IsMine)
        {
            return;
        } //guardclause for non-local player
        Move();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Interact();
        }
        if (currentCounter)
        {
            if((currentCounter.transform.position - transform.position).magnitude > 1.5)
            {
                currentCounter = null;
            }
        }
    }

    private void Move()
    {
        moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        if (inverted)
        {
            moveDir = -moveDir;
        }
    }

    private void Interact()
    {
        if (currentCounter)
        {
            currentCounter.InteractAction(this);
        }
        
    }

    private void FixedUpdate()
    {
        if (!PV.IsMine) return;
        rb.MovePosition(transform.position + moveDir * moveSpeed * Time.fixedDeltaTime);
        if (moveDir != Vector3.zero)
        {
            rb.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDir), rotateSpeed * Time.fixedDeltaTime);
        }
            
        
     

    }

    public void invertControls()
    {
        StartCoroutine(invertControlsEnumerator());
    }
    public IEnumerator invertControlsEnumerator()
    {
        Debug.Log("kkekW");
        yield return new WaitForSeconds(0.5f);
        inverted = !inverted;
    }
}
