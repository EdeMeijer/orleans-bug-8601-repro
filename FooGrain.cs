using System.Threading.Tasks;
using Orleans;

namespace Repro
{
    public class FooGrain : Grain, IFoo
    {
        public Task<int> Act<T>(int a) => Task.FromResult(1);

        public Task<int> Act<T>(int a, string b) => Task.FromResult(2);
    }
}
