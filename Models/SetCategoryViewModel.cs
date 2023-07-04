using MessagePack;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JobPortal.Models.Domain
{
    public class SetCategoryViewModel
    {
        [System.ComponentModel.DataAnnotations.Key]
        public Guid CategoryId { get; set; }

        public string CategoryName { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
