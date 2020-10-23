# imdf

.NET Standard 2.1 Library for Indoor Mapping Data Format https://register.apple.com/resources/imdf/

This library has a dependency on GeoJSON.NET for the feature types.

## Implemented: 

Feature types: Venue

Categories: VenueCategory, RestrictionGategory

## Sample code

Reading Venue file:

```
var venueJson = File.ReadAllText("testdata/venue.json");
var venue = JsonConvert.DeserializeObject<Venue>(venueJson);
```

Write Venue file:

```
var coordinates = new List<IPosition>
{
    new Position(0.0, 100.0),
    new Position(0.0,101.0),
    new Position(1.0, 101.0),
    new Position(1.0,100.0),
    new Position(0.0, 100.0),
};

var testpolygon = new Polygon(new List<LineString> { new LineString(coordinates) });
var id = "11111111-1111-1111-1111-111111111111";
var venue = new Venue(testpolygon, id);
venue.VenueCategory = VenueCategory.shoppingcenter;
venue.RestrictionCategory = RestrictionCategory.employeesonly;
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

var venueJson = JsonConvert.SerializeObject(venue);
File.WriteAllText("testdata/venue.json", venueJson);
```

## Todo: 

Manifest: https://register.apple.com/resources/imdf/Manifest/

Feature types: Address, Amenity, Anchor, Building, Detail, Ficture, Footprint, Geofence, Kiosk, Level, Occupant, Opening, Relationship, Section, Unit

Categories: Access control, Accessibility, Amenity, Building, Door, Fixture, Footprint, Geofence, Level, Occupant, Opening, Relationship, Section, Unit 

Validations: https://register.apple.com/resources/imdf/Validations/
