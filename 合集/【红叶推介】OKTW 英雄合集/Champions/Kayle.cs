﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeagueSharp;
using LeagueSharp.Common;
using SharpDX;

namespace OneKeyToWin_AIO_Sebby.Champions
{
    class Kayle
    {
        private Menu Config = Program.Config;
        public static Orbwalking.Orbwalker Orbwalker = Program.Orbwalker;
        private Spell E, Q, R, W;
        private float QMANA = 0, WMANA = 0, EMANA = 0, RMANA = 0;
        public Obj_AI_Hero Player { get { return ObjectManager.Player; } }
        public void LoadOKTW()
        {
            Q = new Spell(SpellSlot.Q, 670);
            W = new Spell(SpellSlot.W, 900);
            E = new Spell(SpellSlot.E, 660);
            R = new Spell(SpellSlot.R, 900);

            Config.SubMenu(Player.ChampionName).SubMenu("Draw").AddItem(new MenuItem("noti", "Show notification & line", true).SetValue(true));
            Config.SubMenu(Player.ChampionName).SubMenu("Draw").AddItem(new MenuItem("onlyRdy", "Draw only ready spells", true).SetValue(true));
            Config.SubMenu(Player.ChampionName).SubMenu("Draw").AddItem(new MenuItem("qRange", "Q range", true).SetValue(false));
            Config.SubMenu(Player.ChampionName).SubMenu("Draw").AddItem(new MenuItem("wRange", "W range", true).SetValue(false));
            Config.SubMenu(Player.ChampionName).SubMenu("Draw").AddItem(new MenuItem("eRange", "E range", true).SetValue(false));
            Config.SubMenu(Player.ChampionName).SubMenu("Draw").AddItem(new MenuItem("rRange", "R range", true).SetValue(false));

            Config.SubMenu(Player.ChampionName).SubMenu("Q Config").AddItem(new MenuItem("autoQ", "Auto Q", true).SetValue(true));

            Config.SubMenu(Player.ChampionName).SubMenu("W Config").AddItem(new MenuItem("autoW", "Auto W", true).SetValue(true));
            Config.SubMenu(Player.ChampionName).SubMenu("W Config").AddItem(new MenuItem("autoWspeed", "W speed-up", true).SetValue(true));
            foreach (var enemy in ObjectManager.Get<Obj_AI_Hero>().Where(enemy => enemy.IsAlly))
                Config.SubMenu(Player.ChampionName).SubMenu("W Config").SubMenu("W ally:").AddItem(new MenuItem("Wally" + enemy.ChampionName, enemy.ChampionName).SetValue(true));


            Config.SubMenu(Player.ChampionName).SubMenu("E Config").AddItem(new MenuItem("autoE", "Auto E", true).SetValue(true));
            Config.SubMenu(Player.ChampionName).SubMenu("E Config").AddItem(new MenuItem("harrasE", "Harras E", true).SetValue(true));

            Config.SubMenu(Player.ChampionName).SubMenu("R Config").AddItem(new MenuItem("autoR", "Auto R", true).SetValue(true));
            foreach (var enemy in ObjectManager.Get<Obj_AI_Hero>().Where(enemy => enemy.IsAlly))
                Config.SubMenu(Player.ChampionName).SubMenu("R Config").SubMenu("R ally:").AddItem(new MenuItem("Rally" + enemy.ChampionName, enemy.ChampionName).SetValue(true));

            foreach (var enemy in ObjectManager.Get<Obj_AI_Hero>().Where(enemy => enemy.IsEnemy))
                Config.SubMenu(Player.ChampionName).SubMenu("Harras").AddItem(new MenuItem("harras" + enemy.ChampionName, enemy.ChampionName).SetValue(true));

            Config.SubMenu(Player.ChampionName).SubMenu("Farm").AddItem(new MenuItem("farmE", "Lane clear E", true).SetValue(true));
            Config.SubMenu(Player.ChampionName).SubMenu("Farm").AddItem(new MenuItem("jungleQ", "Jungle clear Q", true).SetValue(true));
            Config.SubMenu(Player.ChampionName).SubMenu("Farm").AddItem(new MenuItem("jungleE", "Jungle clear E", true).SetValue(true));

            Game.OnUpdate += Game_OnGameUpdate;
            Drawing.OnDraw += Drawing_OnDraw;
            Obj_AI_Base.OnProcessSpellCast += Obj_AI_Base_OnProcessSpellCast;
        }

        private void Obj_AI_Base_OnProcessSpellCast(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (!R.IsReady() || !sender.IsEnemy || sender.IsMinion || !sender.IsValidTarget(1600) || !Config.Item("autoR", true).GetValue<bool>())
                return;

            foreach (var ally in Program.Allies.Where(ally => Config.Item("Rally" + ally.ChampionName).GetValue<bool>() && ally.IsValid && !ally.IsDead && Player.Distance(ally.ServerPosition) < R.Range))
            {
                double dmg = 0;
                if (args.Target != null && args.Target.NetworkId == ally.NetworkId)
                {
                    dmg = dmg + sender.GetSpellDamage(ally, args.SData.Name);
                }
                else
                {
                    var castArea = ally.Distance(args.End) * (args.End - ally.ServerPosition).Normalized() + ally.ServerPosition;
                    if (castArea.Distance(ally.ServerPosition) < ally.BoundingRadius / 2)
                        dmg = dmg + sender.GetSpellDamage(ally, args.SData.Name);
                    else
                        continue;
                }
                if(dmg > Player.Level * 30)
                    R.Cast(ally);
                else if (ally.Health - dmg < ally.CountEnemiesInRange(900) * ally.Level * 20)
                    R.Cast(ally);
                else if (ally.Health - dmg <  ally.Level * 5)
                    R.Cast(ally);
            }
        }
        
        private void Game_OnGameUpdate(EventArgs args)
        {
            if (Program.LagFree(1))
            {
                SetMana();
                Jungle();
            }

            if (Program.LagFree(2) && W.IsReady() && !Player.IsWindingUp && Config.Item("autoW", true).GetValue<bool>())
                LogicW();
            
            if (Program.LagFree(3) && E.IsReady() && Config.Item("autoE", true).GetValue<bool>())
                LogicE();
            if (Program.LagFree(4) && Q.IsReady() && !Player.IsWindingUp && Config.Item("autoQ", true).GetValue<bool>())
                LogicQ();
        }

        private void LogicQ()
        {
            var t = TargetSelector.GetTarget(Q.Range, TargetSelector.DamageType.Magical);

            if (t.IsValidTarget())
            {
                if (Q.GetDamage(t) > t.Health)
                    Q.Cast(t);
                else if (Program.Combo && ObjectManager.Player.Mana > WMANA + QMANA)
                    Q.Cast(t);
                else if (Program.Farm && Config.Item("harras" + t.ChampionName).GetValue<bool>() && Player.Mana > RMANA + WMANA + QMANA + QMANA)
                    Q.Cast(t);
                else if (Player.Health < Player.Level * 40 && !W.IsReady() && !R.IsReady())
                    Q.Cast(t);
            }
        }

        private void LogicW()
        {
            if (!Player.InFountain() && !Player.HasBuff("Recall") && !Player.IsRecalling())
            {
                Obj_AI_Hero lowest = Player;

                foreach (var ally in Program.Allies.Where(ally => ally.IsValid && !ally.IsDead && Config.Item("Wally" + ally.ChampionName).GetValue<bool>() && Player.Distance(ally.Position) < W.Range))
                {
                    if (ally.Health < lowest.Health)
                        lowest = ally;
                }
                
                if (Player.Mana > WMANA + QMANA && lowest.Health < lowest.Level * 40)
                    W.CastOnUnit(lowest);
                else if (Player.Mana > WMANA + EMANA + QMANA && lowest.Health < lowest.MaxHealth * 0.4 && lowest.Health < 1500)
                    W.CastOnUnit(lowest);
                else if (Player.Mana > Player.MaxMana * 0.5 && lowest.Health < lowest.MaxHealth * 0.7 && lowest.Health < 2000)
                    W.CastOnUnit(lowest);
                else if (Player.Mana > Player.MaxMana * 0.9 && lowest.Health < lowest.MaxHealth * 0.9)
                    W.CastOnUnit(lowest);
                else if (Player.Mana == Player.MaxMana && lowest.Health < lowest.MaxHealth * 0.9)
                    W.CastOnUnit(lowest);
                if (Config.Item("autoWspeed", true).GetValue<bool>())
                {
                    var t = TargetSelector.GetTarget(1000, TargetSelector.DamageType.Magical);
                    if (t.IsValidTarget())
                    {
                        if (Program.Combo && Player.Mana > WMANA + QMANA + EMANA && Player.Distance(t.Position) > Q.Range)
                            W.CastOnUnit(Player);
                    }
                }
            }
        }

        private void LogicE()
        {
            if(Program.Combo && Player.Mana > WMANA + EMANA && Player.CountEnemiesInRange(700) > 0)
                E.Cast();
            else if (Program.Farm && Config.Item("harrasE", true).GetValue<bool>() && Player.Mana > WMANA + EMANA + QMANA && Player.CountEnemiesInRange(500) > 0)
                E.Cast();
            else if (Program.LaneClear && Config.Item("farmE", true).GetValue<bool>() && Player.Mana > WMANA + EMANA + QMANA && FarmE())
                E.Cast();
        }

        private void Jungle()
        {
            if (Program.LaneClear && Player.Mana > RMANA + WMANA + RMANA + WMANA)
            {
                var mobs = MinionManager.GetMinions(Player.ServerPosition, 600, MinionTypes.All, MinionTeam.Neutral, MinionOrderTypes.MaxHealth);
                if (mobs.Count > 0)
                {
                    var mob = mobs[0];
                    if (E.IsReady() && Config.Item("jungleE", true).GetValue<bool>())
                    {
                        E.Cast();
                        return;
                    }
                    if (Q.IsReady() && Config.Item("jungleQ", true).GetValue<bool>())
                    {
                        Q.Cast(mob);
                        return;
                    }
                }
            }
        }

        private bool FarmE()
        {
            var allMinions = MinionManager.GetMinions(Player.ServerPosition, 600, MinionTypes.All);
            foreach (var minion in allMinions)
            {
                return true;
            }
                return false;
        }

        private void SetMana()
        {
            if ((Config.Item("manaDisable", true).GetValue<bool>() && Program.Combo) || Player.HealthPercent < 20)
            {
                QMANA = 0;
                WMANA = 0;
                EMANA = 0;
                RMANA = 0;
                return;
            }

            QMANA = Q.Instance.ManaCost;
            WMANA = W.Instance.ManaCost;
            EMANA = E.Instance.ManaCost;

            if (!Q.IsReady())
                QMANA = QMANA - Player.PARRegenRate * Q.Instance.Cooldown;

        }
        private void Drawing_OnDraw(EventArgs args)
        {
            if (Config.Item("qRange", true).GetValue<bool>())
            {
                if (Config.Item("onlyRdy", true).GetValue<bool>())
                {
                    if (Q.IsReady())
                        Utility.DrawCircle(ObjectManager.Player.Position, Q.Range, System.Drawing.Color.Cyan, 1, 1);
                }
                else
                    Utility.DrawCircle(ObjectManager.Player.Position, Q.Range, System.Drawing.Color.Cyan, 1, 1);
            }
            if (Config.Item("wRange", true).GetValue<bool>())
            {
                if (Config.Item("onlyRdy", true).GetValue<bool>())
                {
                    if (W.IsReady())
                        Utility.DrawCircle(ObjectManager.Player.Position, W.Range, System.Drawing.Color.Orange, 1, 1);
                }
                else
                    Utility.DrawCircle(ObjectManager.Player.Position, W.Range, System.Drawing.Color.Orange, 1, 1);
            }
            if (Config.Item("eRange", true).GetValue<bool>())
            {
                if (Config.Item("onlyRdy", true).GetValue<bool>())
                {
                    if (E.IsReady())
                        Utility.DrawCircle(ObjectManager.Player.Position, E.Range, System.Drawing.Color.Yellow, 1, 1);
                }
                else
                    Utility.DrawCircle(ObjectManager.Player.Position, E.Range, System.Drawing.Color.Yellow, 1, 1);
            }
            if (Config.Item("rRange", true).GetValue<bool>())
            {
                if (Config.Item("onlyRdy", true).GetValue<bool>())
                {
                    if (R.IsReady())
                        Utility.DrawCircle(ObjectManager.Player.Position, R.Range, System.Drawing.Color.Gray, 1, 1);
                }
                else
                    Utility.DrawCircle(ObjectManager.Player.Position, R.Range, System.Drawing.Color.Gray, 1, 1);
            }
        }
    }
}
