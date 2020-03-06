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
        
        public async Task<bool> VerifyConnect()
        {
            try
            {
                var current = Connectivity.NetworkAccess;
                
                    if (current == NetworkAccess.Internet)
                        return true;
            }
            catch (Exception ex) { Debug.WriteLine("ERRO NA API - !!!SEM CONEXÃO!!! - " + ex.Message); }
            return false;

        }
    }
}
