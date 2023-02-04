using MongoDB.Bson;

namespace MvcWeb.Models
{
    public class Product
    {
        public ObjectId Id { get; set; }
        public string Name { get; set; }
    }
}
