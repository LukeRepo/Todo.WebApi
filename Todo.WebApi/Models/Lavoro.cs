using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Todo.WebApi.Models
{
    [Table("Tasks")]
    public class Lavoro
    {
        [Key]
        [Column("NomeTask")]
        public string NomeTask { get; set; }

        [Required]
        [Column("Inserito")]
        public bool Inserito { get; set; }

        [Required]
        [Column("Fatto")]
        public bool Fatto { get; set; }

        [Required]
        [Column("Scaduto")]
        public bool Scaduto { get; set; }

        [Column("Descrizione")]
        public String Descrizione { get; set; }

        [Column("DataScadenza")]
        public DateTime? DataScadenza { get; set; }

        public string NomeBacheca { get; set; }

        public Bacheca Bacheca { get; set; }

        public List<Bacheca> ListaBacheca { get; set; }
    }
}