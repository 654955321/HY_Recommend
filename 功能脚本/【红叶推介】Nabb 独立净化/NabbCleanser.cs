namespace NabbCleanser
{    
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;

    using LeagueSharp;
    using LeagueSharp.Common;

    using SharpDX;
    using SharpDX.Direct3D9;
    
    /// <summary>
    ///    The main class.
    /// </summary>
    internal class Cleanse
    {
        #region variables
        public static Menu Menu;
        private SpellSlot cleanse;

        int Mercurial = 3139;
        #endregion

        /// <summary>
        ///    The cleanser.
        /// </summary>
        public Cleanse()
        {
            (Menu = new Menu("【红叶推介】Nabb 独立净化", "NabbCleanser", true)).AddToMainMenu();
            {
                Menu.AddItem(new MenuItem("use.cleanse", "Use Cleanse.").SetValue(true));
                Menu.AddItem(new MenuItem("use.cleansers", "Use Cleansers.").SetValue(true));
                Menu.AddItem(new MenuItem("use.cleansevsignite", "Cleanse enemy Ignite.").SetValue(true));
                Menu.AddItem(new MenuItem("use.info1", "Cleansers = QSS, Dervish, Mercurial, Mikaels"));
                Menu.AddItem(new MenuItem("use.separator1", ""));
                Menu.AddItem(new MenuItem("panic_key_enable", "Only Cleanse when pressed button enable").SetValue(true));
                Menu.AddItem(new MenuItem("use.panic_key", "Only Cleanse when pressed button").SetValue(new KeyBind(32, KeyBindType.Press)));
                Menu.AddItem(new MenuItem("use.delay", "Delay cleanse/cleansers usage by X ms.").SetValue(new Slider(500, 0, 2000)));
            }
            BuildMikaelsMenu(Menu);
            
            Game.OnUpdate += Game_OnGameUpdate;
        }
        
        /// <summary>
        ///    Declares whenever the target unit has no protection.
        /// </summary>
        bool HasNoProtection(Obj_AI_Hero target)
        {
            // return "true" if the Player..
            return 
                //..has no SpellShield..
                !target.HasBuffOfType(BuffType.SpellShield)
                
             //..nor SpellImmunity.  
             && !target.HasBuffOfType(BuffType.SpellImmunity)
            ; 
        }
        
        /// <summary>
        ///    Called when the player needs to use cleanse.
        /// </summary>
        bool ShouldUseCleanse(Obj_AI_Hero target)
        {
            // return "true" if the Player is being affected by..
            return (
                // ..Charms..
                target.HasBuffOfType(BuffType.Charm)
                
             // ..or Fears..
             || target.HasBuffOfType(BuffType.Flee)
             
             // ..or Polymorphs..
             || target.HasBuffOfType(BuffType.Polymorph)
             
             // ..or Snares..
             || target.HasBuffOfType(BuffType.Snare)
             
             // ..or Stuns..
             || target.HasBuffOfType(BuffType.Stun)
             
             // ..or Taunts..
             || target.HasBuffOfType(BuffType.Taunt)
             
             // ..or Exhaust..
             || target.HasBuff("summonerexhaust")
             )
            
             //..and, if he has no protection..
             && HasNoProtection(target)
             
             // ..and the relative option is enabled.
             && Menu.Item("use.cleanse").GetValue<bool>()
            ; 
        }

        /// <summary>
        ///    Called when the player needs to use a cleanser Item.
        /// </summary>
        bool ShouldUseCleanser()
        {
            // return "true" if the Player is being affected by..
            return (
                // ..Zed's Target Mark (R)..
                ObjectManager.Player.HasBuff("ZedR")

             // ..or Vladimir's Mark (R)..
             || ObjectManager.Player.HasBuff("VladimirHemoplague")
             
             // ..or Mordekaiser's Mark (R)..
             || ObjectManager.Player.HasBuff("MordekaiserChildrenOfTheGrave")
             
             // ..or Poppy's Immunity Mark (R)..
             || ObjectManager.Player.HasBuff("PoppyDiplomaticImmunity")
             
             // ..or Fizz's Fish Mark (R)..
             || ObjectManager.Player.HasBuff("FizzMarinerDoom")
            
             // ..or Suppressions..
             || ObjectManager.Player.HasBuffOfType(BuffType.Suppression)
             )
             
             //..and, if he has no protection..
             && HasNoProtection(ObjectManager.Player)
             
             // ..and the relative option is enabled.
             && Menu.Item("use.cleansers").GetValue<bool>()
            ; 
        }
        
        /// <summary>
        ///    Called when the player needs to cleanse enemy ignite.
        /// </summary>
        bool CanAndShouldCleanseIfIgnited()
        {
            // return "true" if..
            return 
                // ..the player is ignited..
                ObjectManager.Player.HasBuff("summonerdot")
                
             // ..or the player has an invulnerability source..
             && !ObjectManager.Player.HasBuffOfType(BuffType.Invulnerability)
                
             // ..and the relative option is enabled.
             && Menu.Item("use.cleansevsignite").GetValue<bool>()
            ;
        }
        
        /// <summary>
        ///    Builds the Mikaels Menu.
        /// </summary>
        /// <param name="Menu">
        ///    The Menu
        /// </param>
        public void BuildMikaelsMenu(Menu Menu)
        {            
            var MikaelsMenu = new Menu("Mikaels Options", "use.mikaelsmenu");
            
            foreach (var ally in ObjectManager.Get<Obj_AI_Hero>()
                .Where(h => h.IsAlly)
                .Select(hero => hero.ChampionName)
                .ToList())
            {
                MikaelsMenu.AddItem(new MenuItem(string.Format("use.mikaels.{0}", ally.ToLowerInvariant()), ally).SetValue(true));
            }
            MikaelsMenu.AddItem(new MenuItem("enable.mikaels", "Enable Mikaels Usage").SetValue(true));

            Menu.AddSubMenu(MikaelsMenu);
        }
        
        /// <summary>
        ///    Called when the player uses a cleanser.
        /// </summary>
        private void UseCleanser()
        {
            // if the player has QuickSilver Sash and is able to use it..
            if (Items.HasItem(3140)
                && Items.CanUseItem(3140))
            {
                // ..JUST (DO)USE IT!
                Items.UseItem(3140);
                return;
            }    
            
            // else if the player has Dervish Blade and is able to use it..
            else if (Items.HasItem(3137)
                && Items.CanUseItem(3137))
            {    
                // ..JUST (DO)USE IT!
                Items.UseItem(3137);
                return;
            }
            
            // else if the player has Mikaels Crucible and is able to use it..
            else if (Items.HasItem(3222)
                && Items.CanUseItem(3222))
            {    
                // ..JUST (DO)USE IT!
                Items.UseItem(3222);
                return;
            }
 
            // else if the player has Mercurial Scimitar and is able to use it..
            else if (Items.HasItem(3139)
                && Items.CanUseItem(3139))
            {    
                // ..JUST (DO)USE IT!
                Items.UseItem(3139);
                return;
            }
        }
        
        /// <summary>
        ///    Called when the game updates itself.
        /// </summary>
        /// <param name="args">
        ///    The <see cref="EventArgs" /> instance containing the event data.
        /// </param>
        private void Game_OnGameUpdate(EventArgs args)
        {
            // Don't use the assembly if the player is dead.
            if (ObjectManager.Player.IsDead)
            {    
                return;
            }
            
            // If the only-cleanse-if-key-pressed option is enabled and the relative key is being pressed or the only-cleanse-if-key-pressed option is disabled..
            if ((Menu.Item("panic_key_enable").GetValue<bool>() && Menu.Item("use.panic_key").GetValue<KeyBind>().Active) 
                || (!Menu.Item("panic_key_enable").GetValue<bool>()))
            {
                cleanse = ObjectManager.Player.GetSpellSlot("summonerboost");
                var CleanseDelay = Menu.Item("use.delay").GetValue<Slider>().Value;
                var IsCleanseReady = ObjectManager.Player.Spellbook.CanUseSpell(cleanse) == SpellState.Ready;

                // For each ally enabled on the menu-option..
                foreach (var ally in ObjectManager.Get<Obj_AI_Hero>()
                    .Where(h => h.IsAlly
                        && Menu.Item(string.Format("use.mikaels.{0}", h.ChampionName.ToLowerInvariant())).GetValue<bool>()
                        && Menu.Item("enable.mikaels").GetValue<bool>()))
                {
                    // if the player has Mikaels and is able to use it..
                    if (Items.HasItem(3222) && Items.CanUseItem(3222))
                    {
                        // If the ally should be cleansed..
                        if (ShouldUseCleanse(ally))
                        {
                            // ..JUST (DO)CLEANSE HIM!
                            Utility.DelayAction.Add(CleanseDelay,
                                () =>
                                {
                                    Items.UseItem(3222, ally);
                                }
                            );
                            return;
                        }
                    }
                }
                
                // If you are being affected by movement-empairing or control-denying cctype or you are being affected by summoner Ignite..
                if (ShouldUseCleanse(ObjectManager.Player) || CanAndShouldCleanseIfIgnited())
                {
                    // If the player actually has the summonerspell Cleanse and it is ready to use..
                    if (cleanse != SpellSlot.Unknown && IsCleanseReady)
                    {
                        // ..JUST (DO)CLEANSE IT!
                        Utility.DelayAction.Add(CleanseDelay,
                            () => 
                            {
                                ObjectManager.Player.Spellbook.CastSpell(cleanse, ObjectManager.Player);
                                return;
                            }
                        );
                    }    
                }
                
                // If the player is being affected by Hard CC..
                if (ShouldUseCleanser())
                {
                    // If the player is being affected by the DeathMark..
                    if (ObjectManager.Player.HasBuff("zedulttargetmark"))
                    { 
                        // ..Cleanse it, but delay the action by 1,5 seconds.
                        Utility.DelayAction.Add(1500,
                            () =>
                            {
                                UseCleanser();
                            }
                        );
                    }

                    // ..JUST (DO)CLEANSE IT!
                    Utility.DelayAction.Add(CleanseDelay,
                        () =>
                        {
                            UseCleanser();
                        }
                    );
                }
                
                // If the player has not cleanse or cleanse is on cooldown and the player is being affected by hard CC..
                if ((cleanse == SpellSlot.Unknown || !IsCleanseReady) && ShouldUseCleanse(ObjectManager.Player))
                {
                    // ..JUST (DO)CLEANSE IT!
                    Utility.DelayAction.Add(CleanseDelay,
                        () =>
                        {
                            UseCleanser();
                        }
                    );
                }
            }
        }
    }
}
