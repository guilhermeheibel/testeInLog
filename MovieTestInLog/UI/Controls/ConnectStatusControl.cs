using Plugin.Connectivity.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MovieTestInLog.UI.Controls
{
   public class ConnectStatusControl
    {
        IConnectivity connectivity { get; }
        public async Task<bool> VerifyConnect()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                
                if (connectivity.IsConnected && await connectivity.IsRemoteReachable(Constants.ApplicationURL, 5))
                    if (current == NetworkAccess.Internet)
                        return true;
            }
            catch (Exception ex) { Debug.WriteLine("ERRO NA API - !!!SEM CONEXÃO!!! - " + ex.Message); }
            return false;

        }
    }
}
