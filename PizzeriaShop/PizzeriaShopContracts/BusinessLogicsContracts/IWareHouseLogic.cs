using PizzeriaShopContracts.BindingModels;
using PizzeriaShopContracts.ViewModels;
using System.Collections.Generic;

namespace PizzeriaShopContracts.BusinessLogicsContracts
{
    public interface IWareHouseLogic
    {
        List<WareHouseViewModel> Read(WareHouseBindingModel model);

        void CreateOrUpdate(WareHouseBindingModel model);

        void Delete(WareHouseBindingModel model);

        void ReplenishByComponent(WareHouseReplenishmentBindingModel model);
    }
}
