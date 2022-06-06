using PizzeriaShopContracts.Attributes;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace PizzeriaShopContracts.ViewModels
{
    [DataContract]
    public class IngredientViewModel
    {
        [DataMember]
        [Column(title: "Номер", width: 100)]
        public int Id { get; set; }

        [DataMember]
        [Column(title: "Ингредиент", gridViewAutoSize: GridViewAutoSize.Fill)]
        [DisplayName("Название ингредиента")]
        public string IngredientName { get; set; }
    }
}
