using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class gun : MonoBehaviourPun, IPunObservable
{
    public GameObject gunfire;
    public bool melee,throwd,fire,onhand,isrigi;
    public float speed,power;
    float last=99f;
    public Rigidbody2D body,thisri;
    PhotonView pov;
    Vector3 tpos,dire;
    Quaternion trot;
    // Start is called before the first frame update
    void Awake()
    {
        pov = GetComponent<PhotonView>();

       thisri = GetComponent<Rigidbody2D>();

    }
    public void throwgun(Vector3 dir)
    {
        Invoke("throwafter", 0.2f);
        onhand = false;
        thisri.isKinematic = false;
        //thisri.AddForce(dir * 300, ForceMode2D.Impulse);
        dire = dir;
    }
    void Update()
    {
        if(pov.IsMine)
        {
            
            if (null != transform.parent && onhand)
            {
                transform.position = transform.parent.position;
                
            }
            else
            {
                thisri.isKinematic = false;
            }
        if (speed < last)
        {
            if (fire)
            {
                if (transform.root != transform)
                {
                    if(melee)
                    {
                        body.AddForce(transform.right * power, ForceMode2D.Impulse);
                    }
                    else
                    {
                    PhotonNetwork.Instantiate(gunfire.name,
                    transform.GetChild(0).transform.position, transform.GetChild(0).rotation);
                    body.AddForce(transform.right * power, ForceMode2D.Impulse);
                    }
                    last = 0;
                }
                
            }
        }
        else
        {
            last += Time.deltaTime;

        }

        //tpos = transform.position;
        //trot = transform.rotation;

        }
        else
        {
            thisri.isKinematic = true;
            //transform.position = tpos;
            //transform.rotation = trot;
        }
    }



    void throwafter()
    {
        gameObject.layer = 0;
        throwd = true;
    }

    private void OnCollisionEnter2D(Collision2D co)
    {
        if(throwd || (onhand && melee))
        {
            if(co.transform.root.CompareTag("Player"))
            {
                co.transform.root.GetComponent<plmove>().die();
            }
            if(throwd)
                throwd = false;
        }
    }

    public void kal(Vector3 pos)
    {
        if(melee)
        {
           GameObject a = PhotonNetwork.Instantiate(gunfire.name,
                    transform.position + transform.right, transform.rotation);
                    a.transform.LookAt(pos);
        }
    }

    public void geted()
    {   thisri.isKinematic = true;
        gameObject.layer = transform.parent.gameObject.layer;
        onhand = true;
        pov.TransferOwnership(transform.root.GetComponent<PhotonView>().Owner);
    }
    
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //stream.SendNext(tpos);
            //stream.SendNext(trot);
            stream.SendNext(throwd);
            stream.SendNext(onhand);
        }
        else
        {
            //tpos = (Vector3)stream.ReceiveNext();
            //trot = (Quaternion)stream.ReceiveNext();
            throwd = (bool)stream.ReceiveNext();
            onhand = (bool)stream.ReceiveNext();
        }

    }

}
