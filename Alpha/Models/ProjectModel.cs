using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Alpha.Models
{
    public enum ProjectStatus
    {
        NotStarted,
        Started,
        Completed
    }
    public class ProjectModel
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string ProjectName { get; set; } = null!;
        [Required]
        public string ClientName { get; set; } = null!;
        public string? Description { get; set; }

        [Column(TypeName = "date")]
        public DateTime StartDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? EndDate { get; set; }
        public decimal? Budget { get; set; }
        public ProjectStatus Status { get; set; }

    }
}
