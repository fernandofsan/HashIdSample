using HashidsNet;

namespace HashIdSample.Helpers
{
    public static class HashIdsUtil
    {
        public static string Encode(object id)
        {
            long.TryParse(id.ToString(), out long currentId);

            return new Hashids("meu salt", 5).EncodeLong(currentId);
        }

        public static long Decode(object key)
        {
            var hashids = new Hashids("meu salt", 5);

            var decoded = hashids.DecodeLong(key as string);

            if (decoded == null || decoded.Length == 0)
                return 0;

            return decoded[0];
        }
    }
}
