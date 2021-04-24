using System;

namespace WarpnetDeepwoods
{
    public interface IWarpNetAPI
    {
        void AddCustomDestinationHandler(string ID, Action<string> Warp, Func<string, bool> GetEnabled, Func<string, string> GetLabel);
        void AddCustomDestinationHandler(string ID, Action<string> Warp, bool Enabled, string Label);
        void RemoveCustomDestinationHandler(string ID);
    }
}
