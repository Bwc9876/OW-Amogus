using System;
using OWML.Common;
using OWML.ModHelper;
using QSB;
using QSB.Menus;

namespace Amogus
{
    public class Amogus : ModBehaviour
    {
        public static Amogus Instance;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Instance.ModHelper.Console.WriteLine("Start of Amogus!");
            ModHelper.HarmonyHelper.AddPrefix<QSBCore>("Configure", typeof(Amogus), nameof(Amogus.Rekt));
        }

        public static void Rekt()
        {
            Instance.ModHelper.Console.WriteLine("GET PATCHED BOZO");
        }
    }
}
