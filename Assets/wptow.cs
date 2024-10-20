using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;


public class wptow : MonoBehaviourPun, IPunObservable
{
    public TargetJoint2D tj;
    public Transform tr;
    Vector3 tpos;
    Quaternion trot;
    PhotonView pov;
    float last;
    public float time;
    public Animator ani;
    public bool wpab,haswp;
    bool trac;
    BoxCollider2D col;
    gun? guns;
    bool coli;
    
    public GameObject gunfire;
    // Start is called before the first frame update
    void Start()
    {
        pov = transform.root.GetComponent<PhotonView>();
        if(wpab)
        col = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(pov.IsMine)
        {
            if(trac||haswp)
            {
                tj.enabled = true;
                tj.target = (Vector2)tr.position;
            }
            else
            {
                    tj.enabled = false;
            }
            if(Input.GetButton("Fire2"))
            {
                    trac = true;
                if(wpab)
                    col.enabled = true;
            }
            else
            {
               trac=false;
                if (wpab)
                    col.enabled = coli;
            }

            if (wpab && haswp && Input.GetButtonDown("Fire2") && guns)
            {
                    guns.transform.parent = null;
                    guns.thisri.isKinematic = false;
                    guns.throwgun(tr.parent.forward);
                    guns.onhand = false;
                    haswp = false;
                    if(0 < transform.GetChild(0).childCount)
                    {
                    transform.GetChild(0).GetChild(0).parent = null;
                    }
                    coli = false;
                    guns.transform.GetComponent<Rigidbody2D>().AddForce(tr.parent.forward * 300f, ForceMode2D.Impulse);
                    guns = null;
                Invoke(nameof(colenable), 0.3f);
            }

            if (wpab && haswp)
            {
                guns.fire = Input.GetButton("Fire1");
            }
        }
        //tpos = transform.position;
        //trot = transform.rotation; 
    }

    void colenable()
    {
        coli = true;
    }

    public void OnAtorpt()
    {
        tj.enabled = true;
        tj.target = (Vector2)tr.position;
    }

    public void Ondisatpt()
    {
        tj.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D wps)
    {
        if (pov.IsMine)
        {
            if (wps.CompareTag("wps"))
            {
                if (wps.transform.root == wps.transform && !haswp && null == wps.transform.parent)
                {
                    guns = wps.GetComponent<gun>();
                    if(guns.onhand == true)
                    {
                        guns = null;
                        return;
                    }
                    //wps.GetComponent<HingeJoint2D>().connectedBody=GetComponent<Rigidbody2D>();
                    wps.GetComponent<Rigidbody2D>().isKinematic = true;
                    wps.transform.position = transform.GetChild(0).position;
                    wps.transform.rotation = transform.GetChild(0).rotation;
                    //wps.GetComponent<Collider2D>().isTrigger = true;
                    wps.transform.parent = transform.GetChild(0);
                    haswp = true;
                    guns.geted();
                    guns.body = GetComponent<Rigidbody2D>();
                }
            }
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        //if (stream.IsWriting)
        //{
        //    stream.SendNext(transform.position);
        //    stream.SendNext(transform.rotation);
        //}
        //else
        //{
        //    tpos = (Vector3)stream.ReceiveNext();
        //    trot = (Quaternion)stream.ReceiveNext();
        //}
    }
}
