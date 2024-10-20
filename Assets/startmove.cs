using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startmove : MonoBehaviourPun, IPunObservable
{
    // Start is called before the first frame update
    public bool moving;
    public Transform tar1, tar2,player1,player2;
    Rigidbody2D[] pl1ri, pl2ri;
    Collider2D[] pl1co, pl2co;
    bool awak = true,find;
    //public void exp()
    void Awake()
    {
        photonView.RPC(nameof(nodie), RpcTarget.All);
    //pl1ri = player1.GetComponentsInChildren<Rigidbody2D>();
    //pl2ri = player2.GetComponentsInChildren<Rigidbody2D>();
    //pl1co = player1.GetComponentsInChildren<Collider2D>();
    //pl2co = player2.GetComponentsInChildren<Collider2D>();

            //if (player1.GetComponent<PhotonView>().IsMine)
            //{
            //    for (int i = 0; i < pl1ri.Length; i++)
            //    {
            //        if (!pl2ri[i].transform.CompareTag("wps"))
            //        {
            //            pl2ri[i].isKinematic = true;
            //        }
            //        if (!pl1ri[i].transform.CompareTag("wps"))
            //        {
            //            pl1ri[i].isKinematic = true;
            //        }
            //    }
            //}
            //else
            //{

            //    for (int i = 0; i < pl1co.Length; i++)
            //    {
            //        if (!pl2ri[i].transform.CompareTag("wps"))
            //        {
            //            pl2co[i].isTrigger = true;
            //        }
            //        if (!pl1ri[i].transform.CompareTag("wps"))
            //        {
            //            pl1co[i].isTrigger = true;
            //        }
            //    }
            //}

    }

    [PunRPC]
    void nodie()
    {
        if (player1 == null)
        {
            try
            {

                player1 = GameObject.Find("player-1").transform;
                pl1co = player1.GetComponentsInChildren<Collider2D>();
                pl1ri = player1.GetComponentsInChildren<Rigidbody2D>();

                for (int i = 0; i < pl1ri.Length; i++)
                {
                    if (!pl2ri[i].transform.CompareTag("wps"))
                    {
                        pl2ri[i].isKinematic = true;
                    }
                }
                for (int i = 0; i < pl1co.Length; i++)
                {
                    if (!pl1co[i].transform.CompareTag("wps"))
                    {
                        pl1co[i].isTrigger = true;
                    }
                }
            }
            catch
            {
            }

        }
        if (player2 == null)
        {
            try
            {

                player2 = GameObject.Find("player_2").transform;

                pl2co = player2.GetComponentsInChildren<Collider2D>();
                pl2ri = player2.GetComponentsInChildren<Rigidbody2D>();

                for (int i = 0; i < pl2co.Length; i++)
                {
                    if (!pl2co[i].transform.CompareTag("wps"))
                    {
                        pl2co[i].isTrigger = true;
                    }
                }
                for (int i = 0; i < pl2ri.Length; i++)
                {
                    if (!pl2ri[i].transform.CompareTag("wps"))
                    {
                        pl2ri[i].isKinematic = true;
                    }

                }
            }
            catch
            {
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (counterplayer.Instance.currentPlayerCount == 2)
        {

        if(PhotonNetwork.InRoom && awak)
        {

        if (find)
        {
            

                awak = false;
                moving = true;
                


                //if (player1.GetComponent<PhotonView>().IsMine)
                //{
                //    player1.position = tar1.position;
                //    player1.position = Vector3.Lerp(player1.position, tar1.transform.position, 0.5f);
                //}
                //else
                //{
                //    player2.position = tar2.position;
                //    player2.position = Vector3.Lerp(player2.position, tar2.transform.position, 0.5f);
                //}
            


        }
        else
        {
                    photonView.RPC(nameof(nodie), RpcTarget.All);

                if(player1 && player2)
                {
                    find = true;
                }
        }
        }
            if (moving)
        {

                //player1.position = Vector3.MoveTowards(player1.position, tar1.transform.position, 0.05f);
                player1.position = Vector3.Lerp(player1.position, tar1.transform.position, 0.5f);

                //player2.position = Vector3.MoveTowards(player2.position, tar2.transform.position, 0.05f);
                player2.position = Vector3.Lerp(player2.position, tar2.transform.position, 0.5f);

            if(Vector3.Distance(player1.position, tar1.position) < 0.1f && Vector3.Distance(player2.position, tar2.position) < 0.1f)
            {
                moving = false;


                    //if (Vector3.Distance(player1.position, tar1.position) < 0.1f)
                        for (int i = 0; i < pl1ri.Length; i++)
                        {
                            if (!pl1ri[i].transform.CompareTag("wps"))
                            {
                                pl1ri[i].isKinematic = false;
                            }
                        }
                    for (int i = 0; i < pl1co.Length; i++)
                    {
                        if (!pl1co[i].transform.CompareTag("wps"))
                        {
                            pl1co[i].isTrigger = false;
                        }
                    }
                

                    //if (Vector3.Distance(player2.position, tar2.position) < 0.1f)
                    for (int i = 0; i < pl2ri.Length; i++)
                    {
                            if (!pl2ri[i].transform.CompareTag("wps"))
                            {
                                pl2ri[i].isKinematic = false;
                            }
                    }
                    for (int i = 0; i < pl2co.Length; i++)
                    {
                        if (!pl2co[i].transform.CompareTag("wps"))
                        {
                            pl2co[i].isTrigger = false;
                        }
                    }
                
            }
        }
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.IsWriting)
        {
            //stream.SendNext(player1);
            //stream.SendNext(player2);

        }
        else
        {
           //player1 = (Transform)stream.ReceiveNext();
           //player2 = (Transform)stream.ReceiveNext();

        }
    }
}
