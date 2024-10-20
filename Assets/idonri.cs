using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class idonri : MonoBehaviourPun, IPunObservable
{ PhotonView pov;
    Rigidbody2D rigidbody2;
    // Start is called before the first frame update
    void Start()
    {
        pov = GetComponent<PhotonView>();
        rigidbody2 = GetComponent<Rigidbody2D>();

        if(!pov.IsMine)
        {
            rigidbody2.isKinematic = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
    }


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

    }

}
