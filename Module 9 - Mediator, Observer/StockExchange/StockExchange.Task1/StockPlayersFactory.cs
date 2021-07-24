namespace StockExchange.Task1
{
    public class StockPlayersFactory
    {
        public Players CreatePlayers()
        {
            Players player = new Players();

            player.Blossomers = new Blossomers(player);
            player.RedSocks = new RedSocks(player);

            return player;
        }
    }
}
