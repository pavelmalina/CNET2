namespace Model
{
    public class Address
    {
        public int Id { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public override string ToString() => $"{City} {Street}";
    }
}