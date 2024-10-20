using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class sceneload : MonoBehaviourPun,IPunObservable
{
    public int a=1;
    public bool b=false,isright;
    PhotonView pov;
    Animator animator;
    public GameObject[] maps;
    void Awake()
    {
        pov = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();
    }

    [PunRPC]
    void requst()
    {

            PhotonNetwork.LoadLevel(System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(a)));
            SceneManager.LoadScene(a);

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.root.CompareTag("Player"))
        {
            //if(PhotonNetwork.IsMasterClient)
            //{
                photonView.RPC("requst", RpcTarget.All);
            //}
            //else
            //{
            //    photonView.RPC("requst", RpcTarget.MasterClient);
            //}

            //PhotonView popo = col.transform.root.GetComponent<PhotonView>();
            //pov.TransferOwnership(popo.Owner.ActorNumber);
            b = true;
           // SceneManager.GetSceneAt(SceneManager.GetActiveScene().buildIndex);
            //GameObject[] diegms;
            //diegms = SceneManager.GetActiveScene().GetRootGameObjects();
            //for(int i = 0; i < diegms.Length; i++)
            //{
            //    if (null == diegms[i].GetComponent<dondes>() && i != 0 && null == diegms[i].GetComponent<sceneload>())
            //    {
            //        diegms[i].transform.parent = diegms[0].transform;
            //    }
            //    PhotonNetwork.Destroy(diegms[0]);
            //    if (diegms[0]  != null)
            //    {
            //        Destroy(diegms[0]);
            //    }
                

            //}
            //if(pov.IsMine)
            //{

            //   GameObject aaaa = PhotonNetwork.Instantiate(maps[a-2].name, maps[a-2].transform.position, Quaternion.identity);
            //   Transform[] asdf = aaaa.GetComponentsInChildren<Transform>();
            //    for(int j = 1; j<asdf.Length; j++)
            //    {
            //        asdf[j].transform.parent = null;
            //    }
            //}
            //PhotonNetwork.Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        //animator.SetBool("asd", b);
    }
    public void loa()
    {
        //PhotonNetwork.LoadLevel(SceneManager.GetSceneAt(a).name);
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.IsWriting)
        {
            stream.SendNext(b);
            //stream.SendNext(trot);

        }
        else
        {
            b = (bool)stream.ReceiveNext();
            //trot = (Quaternion)stream.ReceiveNext();

        }
    }
}
