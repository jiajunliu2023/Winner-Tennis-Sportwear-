
namespace A2.Dtos
{
    public class CheckOutInDtos
    {
        public int CustomerID { get; set; }
        public DateOnly Date { get; set; } = new DateOnly();
        public string DeliverMethod { get; set; }
    }
}
