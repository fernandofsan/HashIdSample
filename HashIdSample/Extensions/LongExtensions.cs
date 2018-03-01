using HashIdSample.Helpers;

namespace HashIdSample.Extensions
{
    public static class LongExtensions
    {
        public static string HashId(this long id)
        {
            return HashIdsUtil.Encode(id);
        }
    }
}
