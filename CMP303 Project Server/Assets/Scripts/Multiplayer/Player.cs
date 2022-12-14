using RiptideNetworking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Player : MonoBehaviour
{
    public static Dictionary<ushort , Player> list = new Dictionary<ushort , Player>();

    public ushort ID { get; private set; }
    public string Username { get; private set; }

    [MessageHandler((ushort)ClientToServerID.name)]

    public static void Spawn(ushort id, string username)
    {
        Player player = Instantiate(GameLogic.Singleton.PlayerPrefab, new Vector3(0f, 1f, 0f), Quaternion.identity).GetComponent<Player>();
        player.name = $"Player {id} ({(string.IsNullOrEmpty(username) ? "Guest" : username)}";
        player.ID = id ;
        player.Username = string.IsNullOrEmpty(username) ? $"Guest {id}" : username;

        list.Add(id, player);
    }
    private static void Name(ushort fromClientID, Message message)
    {
        Spawn(fromClientID, message.GetString());
    }
}
