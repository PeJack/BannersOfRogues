using RogueSharp;
using UnityEngine;

namespace BannersOfRogues.Interfaces
{
    public interface IDrawable
    {
        Color   Color   { get; set; }
        char    Symbol  { get; set; }
        int     X       { get; set; }
        int     Y       { get; set; }

        void Draw(IMap map);
    }
}