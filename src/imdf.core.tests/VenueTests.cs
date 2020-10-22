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
                new Position(0.0, 100.0),
                new Position(0.0,101.0),
                new Position(1.0, 101.0),
                new Position(1.0,100.0),
                new Position(0.0, 100.0),
            };

            testpolygon = new Polygon(new List<LineString> { new LineString(coordinates) });
            id = "11111111-1111-1111-1111-111111111111";
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
            venue.VenueCategory = VenueCategory.shoppingcenter;
            venue.RestrictionCategory = null;
            venue.DisplayPoint = new Point(new Position(1.0, 100.0));
            venue.Phone = "+12225551212";
            venue.Website = "http://example.com";
            venue.Hours = "Mo-Fr 08:30-20:00";
            venue.AddressId = "22222222-2222-2222-2222-222222222222";

            var nameObject = JObject.FromObject(new{
                en = "Test Venue"
            });
            venue.Name = nameObject;
            venue.AltName = null;

            var actualJson = JsonConvert.SerializeObject(venue);
            var actualVenue = JsonConvert.DeserializeObject<Venue>(actualJson);
            var expectedJson = File.ReadAllText("testdata/venue.json");
            var expectedVenue = JsonConvert.DeserializeObject<Venue>(expectedJson);
            Assert.AreEqual(expectedVenue, actualVenue);
            Assert.IsTrue(actualVenue.Geometry != null);
            Assert.IsTrue(((Polygon)actualVenue.Geometry).Coordinates[0].Coordinates[0].Longitude == 100.0);
            Assert.IsTrue(actualVenue.VenueCategory == VenueCategory.shoppingcenter);
            Assert.IsTrue(actualVenue.RestrictionCategory == null);
            Assert.IsTrue(actualVenue.Phone.Equals("+12225551212"));
            Assert.IsTrue(actualVenue.Website.Equals("http://example.com"));
            Assert.IsTrue(actualVenue.Hours.Equals("Mo-Fr 08:30-20:00"));
            Assert.IsTrue(actualVenue.AddressId.Equals("22222222-2222-2222-2222-222222222222"));
            Assert.IsTrue(actualVenue.Name.ToString() == nameObject.ToString());
            Assert.IsTrue(actualVenue.AltName == null);
            Assert.IsTrue(actualVenue.DisplayPoint.Equals(new Point(new Position(1.0, 100.0))));
        }
    }
}