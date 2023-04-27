namespace Presentation.Areas.Admin.Models.EntrysViewModel
{
    public class EntriesViewModel
    {
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public bool? IsEntry { get; set; }
        public DateTime? EntryDate { get; set; }
        public bool? IsExit { get; set; }
        public DateTime? ExitDate { get; set; }

    }
}
