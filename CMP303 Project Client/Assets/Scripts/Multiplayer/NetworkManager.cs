using RiptideNetworking;
using RiptideNetworking.Utils;
using System;
using UnityEngine;

public enum ClientToServerID : ushort
{
    name = 1,
}

public class NetworkManager : MonoBehaviour
{
    //Create Singleton that destroys if instance already exists
    private static NetworkManager _singleton;
    public static NetworkManager Singleton
    {
        get => _singleton;
        private set
        {
            if (_singleton == null)
                _singleton = value;
            else if (_singleton != value)
            {
                Debug.Log($"{nameof(NetworkManager)} instance already exists!");
                Destroy(value);
            }
        }
    }

    //Create Client Properties
    public Client Client { get; private set; }

    [SerializeField] private string ip;
    [SerializeField] private ushort port;

    //Clientfunctions
    private void Awake() //Awake
    {
        Singleton = this;
    }

    private void Start() //Start
    {
        //Allows messages to be printed to Unity logger
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);

        Client = new Client();
        Client.Connected += DidConnect;
        Client.ConnectionFailed += FailedToConnect;
        Client.Disconnected += DidDisconnect;
    }

    private void FixedUpdate() //FixedUpdate
    {
        Client.Tick();
    }

    private void OnApplicationQuit() //On Quit
    {
       Client.Disconnect();
    }

    public void Connect()
    {
        //Client Connect method 
        Client.Connect($"{ip}:{port}");
    }

    private void DidConnect(object sender, EventArgs e)
    {
        UIManager.Singleton.SendName();
    }

    private void FailedToConnect(object sender, EventArgs e)
    {
        UIManager.Singleton.MainReturn();
    }

    private void DidDisconnect(object sender, EventArgs e)
    {
        UIManager.Singleton.MainReturn();
    }
}
