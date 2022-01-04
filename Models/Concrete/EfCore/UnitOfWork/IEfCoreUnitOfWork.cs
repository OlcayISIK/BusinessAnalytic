using BusinessAnalytic.Models.Abstract;
using System;
using System.Threading.Tasks;

namespace BusinessAnalytic.Models.Concrete.EfCore.UnitOfWork
{
    public interface IEfCoreUnitOfWork : IDisposable
    {
        Task<int> Commit();

        IProductRepository ProductRepository { get; }
        IProductModelsRepository ProductModelsRepository { get; }
        IProductSubCategoriesRepository ProductSubCategoriesRepository { get; }
        IUnitMeasuresRepository UnitMeasuresRepository { get; }
        ICustomersRepository CustomersRepository { get; }
        IEmployeeRepository EmployeeRepository { get; }
        IProductPurchaseRepository ProductPurchaseRepository { get; }
        IProductSalesRepository ProductSalesRepository { get; }
        IProductTransactionRepository ProductTransactionRepository { get; }
        ISalesTerritoriesRepository SalesTerritoriesRepository { get; }
        IWarehousesRepository WarehousesRepository { get; }
        IProfitRepository ProfitRepository { get; }
        IPeopleRepository PeopleRepository { get; }
        IVendorsRepository VendorsRepository { get; }
        ILocationsRepository LocationsRepository { get; }
    }
}
