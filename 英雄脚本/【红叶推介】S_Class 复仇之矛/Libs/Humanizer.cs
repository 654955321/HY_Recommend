// <copyright file="Humanizer.cs" company="Kallen">
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
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace S_Class_Kalista
//{
//    class Humanizer
//    {
//        private struct Action
//        {
//            public string Name { get; set; }
//            public float Delay { get; set; }
//            public float LastTick { get; set; }
//        }

//        private static DateTime _assemblyLoadTime = DateTime.Now;
//        private static readonly List<Action> ActionDelayList = new List<Action>();

//        public static float TickCount
//        {
//            get
//            {
//                return (int)DateTime.Now.Subtract(_assemblyLoadTime).TotalMilliseconds;
//            }
//        }

//        public static void AddAction(string actionName, float delayMs)
//        {
//            if (ActionDelayList.Any(a => a.Name == actionName)) return; // Id is in list already
//            var nAction = new Action {Name = actionName, Delay = delayMs};
//            ActionDelayList.Add(nAction);
//        }

//        public static void ChangeDelay(string actionName, float nDelay)
//        {
//            var cAction = ActionDelayList.Find(action => action.Name == actionName);
//            if (cAction.Name == null) return;
//            cAction.Delay = nDelay + TickCount;
//        }

//        public static bool CheckDelay(string actionName)
//        {
//            var cAction = ActionDelayList.Find(action => action.Name == actionName);
//            if (cAction.Name == null) return false;

//            Console.WriteLine("Current Last Tick{0} Current Tick{1}" ,cAction.LastTick,TickCount);
//            if (!(TickCount - cAction.LastTick >= cAction.Delay)) return false;
//            float delay = cAction.Delay;
//            ActionDelayList.Remove(cAction);
//            AddAction(actionName, delay);
//            return true;
//        }
//    }
//}