using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Imdf.Core.Categories
{
    // sepcs: https://register.apple.com/resources/imdf/Categories/#venue
    [JsonConverter(typeof(StringEnumConverter))]
    public enum VenueCategory
    {
        airport,
        airport_intl, // originally: airport.intl ...
        aquarium,
        businesscampus,
        casino,
        communitycenter,
        conventioncenter,
        governmentfacility,
        healthcarefacility,
        hotel,
        museum,
        parkingfacility,
        resort,
        retailstore,
        shoppingcenter,
        stadium,
        stripmall,
        theater,
        themepark,
        trainstation,
        transitstation,
        university
    }
}
