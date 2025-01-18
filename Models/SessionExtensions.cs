using System.Text.Json;

namespace Cartel_Search_Products.Models
{
    /* Manages the session of a user */
    public static class SessionExtensions
    {
        // The Set<T> method stores an object in the session using a specified key.
        // The object is serialised into JSON format before being stored.
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value)); // Serialises the object and stores it as a string.
        }
        // The Get<T> method retrieves an object from the session using a specified key.
        // If the key does not exist, it returns the default value for the type.
        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key); // Retrieves the stored string value from the session.
            return value == null ? default : JsonSerializer.Deserialize<T>(value); // Deserialises the string back to the original object or returns default if null.
        }
    }
}
