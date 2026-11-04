using System;
using System.Reflection;
using Modding;
using UnityEngine;

namespace MoreGeoDrops
{
    public class Settings
    {
        public float Multiplier = 2f; // غيّرها كما تريد
    }

    public class MoreGeoDrops : Mod, IGlobalSettings<Settings>
    {
        public static Settings S = new Settings();

        public override string GetVersion() => "1.0.0";

        public void OnLoadGlobal(Settings s) => S = s;
        public Settings OnSaveGlobal() => S;

        public override void Initialize()
        {
            On.HealthManager.Start += OnStart;
        }

        private void OnStart(On.HealthManager.orig_Start orig, HealthManager self)
        {
            orig(self);
            Scale(self, "smallGeoDrops");
            Scale(self, "mediumGeoDrops");
            Scale(self, "largeGeoDrops");
        }

        private void Scale(HealthManager hm, string field)
        {
            var f = typeof(HealthManager).GetField(field,
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            if (f == null) return;
            int old = (int) f.GetValue(hm);
            f.SetValue(hm, Mathf.RoundToInt(old * S.Multiplier));
        }
    }
}
