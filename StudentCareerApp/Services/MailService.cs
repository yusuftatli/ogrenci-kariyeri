using SCA.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StudentCareerApp
{
    public class MailService : HostedService
    {
        private readonly ISender _sender;
        public MailService(ISender sender)
        {
            _sender = sender;
        }

        protected override async Task ExecuteAsync(CancellationToken cToken)
        {
            while (!cToken.IsCancellationRequested)
            {
                Debug.WriteLine($"{DateTime.Now.ToString(CultureInfo.InvariantCulture)} Mail Gönderiliyor...");
                await _sender.SendEmail();
                await Task.Delay(TimeSpan.FromMinutes(10), cToken);
            }
        }
    }
}
