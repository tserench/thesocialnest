namespace WebApplication4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PostComment")]
    public partial class PostComment
    {
        public int Id { get; set; }
        

        public int PostId { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Comment { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual Post Post { get; set; }
    }
}
