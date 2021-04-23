using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProgrammationConformit.Infrastructures;

namespace TestProgrammationConformit.Dtos
{
    public class CommentaireDto
    {  
        public int CommentaireId { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public int EvenementId { get; set; }

        public Evenement Evenement { get; set; }

    }
}
