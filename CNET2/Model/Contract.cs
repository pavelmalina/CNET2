namespace Model
{
    public class Contract
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Number { get; set; } = string.Empty;

        public DateTime Signed { get; set; }

        public bool IsActive { get; set; }

        public Company? Company { get; set; } = null;
    }
}