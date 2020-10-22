using GeoJSON.Net.Geometry;
using Imdf.Core.Categories;
using Imdf.Core.FeatureTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace imdf.core.tests
{
    public class VenueTest
    {
        private Polygon testpolygon;
        private string id;

        [SetUp]
        public void Setup()
        {
            var coordinates = new List<IPosition>
            {
                new Position(52.370725881211314, 4.889259338378906),
                new Position(52.3711451105601, 4.895267486572266),
                new Position(52.36931095278263, 4.892091751098633),
                new Position(52.370725881211314, 4.889259338378906)
            };

            testpolygon = new Polygon(new List<LineString> { new LineString(coordinates) });
            id = "4343";
        }

        [Test]
        public void ParseVenueTest()
        {
            var venueJson = File.ReadAllText("testdata/venue.json");
            var venue = JsonConvert.DeserializeObject<Venue>(venueJson);
            Assert.IsTrue((string)venue.Properties["category"] == "shoppingcenter");
            Assert.IsTrue(venue.Phone == "+12225551212");
        }

        [Test]
        public void VenueMustHaveObligatoryFields()
        {
            // act
            var venue = new Venue(testpolygon, id);

            // assert
            Assert.IsTrue(venue.Id == id);
            Assert.IsTrue(venue.FeatureType == "venue");
            Assert.IsTrue(venue.Geometry.Type == GeoJSON.Net.GeoJSONObjectType.Polygon);
        }

        [Test]
        public void TestVenueAttributes()
        {
            var venue = new Venue(testpolygon, id);
            venue.VenueCategory = VenueCategory.airport;
            venue.RestrictionCategory = RestrictionCategory.employeesonly;
            venue.DisplayPoint = new Point(new Position(1, 2));
            venue.Phone = "+4343";
            venue.Website = "http://www.test.nl";
            venue.Hours = "Mo-Fr 08:30-20:00";
            venue.AddressId = "22222222-2222-2222-2222-222222222222";

            var nameObject = JObject.FromObject(new{
                en = "testvalue"
            });
            venue.Name = nameObject;
            venue.AltName = nameObject;

            var actualJson = JsonConvert.SerializeObject(venue);

            Assert.IsTrue(venue.VenueCategory == VenueCategory.airport);
            Assert.IsTrue(venue.RestrictionCategory == RestrictionCategory.employeesonly);
            Assert.IsTrue(venue.DisplayPoint.Equals(new Point(new Position(1, 2))));
            Assert.IsTrue(venue.Phone.Equals("+4343"));
            Assert.IsTrue(venue.Website.Equals("http://www.test.nl"));
            Assert.IsTrue(venue.Hours.Equals("Mo-Fr 08:30-20:00"));
            Assert.IsTrue(venue.AddressId.Equals("22222222-2222-2222-2222-222222222222"));
            Assert.IsTrue(venue.Name.Equals(nameObject));
            Assert.IsTrue(venue.AltName.Equals(nameObject));
        }
    }
}