namespace NabbTracker
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

    using Font = SharpDX.Direct3D9.Font;
    using Color = System.Drawing.Color;    
    
    /// <summary>
    ///     The main class.
    /// </summary>
    internal class Track
    {
        /// <summary>
        ///     The Menu.
        /// </summary>
        Menu Menu;
        
        /// <summary>
        ///     The Text fcnt.
        /// </summary>
        Font DisplayTextFont = new Font(Drawing.Direct3DDevice, new System.Drawing.Font("Tahoma", 8));
        
        /// <summary>
        ///     The SpellLevel Text font.
        /// </summary>
        Font DisplayLevelFont = new Font(Drawing.Direct3DDevice, new System.Drawing.Font("Comic Sans", 14));
     
        /// <summary>
        ///     Gets the Summoner-Spell name.
        /// </summary>
        string GetSummonerSpellName;
        
        /// <summary>
        ///     Gets the Spellslots
        /// </summary>
        private SpellSlot[]
            SpellSlots = {
                SpellSlot.Q,
                SpellSlot.W,
                SpellSlot.E,
                SpellSlot.R
            },
            
            SummonerSpellSlots = { 
                SpellSlot.Summoner1,
                SpellSlot.Summoner2
            }
        ;

        /// <summary>
        ///     The tracker.
        /// </summary>
        public Track()
        {
            (Menu = new Menu("【红叶推介】Nabb 简版CD", "NabbTracker", true)).AddToMainMenu();
            {
                Menu.AddItem(new MenuItem("display.allies", "Track Allies").SetValue(true));
                Menu.AddItem(new MenuItem("display.enemies", "Track Enemies").SetValue(true));
                Menu.AddItem(new MenuItem("display.spell_levels", "Track Spell levels").SetValue(true));
            }
            Menu.AddItem(new MenuItem("enable", "Enable").SetValue(true));
        
            Drawing.OnDraw += Drawing_OnDraw;
        }

        /// <summary>
        ///     Called when the game draws itself.
        /// </summary>
        /// <param name="args">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void Drawing_OnDraw(EventArgs args)
        {
            if (!Menu.Item("enable").GetValue<bool>()) return;
            
            foreach (var PlayingCharacter in ObjectManager.Get<Obj_AI_Hero>()
                .Where(pg => pg.IsValid
                    && !pg.IsMe
                    && pg.IsHPBarRendered
                    && (pg.IsEnemy && Menu.Item("display.enemies").GetValue<bool>() ||
                        pg.IsAlly && Menu.Item("display.allies").GetValue<bool>())))
            {
                for (int Spell = 0; Spell < SpellSlots.Count(); Spell++)
                {
                    int X = (int)PlayingCharacter.HPBarPosition.X + 10 + (Spell * 25);
                    int Y = (int)PlayingCharacter.HPBarPosition.Y + 35;
                    
                    var GetSpell = PlayingCharacter.Spellbook.GetSpell(SpellSlots[Spell]);
                    var GetSpellCD = GetSpell.CooldownExpires - Game.Time;
                    var SpellCDString = string.Format("{0:0}", GetSpellCD);
                    
                    DisplayTextFont.DrawText(
                        null,
                        GetSpellCD > 0 ?
                        SpellCDString : SpellSlots[Spell].ToString(),
                        
                        X,
                        Y,
                        
                        // Show Grey color if the spell is not learned.
                        GetSpell.Level < 1 ?
                        SharpDX.Color.Gray :
                        
                        // Blue color if the target has not enough mana to use the spell.
                        GetSpell.SData.ManaCostArray.MaxOrDefault((value) => value) > PlayingCharacter.Mana ?
                        SharpDX.Color.Cyan :
                        
                        // Red color if the Spell CD is <= 4 (almost up),
                        GetSpellCD > 0 && GetSpellCD <= 4 ? 
                        SharpDX.Color.Red :
                        
                        // Yellow color if the Spell is on CD, else show Green.
                        GetSpellCD > 0 ?
                        SharpDX.Color.Yellow : SharpDX.Color.LightGreen
                    );
                    
                    if (Menu.Item("display.spell_levels").GetValue<bool>())
                    {
                        for (int DrawSpellLevel = 0; DrawSpellLevel <= GetSpell.Level - 1; DrawSpellLevel++)
                        {
                            int SpellLevelX = X + (DrawSpellLevel * 3) - 4;
                            int SpellLevelY = Y;
                            
                            DisplayLevelFont.DrawText(
                                null,
                                ".",
                                SpellLevelX,
                                SpellLevelY,
                                SharpDX.Color.White    
                            );
                        }
                    }
                }
                
                for (int SummonerSpell = 0; SummonerSpell < SummonerSpellSlots.Count(); SummonerSpell++)
                {
                    int SummonerSpellX = (int)PlayingCharacter.HPBarPosition.X + 10 + (SummonerSpell * 88);
                    int SummonerSpellY = (int)PlayingCharacter.HPBarPosition.Y + 4;
                    
                    var GetSummonerSpell = PlayingCharacter.Spellbook.GetSpell(SummonerSpellSlots[SummonerSpell]);
                    var GetSummonerSpellCD = GetSummonerSpell.CooldownExpires - Game.Time;
                    var SummonerSpellCDString = string.Format("{0:0}", GetSummonerSpellCD);
                    
                    switch (GetSummonerSpell.Name.ToLower())
                    {
                        case "summonerflash":    
                            GetSummonerSpellName = "Flash";
                            break;
                            
                        case "summonerdot":
                            GetSummonerSpellName = "Ignite";
                            break;
                        
                        case "summonerheal":
                            GetSummonerSpellName = "Heal";
                            break;
                        
                        case "summonerteleport":
                            GetSummonerSpellName = "Teleport";
                            break;
                        
                        case "summonerexhaust":
                            GetSummonerSpellName = "Exhaust";
                            break;
                        
                        case "summonerhaste":
                            GetSummonerSpellName = "Ghost";
                            break;
                        
                        case "summonerbarrier":
                            GetSummonerSpellName = "Barrier";
                            break;
                        
                        case "summonerboost":
                            GetSummonerSpellName = "Cleanse";
                            break;
                        
                        case "summonermana":
                            GetSummonerSpellName = "Clarity";
                            break;
                        
                        case "summonerclairvoyance":
                            GetSummonerSpellName = "Clairvoyance";
                            break;
                        
                        case "summonerodingarrison":
                            GetSummonerSpellName = "Garrison";
                            break;
                        
                        case "summonersnowball":
                            GetSummonerSpellName = "Mark";
                            break;
                        
                        default:
                            GetSummonerSpellName = "Smite";
                            break
                        ;
                    }
                    
                    DisplayTextFont.DrawText(
                        null,
                        GetSummonerSpellCD > 0 ? 
                        GetSummonerSpellName + ":" + SummonerSpellCDString : GetSummonerSpellName + ": UP ",
                        
                        SummonerSpellX,
                        SummonerSpellY,
                        
                        GetSummonerSpellCD > 0 ?
                        SharpDX.Color.Red : SharpDX.Color.Yellow
                    );
                }
            }
        }
    }
}
