
namespace LeagueSharp.Common
{
    using System;
    using Lost = LeagueSharp.Hacks;
    internal static class Flowers
    {
        public static readonly Menu fl = new Menu("Flowers Utility", "Flowers Utility");
        internal static void Initiate()
        {
            CustomEvents.Game.OnGameLoad += Game_OnGameLoad;
        }

        public static void Game_OnGameLoad(EventArgs args)
        {
            fl.AddItem(new MenuItem("Flowers", "自动设置AA后摇").SetShared().SetValue(false));
            fl.AddItem(new MenuItem("say1", " "));
            fl.AddItem(new MenuItem("Disable Drawing", "Disable Drawing").SetValue(new KeyBind(36, KeyBindType.Toggle)));
            fl.AddItem(new MenuItem("say", "HotKey(default):Home"));
            fl.AddItem(new MenuItem("say1", " "));
            fl.AddItem(new MenuItem("zoom hack", "ZoomHack").SetValue(false));
            fl.AddItem(new MenuItem("say2", " "));
            fl.AddItem(new MenuItem("disable say", "Disable Say").SetValue(false));
            fl.AddItem(new MenuItem("say3", " "));
            fl.AddItem(new MenuItem("Tower Ranges", "Show Tower Ranges").SetValue(false));
            fl.AddItem(new MenuItem("say4", " "));
            fl.AddItem(new MenuItem("say5", "Wait to Add More"));
            CommonMenu.Config.AddSubMenu(fl);

            Game.OnUpdate += Game_OnUpdate;
        }

        static void Game_OnUpdate(EventArgs args)
        {
            if (fl.Item("zoom hack").IsActive())
            {
                Lost.ZoomHack = true;
            }
            else
            {
                Lost.ZoomHack = false;
            }

            if (fl.Item("Disable Drawing").GetValue<KeyBind>().Active)
            {
                Lost.DisableDrawings = true;
            }
            else
            {
                Lost.DisableDrawings = false;
            }

            if (fl.Item("disable say").GetValue<KeyBind>().Active)
            {
                Lost.DisableSay = true;
            }
            else
            {
                Lost.DisableSay = false;
            }

            if (fl.Item("Tower Ranges").GetValue<KeyBind>().Active)
            {
                Lost.TowerRanges = true;
            }
            else
            {
                Lost.TowerRanges = false;
            }
        }
    }
}
