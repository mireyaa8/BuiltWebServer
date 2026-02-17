namespace BuiltWebServer.Server.Common
{
    public static class Guard
    {
        public static void AgainstNull(object value, string name = null)
        {
            if (value == null)
                throw new ArgumentNullException(name ?? "Value cannot be null");
        }
    }
}
