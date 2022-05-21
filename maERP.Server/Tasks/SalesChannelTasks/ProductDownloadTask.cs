#nullable disable

using maERP.Server.Contracts;

namespace maERP.Server.Tasks.SalesChannelTasks
{
    public class ProductDownloadTask : IHostedService
    {
        private readonly IServiceScopeFactory _service;

        public ProductDownloadTask(IServiceScopeFactory serviceScopeFactory)
        {
            _service = serviceScopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(async () =>
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    MainLoop();

                    await Task.Delay(new TimeSpan(0, 0, 5)); // 5 second delay
                }
            });
            
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        private async void MainLoop()
        {
            Console.WriteLine("ProductDownloadTask started!");

            using (var scope = _service.CreateScope())
            {
                var _repository = scope.ServiceProvider.GetService<ISalesChannelRepository>();

                var salesChannels = await _repository.GetAllAsync();

                foreach (var salesChannel in salesChannels)
                {
                    Console.WriteLine("Start ProductDownload for {salesChannel.Name} (ID: {salesChannel.Id})");
                }
            }
        }
    }
}