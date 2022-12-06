namespace MooGame
{
    public interface IDataAccess
    {
        List<Player> GetplayersList();
        void PostPlayersList(List<Player> plyers);
    }
}