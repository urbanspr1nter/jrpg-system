using System;
using System.Collections.Generic;
using Jrpg.System;
using UnityEngine;

namespace Jrpg.Unity.Menus
{
    public static class DebugHelper
    {
        private static readonly int DisplayLimit = 5;
        private static readonly string DebugLines = "DebugLines";
        private static readonly string DisplayLines = "DisplayLines";

        private static int Index = 0;

        private static void MakeDisplayLines()
        {
            var g = GameStore.GetInstance();
            var lines = g.Get<List<string>>(DebugLines);

            if (lines == null)
                g.Put(DisplayLines, new List<string>());

            if (lines.Count < DisplayLimit)
                g.Put(DisplayLines, lines.GetRange(0, lines.Count));
            else
                g.Put(DisplayLines, lines.GetRange(Index, DisplayLimit));

            MenuCacheController.BustCachedMenu("menu-debug");
        }

        public static void DecrementIndex()
        {
            if (Index <= 0)
                Index = 0;
            else
                Index--;

            MakeDisplayLines();
        }

        public static void IncrementIndex()
        {
            var g = GameStore.GetInstance();
            var lines = g.Get<List<string>>(DebugLines);

            if (lines == null)
                return;

            var currentLineCount = lines.Count - 1;
            if (currentLineCount - Index <= DisplayLimit)
                Index = currentLineCount - DisplayLimit;
            else
                Index++;

            MakeDisplayLines();
        }

        public static void Log(string message)
        {
            var g = GameStore.GetInstance();

            List<string> lines = new List<string>();
            try
            {
                var currentLog = g.Get<List<string>>(DebugLines);
                if (currentLog != null)
                {
                    lines = currentLog;
                }
            }
            catch
            {
                g.Put(DebugLines, lines);
            }

            lines.Add(message);

            if (lines.Count > DisplayLimit)
                IncrementIndex();

            g.Put(DebugLines, lines);

            MakeDisplayLines();

            // Now add this message to the Unity Console
            Debug.Log(message);
        }
    }
}
