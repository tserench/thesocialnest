namespace WebApplication4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Post")]
    public partial class Post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Post()
        {
            PostComments = new HashSet<PostComment>();
            PostReacts = new HashSet<PostReact>();
        }

        public int PostId { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Content { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        public DateTime Time { get; set; }

        [Required]
        [StringLength(50)]
        public string Visibility { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostComment> PostComments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PostReact> PostReacts { get; set; }
    }
}
