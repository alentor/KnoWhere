using System;

namespace KnoWhere.API.Data.Entities
{
    public class PlaceBucket
    {
        public Guid Id { get; set; }
        public string RestaurantBucketId { get; set; }
        public string EntertainmentBucketId { get; set; }
        public DateTime AddDate { get; set; }
    }
}