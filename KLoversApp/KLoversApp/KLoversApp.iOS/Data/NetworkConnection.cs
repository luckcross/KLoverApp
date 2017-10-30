using CoreFoundation;
using KLoversApp.Data;
using KLoversApp.iOS.Data;
using System;
using SystemConfiguration;
using Xamarin.Forms;

[assembly: Dependency(typeof(NetworkConnection))]

namespace KLoversApp.iOS.Data
{
    public class NetworkConnection : INetworkConnection
    {
        public event EventHandler ReachabilityChanged;

        private NetworkReachability defaultReachability;

        public bool IsConnected { get; set; }

        public void CheckNetworkConnection()
        {
            IsConnected = InternetStatus();
        }

        public bool InternetStatus()
        {
            NetworkReachabilityFlags flags;
            bool defaultNetworkAvailable = IsNetworkAvailable(out flags);

            if (defaultNetworkAvailable && ((flags & NetworkReachabilityFlags.IsDirect) != 0))
            {
                return false;
            }
            else if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
            {
                return true;
            }
            else if (flags == 0)
            {
                return false;
            }

            return true;
        }

        private void OnChange(NetworkReachabilityFlags flags)
        {
            EventHandler h = ReachabilityChanged;
            if (h == null)
                h(null, EventArgs.Empty);
        }
        
        public bool IsNetworkAvailable (out NetworkReachabilityFlags flags)
        {
            if (defaultReachability == null)
            {
                defaultReachability = new NetworkReachability(new System.Net.IPAddress(0));
                defaultReachability.SetNotification(OnChange);
                defaultReachability.Schedule(CFRunLoop.Current, CFRunLoop.ModeDefault);
            }

            if (!defaultReachability.TryGetFlags(out flags))
            {
                return false;
            }

            return IsReachableWithoutRequiringConnection(flags);
        }

        private bool IsReachableWithoutRequiringConnection(NetworkReachabilityFlags flags)
        {
            bool isReachable = (flags & NetworkReachabilityFlags.Reachable) != 0;
            bool noConnectionRequired = (flags & NetworkReachabilityFlags.ConnectionRequired) == 0;

            if ((flags & NetworkReachabilityFlags.IsWWAN) != 0)
                noConnectionRequired = true;

            return isReachable && noConnectionRequired;
        }
    }
}