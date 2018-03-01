using HashidsNet;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace HashIdSample.Helpers
{
    public static class HashIdsUtil
    {
        public static IConfiguration Configuration { get; set; }

        private static string GetSalt()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            Configuration = builder.Build();

            var salt = Configuration.GetValue<string>("HashIdSampleSalt");

            if (salt == null)
                throw new Exception("HashIdSampleSalt não foi definido no appsettings");

            return salt;
        }

        public static string Encode(object id)
        {           
            long.TryParse(id.ToString(), out var currentId);

            return new Hashids(GetSalt()).EncodeLong(currentId);
        }

        public static string Encode(int id)
        {
            return new Hashids(GetSalt()).EncodeLong(id);
        }

        public static string Encode(long id)
        {
            return Encode(Convert.ToInt32(id));
        }

        public static long Decode(object key)
        {
            var hashids = new Hashids(GetSalt());

            var decoded = hashids.DecodeLong(key as string);

            if (decoded == null || decoded.Length == 0)
                return 0;

            return decoded[0];
        }
    }
}
