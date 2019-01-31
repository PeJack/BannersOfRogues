using BannersOfRogues.Characters;
using BannersOfRogues.Systems;

namespace BannersOfRogues.Interfaces {
    public interface IBehaviour
    {
        bool Act(Enemy enemy, CommandSystem commandSystem, Game game);
    }
}