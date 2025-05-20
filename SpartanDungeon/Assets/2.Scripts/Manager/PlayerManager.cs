public class PlayerManager : Singleton<PlayerManager>
{
    Player player;

    public Player Player
    {
        get { return player; }
        set { player = value; }
    }
}
