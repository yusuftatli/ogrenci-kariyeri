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
    public class SyncServices : HostedService
    {
        private readonly ISender _sender;
        private readonly ISyncManager _syncManager;
        public SyncServices(ISender sender, ISyncManager syncManager)
        {
            _sender = sender;
            _syncManager = syncManager;
        }

        protected override async Task ExecuteAsync(CancellationToken cToken)
        {
            while (!cToken.IsCancellationRequested)
            {
              //  await _syncManager.SyncAssay();
                //await Task.Delay(TimeSpan.FromDays(1), cToken);
            }
        }
    }
}
