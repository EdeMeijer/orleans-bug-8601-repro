using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;

namespace Repro
{
    public class Program
    {
        public static int Main(string[] args)
        {
            return RunMainAsync().Result;
        }

        private static async Task<int> RunMainAsync()
        {
            try
            {
                var host = await StartSilo();
                
                var grainFactory = host.Services.GetService<IGrainFactory>();
                var sut = grainFactory.GetGrain<IFoo>(Guid.NewGuid());
                
                var res = await sut.Act<bool>(42);
                Console.WriteLine($"Expected: 1, actual: {res}");
                
                res = await sut.Act<bool>(42, "a");
                Console.WriteLine($"Expected: 2, actual: {res}");

                await host.StopAsync();
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return 1;
            }
        }

        private static async Task<ISiloHost> StartSilo()
        {
            // define the cluster configuration
            var builder = new SiloHostBuilder()
                .UseLocalhostClustering()
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "dev";
                    options.ServiceId = "OrleansBasics";
                });

            var host = builder.Build();
            await host.StartAsync();
            return host;
        }
    }
}
