using System;
using LeagueSharp;
using LeagueSharp.Common;
using ItemData = LeagueSharp.Common.Data.ItemData;
using Item = LeagueSharp.Common.Items.Item;

// ReSharper disable once CheckNamespace
namespace S_Class_Kalista
{
    class ItemManager
    {

        //Create Item's and set them inactive
        public struct Offensive
        {
            public static Item Botrk = new Item(ItemData.Blade_of_the_Ruined_King.GetItem().Id);
            public static Item Cutless = new Item(ItemData.Bilgewater_Cutlass.GetItem().Id);
            public static Item Hydra = new Item(ItemData.Ravenous_Hydra_Melee_Only.GetItem().Id);
            public static Item Tiamat = new Item(ItemData.Tiamat_Melee_Only.GetItem().Id);
            public static Item GunBlade = new Item(ItemData.Hextech_Gunblade.GetItem().Id);
            public static Item Muraman = new Item(ItemData.Muramana.GetItem().Id);
            public static Item GhostBlade = new Item(ItemData.Youmuus_Ghostblade.GetItem().Id);
        }

        public struct Defensive
        {
            public static Item Qss = new Item(ItemData.Quicksilver_Sash.GetItem().Id);
            public static Item Merc = new Item(ItemData.Mercurial_Scimitar.GetItem().Id);
        }

        public static void Initilize()
        {
            Orbwalking.AfterAttack += After_Attack;
            Orbwalking.BeforeAttack += Before_Attack;
            Game.OnUpdate += OnUpdate;
        }

        private static int GetDangerLevel()
        {
            var count = 0;
            //Bad
            if (Properties.PlayerHero.HasBuffOfType(BuffType.Suppression))
                count += 3;
            if (Properties.PlayerHero.HasBuffOfType(BuffType.Taunt))
                count += 3;
            if (Properties.PlayerHero.HasBuffOfType(BuffType.Stun))
                count += 3;

            //Not good
            if (Properties.PlayerHero.HasBuffOfType(BuffType.Snare))
                count += 2;
            if (Properties.PlayerHero.HasBuffOfType(BuffType.Polymorph))
                count += 2;
            if (Properties.PlayerHero.HasBuffOfType(BuffType.Charm))
                count += 2;
            if (Properties.PlayerHero.HasBuffOfType(BuffType.Fear))
                count += 2;

            //Meh
            if (Properties.PlayerHero.HasBuffOfType(BuffType.Blind))
                count += 1;
            if (Properties.PlayerHero.HasBuffOfType(BuffType.Flee))
                count += 1;
            if (Properties.PlayerHero.HasBuffOfType(BuffType.Sleep))
                count += 1;
            if (Properties.PlayerHero.HasBuffOfType(BuffType.Slow))
                count += 1;

            return count;
        }


        //var itemMenu = new Menu("Item Options", "itemOptions");
        //itemMenu.AddItem(new MenuItem("bUseBork", "Smart BotRK/Cutlass Usage").SetValue(true));
        //    itemMenu.AddItem(new MenuItem("sEnemyBorkHP", "Max% HP Target has remaining").SetValue(new Slider(50, 0, 100)));
        //    itemMenu.AddItem(new MenuItem("sMinPlayerHP", "Min% HP Remaining for player(different check)").SetValue(new Slider(20, 0, 100)));
        //    itemMenu.AddItem(new MenuItem("bUseYoumuu", "Smart Youmuu's Usage").SetValue(true));
        //    itemMenu.AddItem(new MenuItem("bUseQSS", "Smart QSS Usage").SetValue(true));
        //    itemMenu.AddItem(new MenuItem("bUseMerc", "Smart Merc Usage").SetValue(true));
        //    itemMenu.AddItem(new MenuItem("bUseOffensiveOnlyInCombo", "Only use offensive items in combo").SetValue(true));
        //    itemMenu.AddItem(new MenuItem("bUseDefensiveOnlyInCombo", "Only use defensive items in combo").SetValue(true));
        private static void OnUpdate(EventArgs args)
        {
            #region Offensive

            var target = TargetSelector.GetTarget(1500, TargetSelector.DamageType.Physical);
            if (target == null) return;

            var inCombo = false;
            switch (Properties.MainMenu.Item("sOrbwalker").GetValue<StringList>().SelectedIndex)
            {
                case 0:
                    inCombo = Properties.SkyWalker.ActiveMode == SkyWalker.OrbwalkingMode.Combo;
                    break;

                case 1:
                    inCombo = Properties.CommonWalker.ActiveMode == Orbwalking.OrbwalkingMode.Combo;
                    break;
            }

            if (Properties.MainMenu.Item("bUseBork").GetValue<bool>() && Items.HasItem(Offensive.Botrk.Id))
                // If enabled and has item
            {
                if (Offensive.Botrk.IsReady())
                {
                    if (
                        target.IsValidTarget(Properties.PlayerHero.AttackRange + Properties.PlayerHero.BoundingRadius) ||
                        Properties.PlayerHero.HealthPercent <
                        Properties.MainMenu.Item("sMinPlayerHP").GetValue<Slider>().Value)
                    {
                        // In auto Range or about to die
                            if (Properties.MainMenu.Item("bUseOffensiveOnlyInCombo").GetValue<bool>() && inCombo &&
                                target.HealthPercent < Properties.MainMenu.Item("sEnemyBorkHP").GetValue<Slider>().Value
                                //in combo and target hp less then
                                ||
                                !Properties.MainMenu.Item("bUseOffensiveOnlyInCombo").GetValue<bool>() &&
                                target.HealthPercent < Properties.MainMenu.Item("sEnemyBorkHP").GetValue<Slider>().Value
                                //not in combo but target HP less then
                                ||
                                (Properties.PlayerHero.HealthPercent <
                                 Properties.MainMenu.Item("sMinPlayerHP").GetValue<Slider>().Value))
                                //Player hp less then
                            {
                                Items.UseItem(Offensive.Botrk.Id, target);
                            }
                        
                    }
                }
            }

            if (Properties.MainMenu.Item("bUseBork").GetValue<bool>() && Items.HasItem(Offensive.Cutless.Id))
                // If enabled and has item
            {
                if (Offensive.Cutless.IsReady())
                {
                    if (
                        target.IsValidTarget(Properties.PlayerHero.AttackRange + Properties.PlayerHero.BoundingRadius) ||
                        Properties.PlayerHero.HealthPercent <
                        Properties.MainMenu.Item("sMinPlayerHP").GetValue<Slider>().Value)
                    {
                        // In auto Range or about to die
                        if (Properties.MainMenu.Item("bUseOffensiveOnlyInCombo").GetValue<bool>() && inCombo &&
                            target.HealthPercent <
                            Properties.MainMenu.Item("sEnemyBorkHP").GetValue<Slider>().Value
                            //in combo and target hp less then
                            ||
                            !Properties.MainMenu.Item("bUseOffensiveOnlyInCombo").GetValue<bool>() &&
                            target.HealthPercent <
                            Properties.MainMenu.Item("sEnemyBorkHP").GetValue<Slider>().Value
                            //not in combo but target HP less then
                            ||
                            (Properties.PlayerHero.HealthPercent <
                             Properties.MainMenu.Item("sMinPlayerHP").GetValue<Slider>().Value))
                            //Player hp less then
                        {
                            Items.UseItem(Offensive.Cutless.Id, target);
                        }
                    }
                }
            }

            if (Properties.MainMenu.Item("bUseYoumuu").GetValue<bool>() && Items.HasItem(Offensive.GhostBlade.Id)) // If enabled and has item
            {
                if (Offensive.GhostBlade.IsReady() && target.IsValidTarget(Properties.PlayerHero.AttackRange + Properties.PlayerHero.BoundingRadius)) // Is ready and target is in auto range 
                {
                    if (inCombo)
                    {
                        Items.UseItem(Offensive.GhostBlade.Id);
                    }
                }
            }
            #endregion

            #region Defensive
            if (Properties.MainMenu.Item("bUseQSS").GetValue<bool>() && Items.HasItem(Defensive.Qss.Id))
            {
                if (Properties.MainMenu.Item("bUseDefensiveOnlyInCombo").GetValue<bool>() && inCombo ||
                    !Properties.MainMenu.Item("bUseDefensiveOnlyInCombo").GetValue<bool>())
                {
                    if (Defensive.Qss.IsReady())
                    {
                        if (GetDangerLevel() >= 3)
                            Items.UseItem(Defensive.Qss.Id);
                    }
                }
            }


            if (Properties.MainMenu.Item("bUseMerc").GetValue<bool>() && Items.HasItem(Defensive.Merc.Id))
            {
                if (Properties.MainMenu.Item("bUseDefensiveOnlyInCombo").GetValue<bool>() && inCombo ||
                    !Properties.MainMenu.Item("bUseDefensiveOnlyInCombo").GetValue<bool>())
                {
                    if (Defensive.Merc.IsReady())
                    {
                        if (GetDangerLevel() >= 3)
                            Items.UseItem(Defensive.Merc.Id);
                    }
                }
            }

            #endregion
        }

        private static void Before_Attack(Orbwalking.BeforeAttackEventArgs args)
        {

        }

        private static void After_Attack(AttackableUnit unit, AttackableUnit target)
        {

        }
    }
}