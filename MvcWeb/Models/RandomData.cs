﻿using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using Bogus.DataSets;
using Bogus.Extensions;
using MvcWeb.Models;

namespace MvcWeb.Models
{
    public class RandomData
    {
        public static List<string> Activities = new List<string>()
        {
            "Baking","Blogging", "Bowling","Collecting", "Hacking", "Backpacking",
            "Canyoning", "Geocaching", "Orienteering", "Golf","Climbing", "Running", "Snow-kiting",
            "Horseback riding", "Photography", "Scuba diving","Wildlife watching",
            "Wine tourism", "Road Touring", "Reading"
        };

        public static List<string> AvailableSports = new List<string>()
        {
            "Soccer","Basketball", "Tennis","Volleyball", "Beach Volleyball", "American Football",
            "Baseball", "Ice Hockey", "Formula 1", "Moto GP","Motor Sport", "Handball", "Water Polo",
            "Table Tennis", "Darts","Snooker", "MMA", "Boxing","Cricket", "Cycling", "Golf"
        };

        public static List<string> AvailableProfessions = new List<string>()
        {
            "Dentist","Photographer","Pharmacist","Teacher","Flight Attendant","Founder / Entrepreneur",
            "Personal Trainer","Waitress / Bartender","Physical Therapist","Lawyer","Marketing Manager","Pilot",
            "Producer","Visual Designer","Model","Engineer", "Firefighter","Doctor","Financial Adviser", "Police Officer",
            "Social-Media Manager","Nurse","Real-Estate Agent"
        };
        public static List<User> GenerateUsers(int count, string locale = "en")
        {
            var person = new Faker<User>(locale)
                .RuleFor(u => u.Gender, f => f.PickRandom<Gender>())
                .RuleFor(u => u.FirstName, (f, u) =>
                    f.Name.FirstName(u.Gender.MapToLibGender()))
                .RuleFor(u => u.LastName, (f, u) => f.Name.LastName(u.Gender.MapToLibGender()))
                .RuleFor(u => u.UserName, (f, u) => f.Internet.UserName(u.FirstName, u.LastName))
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.FirstName, u.LastName))
                .RuleFor(u => u.Avatar, f => f.Internet.Avatar())
                .RuleFor(u => u.DateOfBirth, (f, u) => f.Date.Past(50, new DateTime?(Date.SystemClock().AddYears(-20))))
                .RuleFor(u => u.Phone, f => f.Phone.PhoneNumber())
                .RuleFor(u => u.Website, f => f.Internet.DomainName().OrNull(f, 0.1f))
                .RuleFor(u => u.Address, f => GenerateCardAddress(locale))
                .RuleFor(u => u.Company, (f, u) => new CompanyCard()
                {
                    Name = f.Company.CompanyName(new int?()),
                    CatchPhrase = f.Company.CatchPhrase(),
                    Bs = f.Company.Bs()
                })
                .RuleFor(u => u.Salary, (f, u) => Math.Round(f.Finance.Amount(1000, 5000)))
                .RuleFor(u => u.MonthlyExpenses, (f, u) => f.Random.Number(1500, 6500))
                .RuleFor(u => u.FavoriteSports,
                    (f, u) => f.PickRandom(AvailableSports, f.Random.Number(1, 19)).ToList())
                .RuleFor(u => u.Profession, (f, u) => f.PickRandom(AvailableProfessions, 1).FirstOrDefault());

            return person.Generate(count);
        }

        public static List<Order> GenerateOrders(int count, string locale = "en")
        {
            var orderIds = 0;
            var order = new Faker<Order>(locale)
                .StrictMode(true)
                .RuleFor(o => o.OrderId, f => orderIds++)
                .RuleFor(o => o.Item, f => f.Commerce.ProductName())
                .RuleFor(o => o.Price, f => f.Random.Int(1, 300))
                .RuleFor(o => o.Quantity, f => f.Random.Number(1, 10))
                .RuleFor(o => o.LotNumber, f => f.Random.Int(0, 100).OrNull(f, .8f))
                .RuleFor(o => o.ShipmentDetails, f => GenerateShipmentDetails(locale));

            return order.Generate(count);

        }

        public static List<Product> GenerateProducts(int count, string locale = "en")
        {
            var product = new Faker<Product>(locale)
                .RuleFor(p => p.Name, f => f.Commerce.ProductName());

            return product.Generate(count);

        }

        public static ShipmentDetails GenerateShipmentDetails(string locale = "en")
        {
            var shipmentDetails = new Faker<ShipmentDetails>(locale)
                .RuleFor(u => u.ContactName, (f, u) => f.Name.FullName())
                .RuleFor(u => u.ContactPhone, (f, u) => f.Phone.PhoneNumber().OrNull(f, .3f))
                .RuleFor(u => u.City, (f, u) => f.Address.City())
                .RuleFor(u => u.ShipAddress, (f, u) => f.Address.FullAddress())
                .RuleFor(u => u.Country, f => f.Address.Country())
                .RuleFor(u => u.ShippedDate, (f, u) =>
                    f.Date.Between(DateTime.UtcNow.AddYears(-1), DateTime.UtcNow).OrNull(f, .8f));

            return shipmentDetails.Generate();
        }

        public static AddressCard GenerateCardAddress(string locale = "en")
        {
            var cardAddress = new Faker<AddressCard>(locale)
                .RuleFor(a => a.Street, (f, u) => f.Address.StreetAddress())
                .RuleFor(a => a.Suite, (f, u) => f.Address.SecondaryAddress())
                .RuleFor(a => a.City, (f, u) => f.Address.City())
                .RuleFor(a => a.State, (f, u) => f.Address.State())
                .RuleFor(a => a.ZipCode, (f, u) => f.Address.ZipCode())
                .RuleFor(a => a.Geo, (f, u) => new AddressCard.AppCardGeo()
                {
                    Lat = f.Address.Latitude(-90.0, 90.0),
                    Lng = f.Address.Longitude(-180.0, 180.0)
                });

            return cardAddress.Generate();
        }

        public static List<Traveler> GenerateTravelers(int count, int maximumActivities = 20, string locale = "en")
        {
            if (maximumActivities > Activities.Count)
            {
                maximumActivities = Activities.Count;
            }

            var traveler = new Faker<Traveler>(locale)
                .RuleFor(t => t.Name, (f, u) => f.Name.FullName())
                .RuleFor(t => t.Age, (f) => (DateTime.Now.Year -
                                             f.Date.Past(50, new DateTime?(Date.SystemClock().AddYears(-20))).Year))
                .RuleFor(t => t.Activities, (f, u) => f.PickRandom(Activities, f.Random.Number(1, maximumActivities)).ToList())
                .RuleFor(u => u.VisitedCountries, (f, u) => GenerateVisitedCountries(f.Random.Int(0, 30)));

            return traveler.Generate(count);
        }

        public static List<VisitedCountry> GenerateVisitedCountries(int count, string locale = "en")
        {
            var visitedCountry = new Faker<VisitedCountry>(locale)
                .RuleFor(u => u.Name, f => f.Address.Country())
                .RuleFor(u => u.TimesVisited, f => f.Random.Number(1, 10))
                .RuleFor(c => c.Coordinates, f => GenerateGeolocations(1).First())
                .RuleFor(u => u.LastDateVisited, (f, u) =>
                    f.Date.Between(DateTime.UtcNow.AddYears(-5), DateTime.UtcNow));

            return visitedCountry.Generate(count);
        }

        public static List<GeoLocation> GenerateGeolocations(int count, string locale = "en")
        {
            var geolocation = new Faker<GeoLocation>(locale)
                .RuleFor(c => c.Latitude, f => f.Address.Latitude())
                .RuleFor(c => c.Longitude, f => f.Address.Longitude());

            return geolocation.Generate(count);
        }

        public static List<SocialAccount> GenerateSocialAccounts(int count, string locale = "en")
        {
            var account = new Faker<SocialAccount>(locale)
                .RuleFor(t => t.Username, (f, u) => f.Internet.UserName())
                .RuleFor(u => u.RelationShips, (f, u) => GenerateRelationships(1).First())
                .RuleFor(a => a.LastNotifications, (f,a) => GenerateNotifications(f.Random.Number(1,2)));

            return account.Generate(count);
        }

        public static List<RelationShips> GenerateRelationships(int count, string locale = "en")
        {
            var relationShip = new Faker<RelationShips>(locale)
                .RuleFor(c => c.Friends, f => Enumerable.Range(1, 5)
                    .Select(_ => f.Internet.UserName())
                    .ToList())
                .RuleFor(c => c.Blocked, f => Enumerable.Range(1, 2)
                    .Select(_ => f.Internet.UserName())
                    .ToList());

            return relationShip.Generate(count);
        }

        public static List<Notification> GenerateNotifications(int count, string locale = "en")
        {
            var notification = new Faker<Notification>(locale)
                .RuleFor(n => n.Text, f => f.Lorem.Sentence(5))
                .RuleFor(n => n.Link, (f, n) => f.Internet.UrlRootedPath());

            return notification.Generate(count);
        }
    }
}
