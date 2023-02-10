namespace TicTacToeClassLibrary
{
    public class Player
    {
        public int Id;
        public string Sign { get; set; } = "";
        public Player(int id, string sign)
        {
            Sign = sign;
            Id = id;
        }
        public Player(Player player)
        {
            Sign = player.Sign;
            Id = player.Id;
        }
        public Player() { }
    }
}
