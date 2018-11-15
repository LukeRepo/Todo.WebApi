using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Todo.WebApi.Models
{
    [Table("Bacheche")]
    public class Bacheca
    {
        [Key]
        [Column("NomeBacheca")]
        public string NomeBacheca { get; set; }

        public string NomeTask { get; set; }

        public Lavoro Task { get; set; }

        public List<Lavoro> listaTasks { get; set; }
    }
}