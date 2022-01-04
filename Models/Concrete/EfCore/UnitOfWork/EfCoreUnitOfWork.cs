using BusinessAnalytic.Models.Abstract;
using System.Threading.Tasks;

namespace BusinessAnalytic.Models.Concrete.EfCore.UnitOfWork
{
    public class EfCoreUnitOfWork : IEfCoreUnitOfWork
    {
        private readonly AdventureWorks2019Context _context;

        public IProductRepository ProductRepository {get; set;}
        public IProductModelsRepository ProductModelsRepository { get; set; }
        public IProductSubCategoriesRepository ProductSubCategoriesRepository { get; set; }
        public IUnitMeasuresRepository UnitMeasuresRepository { get; set; }
        public ICustomersRepository CustomersRepository { get; set; }
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IProductPurchaseRepository ProductPurchaseRepository { get; set; }
        public IProductSalesRepository ProductSalesRepository { get; set; }
        public IProductTransactionRepository ProductTransactionRepository { get; set; }
        public ISalesTerritoriesRepository SalesTerritoriesRepository { get;set; }
        public IWarehousesRepository WarehousesRepository { get; set;}
        public IProfitRepository ProfitRepository { get; set; }
        public IPeopleRepository PeopleRepository { get; set; }
        public IVendorsRepository VendorsRepository { get; set; }
        public ILocationsRepository LocationsRepository { get; set; }
        public EfCoreUnitOfWork(AdventureWorks2019Context context, IProductRepository productRepository, IProductModelsRepository productModelsRepository,
            IProductSubCategoriesRepository productSubCategoriesRepository, IUnitMeasuresRepository unitMeasuresRepository, ICustomersRepository customersRepository,
            IEmployeeRepository employeeRepository, IProductPurchaseRepository productPurchaseRepository, IProductSalesRepository productSalesRepository,
            IProductTransactionRepository productTransactionRepository, ISalesTerritoriesRepository salesTerritoriesRepository, IWarehousesRepository warehousesRepository,
            IProfitRepository profitRepository, IPeopleRepository peopleRepository, IVendorsRepository vendorsRepository, ILocationsRepository locationsRepository)
        {
            ProductModelsRepository = productModelsRepository;
            ProductRepository = productRepository;    
            ProductSubCategoriesRepository = productSubCategoriesRepository;
            UnitMeasuresRepository = unitMeasuresRepository;
            CustomersRepository = customersRepository;
            _context = context;
            EmployeeRepository = employeeRepository;
            ProductPurchaseRepository = productPurchaseRepository;
            ProductSalesRepository = productSalesRepository;
            ProductTransactionRepository = productTransactionRepository;
            SalesTerritoriesRepository = salesTerritoriesRepository;
            WarehousesRepository = warehousesRepository;
            ProfitRepository = profitRepository;
            PeopleRepository = peopleRepository;
            VendorsRepository = vendorsRepository;
            LocationsRepository = locationsRepository;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> Commit()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
