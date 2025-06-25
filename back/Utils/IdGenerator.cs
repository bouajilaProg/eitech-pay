using System;

namespace Back.Utils
{
    public static class IdGenerator
    {
        public static string GenerateId(string prefix)
        {
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var random = new Random().Next(1000, 9999);
            return $"{prefix}_{timestamp}_{random}";
        }
    }
}
