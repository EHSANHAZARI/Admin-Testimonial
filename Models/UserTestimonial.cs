using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TS.Models
{
    [Table("User Testimonial")]
    public class UserTestimonial
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("User Profile ID")]
        public int UserProfileId { get; set; }

        [Column("Testimonial Content")]
        [Required]
        public string Description { get; set; }     //This is equal description 

        [Column("Testimonial Title")]
        [Required]
        public string Title { get; set; }    // This is equal to testimonial

        [Column("IsApprove")]
        public bool IsApprove { get; set; }

        public virtual ApplicantProfile ApplicantProfile { get; set; }
    }
}
