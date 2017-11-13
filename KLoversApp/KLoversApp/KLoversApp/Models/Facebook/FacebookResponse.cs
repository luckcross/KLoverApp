using Newtonsoft.Json;
using System.Collections.Generic;

namespace KLoversApp.Models.Facebook
{
    public class DataFacebookResp
    {
        [JsonProperty(PropertyName = "is_silhouette")]
        public bool IsSilhouette { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }

    public class PictureResp
    {
        [JsonProperty(PropertyName = "data")]
        public DataFacebookResp Data { get; set; }
    }

    public class CoverResp
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "offset_y")]
        public int OffsetY { get; set; }

        [JsonProperty(PropertyName = "source")]
        public string Source { get; set; }
    }

    public class AgeRangeResp
    {
        [JsonProperty(PropertyName = "min")]
        public int Min { get; set; }
    }

    public class DeviceResp
    {
        [JsonProperty(PropertyName = "hardware")]
        public string Hardware { get; set; }

        [JsonProperty(PropertyName = "os")]
        public string Os { get; set; }
    }

    public class FacebookResponse
    {
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "picture")]
        public PictureResp Picture { get; set; }

        [JsonProperty(PropertyName = "cover")]
        public CoverResp Cover { get; set; }

        [JsonProperty(PropertyName = "age_range")]
        public AgeRangeResp AgeRange { get; set; }

        [JsonProperty(PropertyName = "devices")]
        public List<DeviceResp> Devices { get; set; }

        [JsonProperty(PropertyName = "gender")]
        public string Gender { get; set; }

        [JsonProperty(PropertyName = "is_verified")]
        public bool IsVerified { get; set; }

        [JsonProperty(PropertyName = "locale")]
        public string Locale { get; set; }

        [JsonProperty(PropertyName = "link")]
        public string Link { get; set; }

        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
    }

}
