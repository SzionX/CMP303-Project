using RiptideNetworking;
using RiptideNetworking.Utils;
using UnityEngine;

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

        //Client Connect method (to be changed)
        Client.Connect($"{ip}:{port}");
    }

    private void FixedUpdate() //FixedUpdate
    {
        Client.Tick();
    }

    private void OnApplicationQuit() //On Quit
    {
       Client.Disconnect();
    }
}
