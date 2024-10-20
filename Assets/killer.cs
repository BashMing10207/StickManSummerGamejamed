using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class killer : MonoBehaviourPun,IPunObservable
{
    PhotonView pov;

    void Awake()
    {
        pov = GetComponent<PhotonView>();
        
    }
    public void OnParticleCollision(GameObject? col)
    {
        if (col.transform.root.CompareTag("Player"))
        {
        //pov.TransferOwnership(col.transform.root.GetComponent<PhotonView>().Owner);


            if(col.CompareTag("wps"))
            {
                col.GetComponent<gun>().kal(transform.position);
            }
            else
            {
            col.transform.root.GetComponent<plmove>().die();
            }
        }
        
            
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //    if (other.transform.root.CompareTag("Player"))
        //{ PhotonView popo = other.transform.root.GetComponent<PhotonView>();
        //        pov.TransferOwnership(popo.Owner);
        //if (pov.IsMine)//변경사항인!
        //{
        //        plmove player = other.transform.root.GetComponent<plmove>();
        //        player.die();
        //    }
        //}
        if (other.transform.root.CompareTag("Player"))
        {
            other.transform.root.GetComponent<plmove>().die();
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.IsWriting)
        {
            //stream.SendNext(tpos);
            //stream.SendNext(trot);

        }
        else
        {
            //tpos = (Vector3)stream.ReceiveNext();
            //trot = (Quaternion)stream.ReceiveNext();

        }
    }

}
