using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Buildings;

namespace WarpnetDeepwoods
{
    public class ModEntry : Mod
    {
        internal static Config Config;
        private static IWarpNetAPI WarpApi = null;
        public override void Entry(IModHelper helper)
        {
            Config = helper.ReadConfig<Config>();
            helper.Events.GameLoop.GameLaunched += SetupIntegrations;
            helper.Events.Content.AssetRequested += LoadAssets;
        }
        private void SetupIntegrations(object sender, GameLaunchedEventArgs ev)
        {
            Config.RegisterModConfigMenu(Helper, ModManifest);
            WarpApi = Helper.ModRegistry.GetApi<IWarpNetAPI>("tlitookilakin.warpnetwork");
            WarpApi.AddCustomDestinationHandler("deepwoods", CanWarpToDeepWoods, GetWarpLabel, () => "Deepwoods", WarpToDeepWoods);
        }
        private string GetWarpLabel()
            => Helper.Translation.Get("ui-label");
        private static void WarpToDeepWoods()
        {
            Game1.exitActiveMenu();
            Game1.activeClickableMenu = new DeepWoodsMod.WoodsObeliskMenu();
        }
        private static bool CanWarpToDeepWoods()
        {
            if (!Config.AfterObelisk)
                return true;
            Farm farm = Game1.getFarm();
            if(farm is null)
                return false;
            foreach(Building building in farm.buildings)
                if(building.buildingType.ToString() == "Woods Obelisk")
                    return true;
            return false;
        }
        private static void LoadAssets(object _, AssetRequestedEventArgs ev)
        {
            if (ev.NameWithoutLocale.IsEquivalentTo("Data/WarpNetwork/Icons/Deepwoods"))
                ev.LoadFromModFile<Texture2D>("assets/icon.png", AssetLoadPriority.Medium);
        }
    }
}
