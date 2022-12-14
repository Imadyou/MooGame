namespace MooGame
{
    public interface IDataAccess
    {
        List<Player> GetPlayersList();
        void PostPlayersList(List<Player> plyers);
    }
}