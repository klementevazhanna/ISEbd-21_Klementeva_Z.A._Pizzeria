using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PizzeriaShopContracts.BindingModels
{
    [DataContract]
    public class WareHouseBindingModel
    {
        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        public string WareHouseName { get; set; }

        [DataMember]
        public string ResponsiblePersonFIO { get; set; }

        [DataMember]
        public DateTime DateCreate { get; set; }

        [DataMember]
        public Dictionary<int, (string, int)> WareHouseIngredients { get; set; }
    }
}
