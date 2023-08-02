using System.Threading.Tasks;
using OsuMemoryDataProvider;
using OsuMemoryDataProvider.OsuMemoryModels;

namespace SkinCommand
{
    internal static class Program
    {
        private static StructuredOsuMemoryReader _sreader;
        public static readonly OsuBaseAddresses BaseAddresses = new OsuBaseAddresses();
        private static readonly int _readDelay = 100;
        private static readonly Bot twitch = new Bot();
        public static async Task Main()
        {
            _sreader = StructuredOsuMemoryReader.Instance;
            _sreader.WithTimes = true;
            twitch._client.Connect();
            while (true) await getOsuData();
        }

        private static async Task getOsuData()
        {
            if (_sreader.CanRead)
                _sreader.TryRead(BaseAddresses.Skin);
            await Task.Delay(_readDelay);
        }
    }
}