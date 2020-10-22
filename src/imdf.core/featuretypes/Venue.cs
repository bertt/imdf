using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Imdf.Core.Categories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Imdf.Core.FeatureTypes
{
    // specs see https://register.apple.com/resources/imdf/Venue/
    public class Venue : Feature
    {
        public Venue(Polygon geometry, string id, IDictionary<string, object>? properties = null) : base(geometry, properties, id)
        {
            FeatureType = "venue";
        }

        [JsonProperty("feature_type")]
        public string FeatureType { get; }

        public VenueCategory VenueCategory
        {
            get
            {
                return (VenueCategory)Enum.Parse(typeof(VenueCategory), Properties["category"].ToString());
            }
            set
            {
                Properties["category"] = value;
            }
        }

        public RestrictionCategory? RestrictionCategory
        {
            get
            {
                if (Properties["restriction"] != null)
                {
                    return (RestrictionCategory)Enum.Parse(typeof(RestrictionCategory), Properties["restriction"].ToString());
                }
                return null;
            }
            set
            {
                Properties["restriction"] = value;
            }
        }

        public Point DisplayPoint
        {
            get
            {
                var s = Properties["display_point"];
                return JsonConvert.DeserializeObject<Point>(s.ToString());
            }
            set
            {
                Properties["display_point"] = value;
            }
        }

        public JObject Name
        {
            get
            {
                return (JObject)Properties["name"];
            }
            set
            {
                Properties["name"] = value;
            }

        }

        public JObject AltName
        {
            get
            {
                return (JObject)Properties["alt_name"];
            }
            set
            {
                Properties["alt_name"] = value;
            }
        }

        public string AddressId
        {
            get
            {
                return (string)Properties["address_id"];
            }
            set
            {
                Properties["address_id"] = value;
            }
        }

        public string Hours
        {
            get
            {
                return (string)Properties["hours"];
            }
            set
            {
                Properties["hours"] = value;
            }
        }

        public string Phone
        {
            get
            {
                return (string)Properties["phone"];
            }
            set
            {
                Properties["phone"] = value;
            }
        }

        public string Website
        {
            get
            {
                return (string)Properties["website"];
            }
            set
            {
                Properties["website"] = value;
            }
        }

    }
}
