using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class plmove : MonoBehaviourPun, IPunObservable
{   
    public Rigidbody2D ri;
    public float speed,jp,fall,bounce;
    float yvel;
    public Animator an;
    scang[] sc;
    public bool isg = false,died,isred;
    private PhotonView pov;
    Vector3 tpos;
    Quaternion trot;
    public Transform target;
    public GameObject[] diesounds;
    bool ang;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        pov = GetComponent<PhotonView>();
        if(pov.IsMine)
        {
            sc = GetComponentsInChildren<scang>();
        }
        else
        {
            Rigidbody2D[] rbs = GetComponentsInChildren<Rigidbody2D>();
            for (int i = 1; i < rbs.Length; i++)
            {
                rbs[i].isKinematic = true;
            }
            rbs[0].constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pov.IsMine && !died)
        {
            bool handed = false, infoot = false;
            isg = false;

            for(int i = 1; i < sc.Length; i++)
            {
                isg = isg || sc[i-1].gr || sc[i].gr;
                 
            }
            for(int i = 0; i < sc.Length; i++)
            {
                if (!sc[i].CompareTag("foot"))
                    handed = handed || sc[i].gr;
                if (sc[i].CompareTag("infoot"))
                    infoot = infoot || sc[i].gr;
            }
            yvel = ri.velocity.y;
            ri.velocity = transform.right * speed * Input.GetAxis("Horizontal") + Vector3.up * yvel;
            //ri.velocity = transform.right * speed * Input.GetAxis("Horizontal") + Vector3.up * (yvel+ (Input.GetButton("Horizontal") == true ? 1:0));
            //ri.AddForce(transform.right * speed * Input.GetAxisRaw("Horizontal"));
            ri.AddForce(Vector3.up * ((!handed && isg) ? fall : -2 + ((infoot == true) ? 1 : 0)),ForceMode2D.Impulse); // Áß·Â(-2) or ´Ù¸® Èû(¹ÞÈ÷´Â Èû)
          
            if (isg)
            {
                if(Input.GetButtonDown("Jump"))
                {
                    int bouncedir = 0;
                for (int i = 0; i < sc.Length; i++)
                {  
                    if(!sc[i].CompareTag("foot"))
                    {
                        if (sc[i].points != null)
                        {
                            bouncedir += transform.GetChild(0).position.x > sc[i].points.Value.point.x ? 1 : -1;
                        }

                    }
                }
                ri.AddForce(Vector3.up * jp + Vector3.right * bounce * bouncedir,ForceMode2D.Impulse);
                }
            
            }
            an.SetBool("walk", Input.GetButton("Horizontal"));
            ri.SetRotation(Input.GetAxis("Horizontal") * 8);//Quaternion.EulerAngles(0,0,Input.GetAxis("Horizontal") * 3);

            //if(Input.GetButton("Fire2"))
            //{
                //transform.GetChild(0).localEulerAngles = new Vector3(0, (transform.position.x < Camera.main.ScreenToWorldPoint(Input.mousePosition).x ? 0 : -180), 0);
            target.LookAt(Camera.main.ScreenToWorldPoint(Input.mousePosition));
            //}

            //tpos = transform.position;
            //trot = transform.rotation;
        }
        else
        {
            if(died)
            {

            }
            else
            {
            //ri.isKinematic = true;
            //transform.position = tpos;
            //transform.rotation = trot;
            }
        }
        an.SetBool("die", died);
    }
    public void die()
    {
        //transform.position = new Vector3(0, -40, 0);
        if(ang)
        {
            PhotonNetwork.Instantiate(diesounds[Random.Range(0, 3)].name, transform.position, Quaternion.identity);
            ang = false;
            Invoke("diesou", 1.5f);
        }


        //mana.manager.dienext(isred,pov.Owner.ActorNumber);
        photonView.RPC(nameof(dieonoff), RpcTarget.All);
        died = true;
        Invoke("ress", 7f);
        //PhotonNetwork.Destroy(gameObject);
    }

    [PunRPC]
    void dieonoff()
    {
        diemanager dieman = GameObject.FindWithTag("manager").GetComponent<diemanager>();
        dieman.transform.GetChild(0).gameObject.SetActive(true);

        dieman.animator.SetTrigger("die");
        dieman.animator.SetBool("reddie", isred);
    }

        void diesou()
    {
        ang = true;
    }
    void ress() 
    {
        died = false;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if(stream.IsWriting)
        {
            //stream.SendNext(tpos);
            //stream.SendNext(trot);
            stream.SendNext(died);
        }
        else
        {
            //tpos = (Vector3)stream.ReceiveNext();
            //trot = (Quaternion)stream.ReceiveNext();
            died = (bool)stream.ReceiveNext();
        }
    }


}
