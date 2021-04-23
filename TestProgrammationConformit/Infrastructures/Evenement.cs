using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestProgrammationConformit.Infrastructures
{
    [Table("tbl_evenement", Schema = "public")]
    public partial class Evenement
    {
        [Key]
        public int EvenementId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Titre { get; set; }

        [MaxLength(100)]
        public string Personne { get; set; }

        public Commentaire Commentaire { get; set; }
    }
}
