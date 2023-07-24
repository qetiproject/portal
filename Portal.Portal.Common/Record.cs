namespace Portal.Portal.Common
{
    public abstract record Record
    {
        public int Id { get; set; }

        public DateTime TimeStamp { get; set; }
    }
}
