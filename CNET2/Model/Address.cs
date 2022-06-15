namespace Model
{
    public class Address
    {
        public string City { get; set; }

        public string Street { get; set; }

        public override string ToString() => $"{City} {Street}";
    }
}