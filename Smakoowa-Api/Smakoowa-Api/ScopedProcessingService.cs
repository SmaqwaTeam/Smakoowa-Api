//using System.Threading.Channels;

//namespace Smakoowa_Api
//{
//    internal interface IScopedProcessingService
//    {
//        Task DoWork(CancellationToken stoppingToken);
//    }

//    internal class ScopedProcessingService : IScopedProcessingService
//    {
//        private int executionCount = 0;
//        private readonly ILogger _logger;

//        public ScopedProcessingService(ILogger<ScopedProcessingService> logger)
//        {
//            _logger = logger;
//        }

//        public async Task DoWork(CancellationToken stoppingToken)
//        {
//            while (!stoppingToken.IsCancellationRequested)
//            {
//                executionCount++;

//                _logger.LogInformation(
//                    "Scoped Processing Service is working. Count: {Count}", executionCount);

//                await Task.Delay(10000, stoppingToken);
//            }
//        }
//    }

//    public class ConsumeScopedServiceHostedService : BackgroundService
//    {
//        private readonly ILogger<ConsumeScopedServiceHostedService> _logger;

//        public ConsumeScopedServiceHostedService(IServiceProvider services,
//            ILogger<ConsumeScopedServiceHostedService> logger)
//        {
//            Services = services;
//            _logger = logger;
//        }

//        public IServiceProvider Services { get; }

//        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            _logger.LogInformation(
//                "Consume Scoped Service Hosted Service running.");

//            await DoWork(stoppingToken);
//        }

//        private async Task DoWork(CancellationToken stoppingToken)
//        {
//            _logger.LogInformation(
//                "Consume Scoped Service Hosted Service is working.");

//            using (var scope = Services.CreateScope())
//            {
//                var scopedProcessingService =
//                    scope.ServiceProvider
//                        .GetRequiredService<IScopedProcessingService>();

//                await scopedProcessingService.DoWork(stoppingToken);
//            }
//        }

//        public override async Task StopAsync(CancellationToken stoppingToken)
//        {
//            _logger.LogInformation(
//                "Consume Scoped Service Hosted Service is stopping.");

//            await base.StopAsync(stoppingToken);
//        }
//    }

//    public interface IBackgroundTaskQueue
//    {
//        ValueTask QueueBackgroundWorkItemAsync(Func<CancellationToken, ValueTask> workItem);

//        ValueTask<Func<CancellationToken, ValueTask>> DequeueAsync(
//            CancellationToken cancellationToken);
//    }

//    public class BackgroundTaskQueue : IBackgroundTaskQueue
//    {
//        private readonly Channel<Func<CancellationToken, ValueTask>> _queue;

//        public BackgroundTaskQueue(int capacity)
//        {
//            // Capacity should be set based on the expected application load and
//            // number of concurrent threads accessing the queue.            
//            // BoundedChannelFullMode.Wait will cause calls to WriteAsync() to return a task,
//            // which completes only when space became available. This leads to backpressure,
//            // in case too many publishers/calls start accumulating.
//            var options = new BoundedChannelOptions(capacity)
//            {
//                FullMode = BoundedChannelFullMode.Wait
//            };
//            _queue = Channel.CreateBounded<Func<CancellationToken, ValueTask>>(options);
//        }

//        public async ValueTask QueueBackgroundWorkItemAsync(
//            Func<CancellationToken, ValueTask> workItem)
//        {
//            if (workItem == null)
//            {
//                throw new ArgumentNullException(nameof(workItem));
//            }

//            await _queue.Writer.WriteAsync(workItem);
//        }

//        public async ValueTask<Func<CancellationToken, ValueTask>> DequeueAsync(
//            CancellationToken cancellationToken)
//        {
//            var workItem = await _queue.Reader.ReadAsync(cancellationToken);

//            return workItem;
//        }
//    }

//    public class QueuedHostedService : BackgroundService
//    {
//        private readonly ILogger<QueuedHostedService> _logger;

//        public QueuedHostedService(IBackgroundTaskQueue taskQueue,
//            ILogger<QueuedHostedService> logger)
//        {
//            TaskQueue = taskQueue;
//            _logger = logger;
//        }

//        public IBackgroundTaskQueue TaskQueue { get; }

//        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            _logger.LogInformation(
//                $"Queued Hosted Service is running.{Environment.NewLine}" +
//                $"{Environment.NewLine}Tap W to add a work item to the " +
//                $"background queue.{Environment.NewLine}");

//            await BackgroundProcessing(stoppingToken);
//        }

//        private async Task BackgroundProcessing(CancellationToken stoppingToken)
//        {
//            while (!stoppingToken.IsCancellationRequested)
//            {
//                var workItem =
//                    await TaskQueue.DequeueAsync(stoppingToken);

//                try
//                {
//                    await workItem(stoppingToken);
//                }
//                catch (Exception ex)
//                {
//                    _logger.LogError(ex,
//                        "Error occurred executing {WorkItem}.", nameof(workItem));
//                }
//            }
//        }

//        public override async Task StopAsync(CancellationToken stoppingToken)
//        {
//            _logger.LogInformation("Queued Hosted Service is stopping.");

//            await base.StopAsync(stoppingToken);
//        }
//    }

//    public class TimedHostedService : BackgroundService
//    {
//        private readonly ILogger<TimedHostedService> _logger;
//        private int _executionCount;

//        public TimedHostedService(ILogger<TimedHostedService> logger)
//        {
//            _logger = logger;
//        }

//        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            _logger.LogInformation("Timed Hosted Service running.");

//            // When the timer should have no due-time, then do the work once now.
//            DoWork();

//            using PeriodicTimer timer = new(TimeSpan.FromSeconds(1));

//            try
//            {
//                while (await timer.WaitForNextTickAsync(stoppingToken))
//                {
//                    DoWork();
//                }
//            }
//            catch (OperationCanceledException)
//            {
//                _logger.LogInformation("Timed Hosted Service is stopping.");
//            }
//        }

//        // Could also be a async method, that can be awaited in ExecuteAsync above
//        private void DoWork()
//        {
//            int count = Interlocked.Increment(ref _executionCount);

//            _logger.LogInformation("Timed Hosted Service is working. Count: {Count}", count);
//        }
//    }
//}
