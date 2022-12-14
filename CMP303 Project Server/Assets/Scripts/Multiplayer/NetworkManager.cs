using RiptideNetworking;
using RiptideNetworking.Utils;
using System.Reflection;
using UnityEngine;

public enum ServerToClientID : ushort
{
    playerSpawned = 1,
}

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

    //Server property
    public Server Server { get; private set; }

    //Serialize ports and clients
    [SerializeField] private ushort port;
    [SerializeField] private ushort maxClientNum;

    //Server functions
    private void Awake() //Awake
    {
        Singleton = this;
    }

    private void Start() //Start
    {
        //Allows messages to be printed to Unity logger
        RiptideLogger.Initialize(Debug.Log, Debug.Log, Debug.LogWarning, Debug.LogError, false);

        Server = new Server();
        Server.Start(port, maxClientNum);
        Server.ClientDisconnected += PlayerLeft;
    }

    private void FixedUpdate() //FixedUpdate
    {
       Server.Tick();
    }

    private void OnApplicationQuit() //On Quit
    {
        Server.Stop();
    }

    private void PlayerLeft(object sender, ClientDisconnectedEventArgs e)
    {
        Destroy(Player.list[e.Id].gameObject);
    }
}
