using System.Collections.Generic;
using Example;
using Godot;

public class GameState : Node
{
    private const int DefaultPort = 10567;
    private const int MaxPeers = 12;

    private string _playerName = "The Warrior";
    private Dictionary<int, string> _players = new Dictionary<int, string>();
    private List<string> _playersReady = new List<string>();

    private void PlayerConnected(int id)
    {
        RpcId(id, "RegisterPlayer", _playerName);
    }

    private void PlayerDisconnected(int id)
    {
        if (HasNode("/root/World"))
        {
            if (GetTree().IsNetworkServer())
            {
                EmitSignal("GameError", $"Player {_players[id]} disconnected");
                EndGame();
            }
        }
        else
        {
            UnregisterPlayer(id);
        }
    }

    private void ConnectedOk()
    {
        EmitSignal("ConnectionSucceeded");
    }

    private void ServerDisconnected()
    {
        EmitSignal("GameError", "Server disconnected");
        EndGame();
    }

    private void ConnectedFail()
    {
        GetTree().NetworkPeer = null;
        EmitSignal("ConnectionFailed");
    }

    [Remote]
    private void RegisterPlayer(string newPlayerName)
    {
        var id = GetTree().GetRpcSenderId();
        _players[id] = newPlayerName;
        EmitSignal("PlayerListChanged");
    }

    private void UnregisterPlayer(int id)
    {
        _players.Remove(id);
        EmitSignal("PlayerListChanged");
    }

    [Remote]
    private void PreStartGame(List<int> playerIDs)
    {
        var world = ResourceLoader.Load<PackedScene>("res://world.tscn").Instance();
        GetTree().Root.AddChild(world);

        GetTree().Root.GetNode("Lobby").SetProcess(false);

        var playerScene = ResourceLoader.Load<PackedScene>("res://player.tscn");

        foreach (var pid in playerIDs)
        {
            var spawnPoint = world.GetNode<Node2D>($"SpawnPoints/{pid}").Position;
            var player = playerScene.Instance<Player>();

            player.Name = spawnPoint.ToString();
            player.Position = spawnPoint;
            player.SetNetworkMaster(pid);

            if (pid == GetTree().GetNetworkUniqueId())
            {
                player.SetPlayerName(_playerName);
            }
            else
            {
                player.SetPlayerName(_players[pid]);
            }

            world.GetNode("Players").AddChild(player);
        }

        //	# Set up score.
        //	world.get_node("Score").add_player(get_tree().get_network_unique_id(), player_name)
        //	for pn in players:
        //		world.get_node("Score").add_player(pn, players[pn])
        //
        //	if not get_tree().is_network_server():
        //		# Tell server we are ready to start.
        //		rpc_id(1, "ready_to_start", get_tree().get_network_unique_id())
        //	elif players.size() == 0:
        //		post_start_game()
    }

    [Remote]
    private void PostStartGame()
    {

    }

    [Remote]
    private void ReadyToStart(int id)
    {

    }

    private void HostGame(string newPlayerName)
    {

    }

    private void JoinGame(string ip, string newPlayerName)
    {

    }

    private void GetPlayerList()
    {

    }

    private void BeginGame()
    {

    }

    private void EndGame()
    {
        if (HasNode("/root/World"))
        {
            GetNode("/root/World").QueueFree();
        }
        EmitSignal("GameEnded");
        _players.Clear();
    }

    public override void _Ready()
    {
        GetTree().Connect("NetworkPeerConnected", this, "NetworkPeerConnected");
        GetTree().Connect("NetworkPeerDisconnected", this, "NetworkPeerDisconnected");
        GetTree().Connect("ConnectedToServer", this, "ConnectedToServer");
        GetTree().Connect("ConnectionFailed", this, "ConnectionFailed");
        GetTree().Connect("ServerDisconnected", this, "ServerDisconnected");
    }
}