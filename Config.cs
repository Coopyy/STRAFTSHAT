using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace STRAFTSHAT
{
    public class Config
    {
        public static Config Instance = new Config();

        public bool ESP = true;
        public bool InfiniteAmmo = false;
        public bool RapidFire = false;
        public bool InstaKill = false;
        public bool NoSpread = false;

        public void Draw()
        {
            GUILayout.Box("Visuals", GUILayout.ExpandWidth(true));
            ESP = GUILayout.Toggle(ESP, "ESP");

            GUILayout.Box("Weapon Mods", GUILayout.ExpandWidth(true));
            InfiniteAmmo = GUILayout.Toggle(InfiniteAmmo, "Infinite Ammo");
            InstaKill = GUILayout.Toggle(InstaKill, "Insta Kill");
            RapidFire = GUILayout.Toggle(RapidFire, "Rapid Fire");
            NoSpread = GUILayout.Toggle(NoSpread, "No Spread");
        }
    }
}
