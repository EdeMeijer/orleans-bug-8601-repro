using System.Threading.Tasks;
using Orleans;

namespace Repro
{
    public interface IFoo : IGrainWithGuidKey
    {
        Task<int> Act<T>(int a);

        Task<int> Act<T>(int a, string b);
    }
}
