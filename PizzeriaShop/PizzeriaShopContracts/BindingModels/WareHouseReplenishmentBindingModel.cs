using System.Runtime.Serialization;

namespace PizzeriaShopContracts.BindingModels
{
    [DataContract]
    public class WareHouseReplenishmentBindingModel
    {
        [DataMember]
        public int WareHouseId { get; set; }

        [DataMember]
        public int IngredientId { get; set; }

        [DataMember]
        public int Count { get; set; }
    }
}
