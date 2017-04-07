using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Prism.Mvvm;
using Prism.Commands;
using Prism.Events;
using System.Net.Http.Headers;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using wpfShoppingCart;
using wpfShoppingCart.Events;

namespace wpfShoppingCart.ViewModels
{
    public class ProductsViewModel:BindableBase
    {
        HttpClient client = new HttpClient();

        FinalOrder fo = new FinalOrder();
        
        public ProductDetails selectedProduct { get; set; }

        private List<int> _quantity= new List<int>() { 1,2,3,4,5,6,7,8,9,10 };
        public DelegateCommand selectedQuantityCommand { get; set; }

        private int _selectedQuantity=1;
        public int selectedQuantity { get {return _selectedQuantity; } set {SetProperty(ref _selectedQuantity,value); } }

       

        public List<int> quantity { get {return _quantity; } set { SetProperty(ref _quantity,value);  } }
        public ObservableCollection<ProductDetails> _listViewProducts;

        private IEventAggregator _eventAggregator;
        
        public ObservableCollection<ProductDetails> listViewProducts
        {
            get { return _listViewProducts; }
            set { SetProperty(ref _listViewProducts, value);  }
        }
        

        public ProductsViewModel(IEventAggregator eventAggregator)
        {
            
            selectedProduct = new ProductDetails();
            _listViewProducts = new ObservableCollection<ProductDetails>();
            client.BaseAddress = new Uri("http://localhost:58076/");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            LoadProducts();
            _eventAggregator = eventAggregator;
            selectedQuantityCommand = new DelegateCommand(exe);
        }

        private void exe()
        {
            fo.imageUrl = selectedProduct.imageUrl;
            fo.Quantity = selectedQuantity;
            fo.productName = selectedProduct.productName;
            fo.TotalCost = selectedProduct.price * selectedQuantity;
            fo.productId = selectedProduct.productId;
            _eventAggregator.GetEvent<UpdateEvent>().Publish(fo);
            
           
            
        }

        private async void LoadProducts()
        {
            var response = await client.GetAsync("api/ProductDetails");
            response.EnsureSuccessStatusCode();
            var products = await response.Content.ReadAsAsync<IEnumerable<ProductDetails>>();
            foreach (var e in products)
            {
                _listViewProducts.Add(e);
            }

            
        }
    }
    public class ProductDetails
    {
        public int productId { get; set; }
        public string productName { get; set; }
        public string imageUrl { get; set; }
        public int price { get; set; }
    }

}
