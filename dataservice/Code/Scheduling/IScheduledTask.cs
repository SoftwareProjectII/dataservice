using System.Threading;
using System.Threading.Tasks;

// Source: https://blog.maartenballiauw.be/post/2017/08/01/building-a-scheduled-cache-updater-in-aspnet-core-2.html
namespace dataservice.Code.Scheduling
{
    public interface IScheduledTask
    {
        string Schedule { get; }
    }
}
