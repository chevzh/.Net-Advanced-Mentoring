namespace StockExchange.Task4
{
    public class StockPlayersFactory
    {
        public Players CreatePlayers()
        {
            Players player = new Players();

            player.Blossomers = new Blossomers(player);
            player.RedSocks = new RedSocks(player);
            player.RossSocks = new RossSocks(player);

            return player;
        }
    }
}
