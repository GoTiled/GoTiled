using Godot;

namespace GoTiled.Example
{
    public class Lobby : Control
    {
        public override void _Ready()
        {
            var gameState = GetNode<GameState>("GameState");

            gameState.Connect("connection_failed", this, "OnConnectionFailed");
            gameState.Connect("connection_succeeded", this, "OnConnectionSuccess");
            gameState.Connect("player_list_changed", this, "RefreshLobby");
            gameState.Connect("game_ended", this, "OnGameEnded");
            gameState.Connect("game_error", this, "OnGameError");

            var username = System.Environment.GetEnvironmentVariable("USERNAME") ?? "NoName";

            GetNode<Label>("Connect/Name").Text = username;
        }

        public void OnHostPressed()
        {
            if (GetNode<Label>("Connect/Name").Text == "")
            {
                GetNode<Label>("Connect/ErrorLabel").Text = "Invalid name!";
                return;
            }

            GetNode<Control>("Connect").Hide();
            GetNode<Control>("Players").Show();
        }
    }
}