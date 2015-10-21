// <copyright file="SoulBound.cs" company="Kallen">
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
using System.Collections.Generic;
using LeagueSharp;
using LeagueSharp.Common;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace S_Class_Kalista
{
    internal class SoulBound
    {
        private static readonly Dictionary<float, float> _incomingDamage = new Dictionary<float, float>();
        private static readonly Dictionary<float, float> _instantDamage = new Dictionary<float, float>();
        private static float _lastTick = 0;

        private static float IncomingDamage
        {
            get { return _incomingDamage.Sum(e => e.Value) + _instantDamage.Sum(e => e.Value); }
        }

        public static void Initialize()
        {
            Game.OnUpdate += OnUpdate;
            Obj_AI_Base.OnProcessSpellCast += OnCast;
        }

        private static void OnUpdate(EventArgs args)
        {
            if (_lastTick - Properties.Time.TickCount < 500)
            {
                _lastTick = Properties.Time.TickCount;


                try
                {


                    if (Properties.SoulBoundHero == null)
                        Properties.SoulBoundHero =
                            HeroManager.Allies.Find(
                                ally =>
                                    ally.Buffs.Any(
                                        user => user.Caster.IsMe && user.Name.Contains("kalistacoopstrikeally")));

                    if (Properties.MainMenu.Item("bAutoSaveSoul").GetValue<bool>())
                    {
                        if (Properties.SoulBoundHero.HealthPercent <
                            Properties.MainMenu.Item("sSoulBoundPercent").GetValue<Slider>().Value
                            && Properties.SoulBoundHero.CountEnemiesInRange(500) > 0
                            || IncomingDamage > Properties.SoulBoundHero.Health)
                            Properties.Champion.R.Cast();
                    }


                    foreach (var entry in _incomingDamage.Where(entry => entry.Key < Game.Time))
                    {
                        _incomingDamage.Remove(entry.Key);
                    }

                    foreach (var entry in _instantDamage.Where(entry => entry.Key < Game.Time))
                    {
                        _instantDamage.Remove(entry.Key);
                    }
                }
                catch
                {
                    //
                }
            }
        }

        private static void OnCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!sender.IsEnemy) return;
            if (Properties.SoulBoundHero == null) return;
            // Calculate Damage
            if ((!(sender is Obj_AI_Hero) || args.SData.IsAutoAttack()) && args.Target != null && args.Target.NetworkId == Properties.SoulBoundHero.NetworkId)
            {
                // Calculate arrival time and damage
                _incomingDamage.Add(Properties.SoulBoundHero.ServerPosition.Distance(sender.ServerPosition) / args.SData.MissileSpeed + Game.Time, (float)sender.GetAutoAttackDamage(Properties.SoulBoundHero));
            }
            // Sender is a hero
            else if (sender is Obj_AI_Hero)
            {
                var attacker = (Obj_AI_Hero)sender;
                var slot = attacker.GetSpellSlot(args.SData.Name);

                if (slot != SpellSlot.Unknown)
                {
                    if (slot == attacker.GetSpellSlot("SummonerDot") && args.Target != null && args.Target.NetworkId == Properties.SoulBoundHero.NetworkId)
                        _instantDamage.Add(Game.Time + 2, (float)attacker.GetSummonerSpellDamage(Properties.SoulBoundHero, Damage.SummonerSpell.Ignite));
                    
                    else if (slot.HasFlag(SpellSlot.Q | SpellSlot.W | SpellSlot.E | SpellSlot.R) &&
                        ((args.Target != null && args.Target.NetworkId == Properties.SoulBoundHero.NetworkId) ||
                        args.End.Distance(Properties.SoulBoundHero.ServerPosition) < Math.Pow(args.SData.LineWidth, 2)))
                        _instantDamage.Add(Game.Time + 2, (float)attacker.GetSpellDamage(Properties.SoulBoundHero, slot));
                    
                }
            }

            if (!Properties.MainMenu.Item("bBST").GetValue<bool>()) return;
            var buffName = "";
            switch (Properties.SoulBoundHero.ChampionName)
            {
                case "Blitzcrank":
                    buffName = "rocketgrab2";
                    break;

                case "Skarner":
                    buffName = "skarnerimpale";
                    break;

                case "TahmKench":
                    buffName = "tahmkenchwdevoured";
                    break;
            }

            if (Properties.MainMenu.Item("bBST").GetValue<bool>() && buffName.Length > 0)
            {
                foreach (var target in ObjectManager.Get<Obj_AI_Hero>())
                {
                    if (!target.IsValid) continue;
                    if (!target.IsEnemy) continue;
                    if (target.Distance(ObjectManager.Player) > 2300f) continue;// out of range
                    if (target.Buffs == null) continue;
                    for (var i = 0; i < target.Buffs.Count(); i++)
                    {
                        if (target.Distance(ObjectManager.Player) > Properties.MainMenu.Item("sBST").GetValue<Slider>().Value)continue;
                        if (target.Buffs[i].Name.ToLower() != buffName || !target.Buffs[i].IsActive) continue;
                        Properties.Champion.R.Cast();//Else
                    }
                }
            }

        }
    }
}