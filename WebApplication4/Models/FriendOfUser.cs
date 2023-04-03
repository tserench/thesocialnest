namespace WebApplication4.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("FriendOfUser")]
    public partial class FriendOfUser
    {
        public int Id { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        [Required]
        [StringLength(128)]
        public string FriendId { get; set; }

        public virtual AspNetUser AspNetUser { get; set; }

        public virtual AspNetUser AspNetUser1 { get; set; }
    }
}
