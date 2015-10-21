using System;
using LeagueSharp;
using LeagueSharp.Common;
using VayneHunter_Reborn.Utility;

namespace VayneHunter_Reborn
{
    class Program
    {
        private static string ChampionName = "Vayne";
        static void Main(string[] args)
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        static void Game_OnGameLoad(EventArgs args)
        {
            if (ObjectManager.Player.ChampionName != ChampionName)
            {
                return;
            }
            VHRBootstrap.OnLoad();

            Game.PrintChat("<font color='#FF0000'><b>銆愮孩鍙舵帹浠嬨€慏Z191 钖囨仼 閲嶅仛</b></font> 鍔犺浇鎴愬姛!");
            Game.PrintChat("Also try <font color='#66FF33'><b>Waifu#</b></font>!");
        }
    }
}
