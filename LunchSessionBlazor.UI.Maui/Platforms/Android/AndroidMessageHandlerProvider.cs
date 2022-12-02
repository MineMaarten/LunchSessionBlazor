using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Android.Net;

namespace LunchSessionBlazor.UI.Maui
{
    internal partial class MessageHandlerProvider
    {
        public partial HttpMessageHandler GetHttpMessageHandler() => new AndroidMessageHandler()
        {
            //https://gist.github.com/Eilon/49e3c5216abfa3eba81e453d45cba2d4#file-devhttpsconnectionhelper-cs-L31
            ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert != null && cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == SslPolicyErrors.None;
            }
        };
    }
}
