namespace BannersOfRogues.Interfaces {
    public interface IAbility
    {
        string Name { get; }
        int Cooldown { get; }
        int CurrCD { get; }

        bool Perform();
        void Tick();
    }
}