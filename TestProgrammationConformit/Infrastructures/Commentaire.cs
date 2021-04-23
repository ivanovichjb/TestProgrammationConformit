using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestProgrammationConformit.Infrastructures
{
    [Table("tbl_commentaire", Schema = "public")]
    public partial class Commentaire
    {
        [Key]
        public int CommentaireId { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public int EvenementId { get; set; }
        public Evenement Evenement { get; set; }
    }
}
