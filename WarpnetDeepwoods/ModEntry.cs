using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Buildings;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarpnetDeepwoods
{
    public class ModEntry : Mod
    {
        internal static WarpnetDeepwoods.Config Config;
        private static IWarpNetAPI WarpApi = null;
        public override void Entry(IModHelper helper)
        {
            Config = helper.ReadConfig<Config>();
            helper.Events.GameLoop.GameLaunched += SetupIntegrations;
        }
        private void SetupIntegrations(object sender, GameLaunchedEventArgs ev)
        {
            Config.RegisterModConfigMenu(Helper, ModManifest);
            WarpApi = Helper.ModRegistry.GetApi<IWarpNetAPI>("tlitookilakin.warpnetwork");
            WarpApi.AddCustomDestinationHandler("deepwoods", WarpToDeepWoods, CanWarpToDeepWoods, GetWarpLabel);
        }
        private string GetWarpLabel(string s)
        {
            return Helper.Translation.Get("ui-label");
        }
        private void WarpToDeepWoods(string s)
        {
            Game1.exitActiveMenu();
            Game1.activeClickableMenu = new DeepWoodsMod.WoodsObeliskMenu();
        }
        private bool CanWarpToDeepWoods(string s)
        {
            if (!Config.AfterObelisk)
            {
                return true;
            }
            Farm farm = Game1.getFarm();
            if(farm is null)
            {
                return false;
            }
            foreach(Building building in farm.buildings)
            {
                if(building.buildingType.ToString() == "Woods Obelisk")
                {
                    return true;
                }
            }
            return false;
        }
    }
}
