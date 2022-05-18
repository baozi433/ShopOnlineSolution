using Blazored.LocalStorage;
using ShopOnlie.Models.Dtos;
using ShopOnline.Web.Services.Contracts;

namespace ShopOnline.Web.Services
{
    public class ManageProductsLocalStorageService : IManageProductsLocalStorageService
    {
        public readonly ILocalStorageService localStorageService;
        public readonly IProductService productService;

        // Set key for local storage (browser)
        private const string key = "ProductCollection";

        // Dependency Injection at runtime in the constructor
        public ManageProductsLocalStorageService(ILocalStorageService lcoalStorageService,
                                                 IProductService productService)
        {
            this.localStorageService = lcoalStorageService;
            this.productService = productService;
        }
        public async Task<IEnumerable<ProductDto>> GetCollection()
        {
            // Check the local storage first if not add collection
            return await this.localStorageService.GetItemAsync<IEnumerable<ProductDto>>(key) ?? await AddCollection();
        }

        public async Task RemoveCollection()
        {
            await this.localStorageService.RemoveItemAsync(key);
        }

        private async Task<IEnumerable<ProductDto>> AddCollection()
        {
            var productCollection = await this.productService.GetItems();

            if(productCollection != null)
            {
                await this.localStorageService.SetItemAsync(key, productCollection);
            }

            return productCollection;
        }
    }
}
