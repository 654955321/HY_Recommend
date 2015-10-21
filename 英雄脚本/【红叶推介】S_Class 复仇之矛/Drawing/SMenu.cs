// <copyright file="SMenu.cs" company="Kallen">
//   Copyright (C) 2015 LeagueSharp Kallen
//
//             This program is free software: you can redistribute it and/or modify
//             it under the terms of the GNU General Public License as published by
//             the Free Software Foundation, either version 3 of the License, or
//             (at your option) any later version.
//
//             This program is distributed in the hope that it will be useful,
//             but WITHOUT ANY WARRANTY; without even the implied warranty of
//             MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//             GNU General Public License for more details.
//
//             You should have received a copy of the GNU General Public License
//             along with this program.  If not, see <http://www.gnu.org/licenses/>.
// </copyright>
// <summary>
//   Assembly to be use with LeagueSharp for champion Kalista
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using LeagueSharp;
using LeagueSharp.Common;
using Color = System.Drawing.Color;
namespace S_Class_Kalista
{
    internal class SMenu
    {
        private const string MenuName = "【红叶推介】S_Class 复仇之矛";
        public static char Walker { get; set; }

        public static void GenerateMenu()
        {
            Properties.MainMenu = new Menu(MenuName, MenuName, true);
            Properties.MainMenu.AddSubMenu(new Menu("Created by Kallen", "ddsfhjsjhdfjhdsfjhdfsjhdf"));
            //Properties.MainMenu.AddSubMenu(HumanizerMenu());
            Properties.MainMenu.AddSubMenu(OrbwalkManagerMenu());
            
            //Reset Walkers
            Properties.SkyWalker = null;
            Properties.CommonWalker = null;

            //Console.WriteLine("Test1");
            //Load Desired Walker
            switch (Properties.MainMenu.Item("sOrbwalker").GetValue<StringList>().SelectedIndex)
            {
                case 0:
                   // Console.WriteLine("Case 1");
                    Properties.MainMenu.AddSubMenu(SkyWalkerMenu());
                    Properties.SkyWalker = new SkyWalker.Orbwalker(Properties.MainMenu.SubMenu("skyWalker"));
                break;

                case 1:
                   // Console.WriteLine("Case 2");
                    Properties.MainMenu.AddSubMenu(CommonWalkerMenu());
                    Properties.CommonWalker = new Orbwalking.Orbwalker(Properties.MainMenu.SubMenu("commonWalker"));
                    break;
            }
            Properties.MainMenu.AddSubMenu(DrawingMenu());
            Properties.MainMenu.AddSubMenu(AutoEvents());
            Properties.MainMenu.AddSubMenu(MixedMenu());
            Properties.MainMenu.AddSubMenu(ComboMenu());
            Properties.MainMenu.AddSubMenu(LaneClearMenu());
            Properties.MainMenu.AddSubMenu(ManaMenu());
            Properties.MainMenu.AddSubMenu(ItemMenu());
            Properties.MainMenu.AddSubMenu(MiscMenu());
        }

        private static Menu ItemMenu()
        {
            var itemMenu = new Menu("Item Options", "itemOptions");
            itemMenu.AddItem(new MenuItem("bUseBork", "Smart BotRK/Cutlass Usage").SetValue(true));
            itemMenu.AddItem(new MenuItem("sEnemyBorkHP", "Max% HP Target has remaining").SetValue(new Slider(50, 0, 100)));
            itemMenu.AddItem(new MenuItem("sMinPlayerHP", "Min% HP Remaining for player(Ignores other settings)").SetValue(new Slider(20, 0, 100)));
            itemMenu.AddItem(new MenuItem("bUseYoumuu", "Smart Youmuu's Usage").SetValue(true));
            itemMenu.AddItem(new MenuItem("bUseQSS", "Smart QSS Usage").SetValue(true));
            itemMenu.AddItem(new MenuItem("bUseMerc", "Smart Merc Usage").SetValue(true));
            itemMenu.AddItem(new MenuItem("bUseOffensiveOnlyInCombo", "Only use offensive items in combo").SetValue(true));
            itemMenu.AddItem(new MenuItem("bUseDefensiveOnlyInCombo", "Only use defensive items in combo").SetValue(true));
            return itemMenu;
        }

        private static Menu OrbwalkManagerMenu()
        {
            var oMenu = new Menu("Orbwalker Options", "orbwalkOptions");
            oMenu.AddItem(new MenuItem("sOrbwalker", "Orbwalker (Changes Require Reload [F5])").SetValue(new StringList(new[] { "SkyWalker", "Common Orbwalker" })));
            return oMenu;
        }

        private static Menu SkyWalkerMenu()
        {
            var orbWalkingMenu = new Menu("Sky Walker", "skyWalker");
            return orbWalkingMenu;
        }

        private static Menu CommonWalkerMenu()
        {
            var orbWalkingMenu = new Menu("Common OrbWalker", "commonWalker");
            var targetSelectorMenu = new Menu("Target Selector", "targetSelect");
            TargetSelector.AddToMenu(targetSelectorMenu);
            orbWalkingMenu.AddSubMenu(targetSelectorMenu);
            return orbWalkingMenu;
        }

        private static Menu MixedMenu()
        {
            var mixedMenu = new Menu("Mixed Options", "mixedOptions");
            mixedMenu.AddItem(new MenuItem("bUseQMixed", "Auto Q").SetValue(true));
            mixedMenu.AddItem(new MenuItem("bUseQMixedReset", "Reset Auto-Attack with Q").SetValue(false));
            mixedMenu.AddItem(new MenuItem("bUseEMixed", "Auto E on X Stacks").SetValue(false));
            mixedMenu.AddItem(new MenuItem("sMixedStacks", ">> Required E Stacks").SetValue(new Slider(4, 2, 15)));

            return mixedMenu;
        }

        private static Menu ComboMenu()
        {
            var mixedMenu = new Menu("Combo Options", "comboMenu");
            mixedMenu.AddItem(new MenuItem("bUseQCombo", "Auto Q").SetValue(false));
            mixedMenu.AddItem(new MenuItem("bUseQComboReset", "Reset Auto-Attack With Q").SetValue(false));
            mixedMenu.AddItem(new MenuItem("bUseECombo", "Auto E to Kill Enemy").SetValue(false));
            mixedMenu.AddItem(new MenuItem("bUseMinionComboWalk", "[BETA] Orbwalk by Using Minions").SetValue(false));
            return mixedMenu;
        }

        private static Menu LaneClearMenu()
        {
            var laneClearMenu = new Menu("Lane Clear Options", "laneClearOptions");
            laneClearMenu.AddItem(new MenuItem("bUseELaneClear", "Auto E to Kill Minions").SetValue(true));
            laneClearMenu.AddItem(new MenuItem("sLaneClearMinionsKilled", ">> Min. Minions to Use E").SetValue(new Slider(3, 2, 10)));

            laneClearMenu.AddItem(new MenuItem("bUseQLaneClear", "Use Q to Kill Minions").SetValue(false));
            laneClearMenu.AddItem(new MenuItem("sLaneClearMinionsKilledQ", ">> Min. Minions to Use Q").SetValue(new Slider(3, 2, 10)));

            laneClearMenu.AddItem(new MenuItem("bUseJungleClear", "Toggle Jungle Clear").SetValue(new KeyBind('G', KeyBindType.Toggle)));
            return laneClearMenu;
        }

        private static Menu DrawingMenu()
        {
            var drawMenu = new Menu("Drawing Settings", "Drawings");

            drawMenu.AddItem(new MenuItem("bDraw", "Display Drawing").SetValue(true));
            drawMenu.AddItem(new MenuItem("cDrawRendRange", "Draw Rend Range").SetValue(new Circle(true, Color.LightSkyBlue)));

            drawMenu.AddItem(new MenuItem("bDrawOnChamp", "Draw Damage Fill on Enemy").SetValue(true));
            drawMenu.AddItem(new MenuItem("cDrawTextOnChamp", "Display Killable-Text on Enemy").SetValue(new Circle(true, Color.Red)));

            drawMenu.AddItem(new MenuItem("cDrawFillOnChamp", "Draw Combo Damage on Enemy").SetValue(new Circle(true, Color.DarkGray)));

            drawMenu.AddItem(new MenuItem("bDrawTextOnSelf", "Display Floating-Text (Self)").SetValue(true));

            drawMenu.AddItem(new MenuItem("cDrawOnMonsters", "Draw Damage on Monsters").SetValue(new Circle(true, Color.LightSlateGray)));
            drawMenu.AddItem(new MenuItem("cFillMonster", "Draw Damage Fill on Monsters").SetValue(new Circle(true, Color.DarkGray)));
            drawMenu.AddItem(new MenuItem("cKillableText", "Display Killable-Text on Monsters").SetValue(new Circle(true, Color.Red)));

            return drawMenu;
        }

        private static Menu AutoEvents()
        {
            var autoEventsMenu = new Menu("Automatic Events", "autoEvents");
            autoEventsMenu.AddItem(new MenuItem("bAutoLevel", "Auto-level Skills").SetValue(true));
            autoEventsMenu.AddItem(new MenuItem("bAutoBuyOrb", "Auto-buy Orb at 6").SetValue(true));
            autoEventsMenu.AddItem(new MenuItem("bUseEToKillEpics", "Auto E Epic Camps").SetValue(true));
            autoEventsMenu.AddItem(new MenuItem("bUseEToKillBuffs", "Auto E Buff Camps").SetValue(true));
            autoEventsMenu.AddItem(new MenuItem("bUseEToAutoKill", "Auto E to Kill Enemy").SetValue(true));
            autoEventsMenu.AddItem(new MenuItem("bUseEToAutoKillMinions", "Use E to Farm Minions").SetValue(true));
            autoEventsMenu.AddItem(new MenuItem("sAutoEMinionsKilled", ">> Min. Minions to Use E").SetValue(new Slider(2, 2, 10)));
            autoEventsMenu.AddItem(new MenuItem("bAutoEOnStacksAndMinions", "Use E on Minions Even if Enemy Has Stacks").SetValue(true));
            autoEventsMenu.AddItem(new MenuItem("sUseEOnMinionKilled", ">> Min. Minions to Use E").SetValue(new Slider(3, 1, 10)));
            autoEventsMenu.AddItem(new MenuItem("sUseEOnChampStacks", ">> Min. Stacks on Enemy").SetValue(new Slider(1, 1, 10)));
            autoEventsMenu.AddItem(new MenuItem("bUseENonKillables", "Use E on Minions That Can't be AA'd").SetValue(true));
            autoEventsMenu.AddItem(new MenuItem("bEBeforeDeath", "Auto E Before Dying").SetValue(false));
            autoEventsMenu.AddItem(new MenuItem("sEBeforeDeathChamps", ">> Min. # of Enemies with Stacks").SetValue(new Slider(1, 1, 5)));
            autoEventsMenu.AddItem(new MenuItem("sEBeforeDeathMinStacks", ">> Min. Stacks On Enemies").SetValue(new Slider(3, 1, 10)));
            autoEventsMenu.AddItem(new MenuItem("sEBeforeDeathMaxHP", ">> % HP to Use E Before Dying").SetValue(new Slider(10, 1, 30)));
            autoEventsMenu.AddItem(new MenuItem("bUseEOnLeave", "Auto E if Leaving Rend Range").SetValue(false));
            autoEventsMenu.AddItem(new MenuItem("sStacksOnLeave", ">> Min. Stacks on Enemy to Use E").SetValue(new Slider(4, 1, 10)));
            autoEventsMenu.AddItem(new MenuItem("bAutoSaveSoul", "Auto-save Soulbound Ally").SetValue(true));
            autoEventsMenu.AddItem(new MenuItem("sSoulBoundPercent", ">> Min. Soulbound Ally % HP to Save").SetValue(new Slider(10, 1, 90)));
            autoEventsMenu.AddItem(new MenuItem("bBST", "Auto-combo R (Blitz, Tahm, Skarner)").SetValue(false));
            autoEventsMenu.AddItem(new MenuItem("sBST", ">> Min. Distance to Activate R-Combo").SetValue(new Slider(1000, 350, 2450)));
            return autoEventsMenu;
        }

        private static Menu MiscMenu()
        {
            var autoEventsMenu = new Menu("Miscellaneous", "miscMenu");
            //autoEventsMenu.AddItem(new MenuItem("slOrbwalker", "Orbwalker To Use").SetValue(new StringList(new[] { "Old Orbwalker", "New Orbwalker" })));
            autoEventsMenu.AddItem(new MenuItem("bSentinel", "Use Sentinel While in Range").SetValue(new KeyBind('T', KeyBindType.Press)));
            autoEventsMenu.AddItem(new MenuItem("bSentinelDragon", "Send to Dragon Camp").SetValue(true));
            autoEventsMenu.AddItem(new MenuItem("bSentinelBaron", "Send to Baron Camp").SetValue(true));
            autoEventsMenu.AddItem(new MenuItem("slQprediction", "Set Q Prediction").SetValue(new StringList(new[] { "Very High", "High", "Dashing" })));
            return autoEventsMenu;
        }

        private static Menu ManaMenu()
        {
            var autoEventsMenu = new Menu("Mana Manager", "manaMenu");
            autoEventsMenu.AddItem(new MenuItem("bUseManaManager", "Use Mana Manager").SetValue(true));
            autoEventsMenu.AddItem(new MenuItem("sMinManaQ", ">> Min. % Mana for Q").SetValue(new Slider(15, 10, 40)));
            autoEventsMenu.AddItem(new MenuItem("sMinManaE", ">> Min. % Mana for E").SetValue(new Slider(0, 0, 30)));
            return autoEventsMenu;
        }
    }
}
