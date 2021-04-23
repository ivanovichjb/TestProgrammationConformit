using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestProgrammationConformit.Infrastructures;

namespace TestProgrammationConformit.Dtos
{
    public class EvenementDto
    {

        public int EvenementId { get; set; }

        public string Titre { get; set; }

        public string Personne { get; set; }

        public Commentaire Commentaire { get; set; }

    }
}
