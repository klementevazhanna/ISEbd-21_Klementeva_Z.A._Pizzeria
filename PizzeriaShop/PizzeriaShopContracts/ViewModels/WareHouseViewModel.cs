using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace PizzeriaShopContracts.ViewModels
{
    [DataContract]
    public class WareHouseViewModel
    {
        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        [DisplayName("Название склада")]
        public string WareHouseName { get; set; }

        [DataMember]
        [DisplayName("ФИО ответственного")]
        public string ResponsiblePersonFIO { get; set; }

        [DataMember]
        [DisplayName("Дата создания склада")]
        public DateTime DateCreate { get; set; }

        [DataMember]
        public Dictionary<int, (string, int)> WareHouseIngredients { get; set; }
    }
}
