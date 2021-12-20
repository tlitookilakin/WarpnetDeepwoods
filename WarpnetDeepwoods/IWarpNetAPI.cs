using System;

namespace WarpnetDeepwoods
{
    public interface IWarpNetAPI
    {
        void AddCustomDestinationHandler(string ID, Func<bool> getEnabled, Func<string> getLabel, Func<string> getIconName, Action warp);
        void RemoveCustomDestinationHandler(string ID);
    }
}
