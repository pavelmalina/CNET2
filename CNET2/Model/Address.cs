namespace Model
{
    public class Address
    {
        public int Id { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public override string ToString() => $"{City} {Street}";

        // Tady by měl být spíše List, aby EF vyhodnotil, že 1 adresa může být u N společností
        public Company? Company { get; set; }
    }
}