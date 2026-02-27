namespace Stock.Entity
{
    public class ProductStock
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime LastUpdated { get; set; }
        /// <summary>
        /// Optimistic concurrency token (PostgreSQL xmin).
        /// </summary>
        public uint Version { get; set; }
    }
}
