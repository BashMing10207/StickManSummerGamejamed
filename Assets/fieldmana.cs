using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class fieldmana : MonoBehaviourPunCallbacks
{
    public GameObject pl1, pl2, boom;
    public Text tx;
    bool started = true;
    PhotonView pov;
    // Start is called before the first frame update
    void Awake()
    {
        pov = GetComponent<PhotonView>();

    }
        [PunRPC]
        void requst()
        {
            PhotonNetwork.LoadLevel(System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(2)));
            SceneManager.LoadScene(2);
        }
    void Update()
    {
        tx.text = PhotonNetwork.PlayerList.Length.ToString();
        if (PhotonNetwork.IsMasterClient)
        {
            if (PhotonNetwork.PlayerList.Length == 2 && started)
            {
                started = false;
                PhotonNetwork.Instantiate(boom.name, transform.position, transform.rotation);
                Invoke("next", 1.8f);
            }

        }
    }
    void next()
    {
        //SceneManager.LoadScene(2);

            photonView.RPC("requst", RpcTarget.All);
        //else
        //if (PhotonNetwork.IsMasterClient)
        //{
        //    photonView.RPC("requst", RpcTarget.All);
        //}
    }

}
