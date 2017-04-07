using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Commands;
using Prism.Events;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows.Controls;
using wpfShoppingCart;
using wpfShoppingCart.Events;
using System.Windows;
using System.Collections.ObjectModel;

namespace wpfShoppingCart.ViewModels
{
    public class CartViewModel:BindableBase
    {

        HttpClient client = new HttpClient();
        OrderDetails myOrder;
        
        private ObservableCollection<FinalOrder> _myCart;
        public ObservableCollection<FinalOrder> myCart { get {return _myCart; } set {SetProperty(ref _myCart,value); RaisePropertyChanged(); } }

        private FinalOrder _selectedOrder;
        public FinalOrder selectedOrder { get {return _selectedOrder; } set {SetProperty(ref _selectedOrder,value); } }

        private int _finalCost;
        public int finalCost { get {return _finalCost; } set {SetProperty(ref _finalCost,value); } }

        public DelegateCommand deleteCommand { get; set; }
        public DelegateCommand placeOrderCommand { get; set; }

        public CartViewModel(IEventAggregator eventAggregator)
        {
            myOrder = new OrderDetails();
            _selectedOrder = new FinalOrder();
            _myCart = new ObservableCollection<FinalOrder>();
            eventAggregator.GetEvent<UpdateEvent>().Subscribe(getOrder);
            client.BaseAddress = new Uri("http://localhost:58076/");
            client.DefaultRequestHeaders.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            deleteCommand = new DelegateCommand(deleteOrder);
            placeOrderCommand = new DelegateCommand(placeOrder);
        }

        private async void placeOrder()
        {
            try
            {
                //myOrder.productId = 1;
                //myOrder.TotalCost = 234;
                //myOrder.Quantity = 3;
                //var response = await client.PostAsJsonAsync("api/ProductDetails", myOrder);
                //response.EnsureSuccessStatusCode();
                foreach (var item in myCart)
                {
                    myOrder.productId = item.productId;
                    myOrder.TotalCost = item.TotalCost;
                    myOrder.Quantity = item.Quantity;
                    var response = await client.PostAsJsonAsync("api/OrderDetails", myOrder);
                    response.EnsureSuccessStatusCode();
                }
                MessageBox.Show("Order Plcaed successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void deleteOrder()
        {
            MessageBox.Show(selectedOrder.productName);
            _finalCost -= selectedOrder.TotalCost;
            _myCart.Remove(selectedOrder);
            
        }

        private void getOrder(FinalOrder fo)
        {
            _myCart.Add(fo);
            finalCost += fo.TotalCost;
            MessageBox.Show(fo.productName + " with quantity = " +fo.Quantity +" has been added to the cart");
        }
    }
}
