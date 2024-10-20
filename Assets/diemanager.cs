using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class diemanager : MonoBehaviourPun , IPunObservable
{ plmove p1, p2;
    public GameObject hole;
    public Image colo;
    bool die;
    public Transform[] exits; // 0:red die 1: blue die
    public Animator animator;
    PhotonView pov;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        pov = GetComponent<PhotonView>();
       //p1 = mana.manager.playingp1.GetComponent<plmove>();
       //p2 = mana.manager.playingp2.GetComponent<plmove>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public void dead(bool red,int po)
    {
        //exits[0].gameObject.SetActive(red);
        //exits[1].gameObject.SetActive(!red);
        pov.TransferOwnership(po);
        animator.SetTrigger("die");
        animator.SetBool("reddie", red);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.IsWriting)
        {

            //stream.SendNext(trot);

        }
        else
        {

            //trot = (Quaternion)stream.ReceiveNext();

        }
    }
}
