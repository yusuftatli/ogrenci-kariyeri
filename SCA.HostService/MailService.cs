using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SCA.HostService
{
    public class MailService : HostedService
    {
        protected override async Task ExecuteAsync(CancellationToken cToken)
        {
            while(!cToken.IsCancellationRequested)
            {
                Debug.WriteLine($"{DateTime.Now.ToString(CultureInfo.InvariantCulture)} Mail Gönderiliyor...");
            }
        }
    }
}
