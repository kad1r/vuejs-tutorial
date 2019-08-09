using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.Model
{
    public partial class Posts
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedDate { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UpdatedDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int UserId { get; set; }

        [ForeignKey("CategoryId")]
        [InverseProperty("Posts")]
        public virtual Categories Category { get; set; }
        [ForeignKey("UserId")]
        [InverseProperty("Posts")]
        public virtual Users User { get; set; }
    }
}