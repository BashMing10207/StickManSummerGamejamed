
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class mana : MonoBehaviourPunCallbacks , IPunObservable
{ public InputField mkroomcode,inroomcode;
    static mana ma;
    public GameObject pl1,pl2,playing1,playing2;
 Transform playingp1, playingp2;
    public Transform p1, p2;
    List<RoomInfo> rooms;
    public static mana manager;
    public bool rightwin,startedgame;
    PhotonView pov;
    public bool reddie, bluedie;
    // Start is called before the first frame update
    void Awake()
    {
        pov = GetComponent<PhotonView>();
        manager = this;
        PhotonNetwork.ConnectUsingSettings();
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad (p1.gameObject);
        DontDestroyOnLoad(p2.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void dienext(bool red,int po)
    {


        if (pov.IsMine)
        {
            //FindOtherOwnerPlayer(po);
            //if (PhotonNetwork.IsMasterClient)
              //  photonView.RPC("dieonoff", RpcTarget.All);
            //else
              //  photonView.RPC("dieonoff", RpcTarget.MasterClient);
        }

        reddie = red;
        
        
        //dieman.dead(red,po);
    }

    //[PunRPC]
    //void dieonoff()
    //{
    //    diemanager dieman = GameObject.FindWithTag("manager").GetComponent<diemanager>();
    //    dieman.transform.GetChild(0).gameObject.SetActive(true);

    //        dieman.animator.SetTrigger("die");
    //        dieman.animator.SetBool("reddie", reddie);
    //    if (reddie)
    //    {
    //        //dieman.exits[0].gameObject.SetActive(true);
    //        //dieman.exits[1].gameObject.SetActive(false);
    //    }
    //    else
    //    {
    //        //dieman.exits[1].gameObject.SetActive(true);
    //        //dieman.exits[0].gameObject.SetActive(false);
    //    }
    //}

    private void FindOtherOwnerPlayer(int aaaaa)
    {
        foreach (var player in PhotonNetwork.PlayerList)
        {
            if (player != PhotonNetwork.LocalPlayer && player.ActorNumber != aaaaa)
            {
                pov.TransferOwnership(player.ActorNumber);
                //Debug.Log("Found other owner: " + player.NickName);
            }
        }
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        PhotonNetwork.JoinLobby();
    }
    public void _createroom()
    {
     if(PhotonNetwork.IsConnected) 
        {
            PhotonNetwork.CreateRoom(mkroomcode.text, new RoomOptions { MaxPlayers = 2 });
            SceneManager.LoadScene(1);
        }
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        base.OnCreateRoomFailed(returnCode, message);
        SceneManager.LoadScene(0);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        base.OnJoinRandomFailed(returnCode, message); 
        SceneManager.LoadScene(0);
    }
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        base.OnJoinRoomFailed(returnCode, message);
        SceneManager.LoadScene(0);
    }

    public void _joinroom()
    {
        if(PhotonNetwork.IsConnected && PhotonNetwork.InLobby)
            {
                OnRoomListUpdate(rooms);
                for(int i = 0; i < rooms.Count; i++)
                {
                    if(rooms[i].Name == inroomcode.text)
                    {
                        SceneManager.LoadScene(1);
                        PhotonNetwork.JoinRoom(inroomcode.text);
                        return;
                    }
                }
            }
    }
    public void _joinroomrand()
    {
        if (PhotonNetwork.IsConnected && PhotonNetwork.InLobby)
        {   
            OnRoomListUpdate(rooms);
            if(rooms.Count > 0)
            {

            SceneManager.LoadScene(1);
            PhotonNetwork.JoinRandomRoom();
            }
        }
    }
    public void updaterooms()
    {
    
    }

    public override void OnJoinedLobby()
    {base.OnJoinedLobby();
        Debug.Log("로비 연결");
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {   base.OnRoomListUpdate(roomList);
        Debug.Log($"룸 리스트 업데이트 ::::::: 현재 방 갯수 : {roomList.Count}");
        rooms = roomList;
        
    }
    public override void OnJoinedRoom()
    {   
        base.OnJoinedRoom();
        //SceneManager.LoadScene(1);
        if(PhotonNetwork.PlayerList.Length == 1)
        {
            playing1 = PhotonNetwork.Instantiate(pl1.name,p1.position, Quaternion.identity);
            //playingp1 = playing1.transform;
        }
        else
        {
            playing2 = PhotonNetwork.Instantiate(pl2.name, p2.position, Quaternion.identity);
            //playingp2 = playing2.transform;
            //Invoke(nameof(gmstart), 3f);
        }
        //GameObject.FindWithTag("manager").GetComponent<startmove>().exp();

    }


    private void gmstart()
    {
        startedgame = true;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {

        if (stream.IsWriting)
        {
            //stream.SendNext(playingp1);
            //if(playingp2 != null)
            //stream.SendNext(playingp2);
            //stream.SendNext(startedgame);
            
        }
        else
        {
            //playingp1 = (Transform)stream.ReceiveNext();
            //playingp2 = (Transform)stream.ReceiveNext();
            //startedgame = (bool)stream.ReceiveNext();
        }
    }
}
