using PizzeriaShopContracts.Attributes;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PizzeriaShopContracts.ViewModels
{
    [DataContract]
    public class WareHouseViewModel
    {
        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        [Column(title: "Название склада", width: 100)]
        public string WareHouseName { get; set; }

        [DataMember]
        [Column(title: "ФИО ответственного", width: 50)]
        public string ResponsiblePersonFIO { get; set; }

        [DataMember]
        [Column(title: "Дата создания", width: 100, dateType: "d M y")]
        public DateTime DateCreate { get; set; }

        [DataMember]
        public Dictionary<int, (string, int)> WareHouseIngredients { get; set; }
    }
}
