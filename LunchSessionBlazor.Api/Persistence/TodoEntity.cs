namespace LunchSessionBlazor.Api.Persistence
{
    public class TodoEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreationDate { get; set; }
        public string Text { get; set; }
        public bool Finished { get; set; }
        public bool Archived { get; set; }
    }
}
