namespace Communication
{
    public class Place : PlaceBase
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double? Rating { get; set; }
        public object Image { get; set; }
    }
}