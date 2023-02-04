﻿using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MvcWeb.Models
{
    public class Traveler
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public List<string> Activities { get; set; }
        public List<VisitedCountry> VisitedCountries { get; set; }
    }
}
