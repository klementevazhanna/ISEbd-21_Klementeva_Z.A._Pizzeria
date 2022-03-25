using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PizzeriaDatabaseImplement.Models
{
    public class WareHouse
    {
        public int Id { get; set; }

        [Required]
        public string WareHouseName { get; set; }

        [Required]
        public string ResponsiblePersonFIO { get; set; }

        [Required]
        public DateTime DateCreate { get; set; }

        [ForeignKey("WareHouseId")]
        public virtual List<WareHouseIngredient> WareHouseIngredients { get; set; }
    }
}
