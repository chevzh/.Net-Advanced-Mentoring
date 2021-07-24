namespace StockExchange.Task2
{
    public class StockPlayersFactory
    {
        public Players CreatePlayers()
        {
            Players player = new Players();

            player.Blossomers = new Blossomers(player);
            player.RedSocks = new RedSocks(player);
            player.RossStones = new RossStones(player);

            return player;
        }
    }
}
