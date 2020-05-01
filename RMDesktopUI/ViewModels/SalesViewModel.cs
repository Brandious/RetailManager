using Caliburn.Micro;
using RMDesktopUI.Library.Api;
using RMDesktopUI.Library.Helpers;
using RMDesktopUI.Library.Models;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace RMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {

		private BindingList<ProductModel> _products;
		private IProductEndpoint _productEndpoint;
		private IConfigHelper _configHelper;
		private ISaleEndpoint _saleEndpoint;

		public SalesViewModel(IProductEndpoint productEndpoint, IConfigHelper configHelper, ISaleEndpoint saleEndpoint)
		{
			_productEndpoint = productEndpoint;
			_configHelper = configHelper;
			_saleEndpoint = saleEndpoint;

			
		}


		protected override async void OnViewLoaded(object view)
		{
			base.OnViewLoaded(view);
			await LoadProducts();
		}

		private async Task LoadProducts()
		{
			Products = new BindingList<ProductModel>(await _productEndpoint.GetAll());
		}
		public BindingList<ProductModel> Products
		{
			get { return _products; }
			set 
			{ 
				_products = value;
				NotifyOfPropertyChange(() => Products);
			}
		}

		private ProductModel _selectedProduct;

		public ProductModel SelectedProduct
		{
			get { return _selectedProduct; }
			set 
			{
				_selectedProduct = value;
				NotifyOfPropertyChange(() => SelectedProduct);
				NotifyOfPropertyChange(() => CanAddToCart);

			}
		}

		private int _itemQuantity = 1;

		public int ItemQuantity
		{
			get { return _itemQuantity; }
			set 
			{
				_itemQuantity = value;
				NotifyOfPropertyChange(() => ItemQuantity);
				NotifyOfPropertyChange(() => CanAddToCart);
			}
		}

		private BindingList<CartItemModel> _cart = new BindingList<CartItemModel>();

		public BindingList<CartItemModel> Cart
		{
			get { return _cart; }
			set 
			{ 
				_cart = value;
				NotifyOfPropertyChange(() => Cart);
			}
		}


		public string SubTotal
		{
			get 
			{
				return CalculateSubtotal().ToString("c");
			}

		}

		private decimal CalculateSubtotal()
		{
			return Cart.Sum(x => x.Product.RetailPrice * x.QuantityInCart);
		}

		public string Tax
		{
			get
			{

				return CalculateTax().ToString("C");
		 		
			}

		}

		private decimal CalculateTax()
		{


			return Cart.Where(x => x.Product.IsTaxable)
				       .Sum(x => x.Product.RetailPrice * x.QuantityInCart * _configHelper.GetTaxRate());

		}

		public string Total
		{
			get
			{
				//Add Calculation
				return (CalculateSubtotal() + CalculateTax()).ToString("C");
			}

		}

		public bool CanAddToCart
		{
			get
			{
				bool output = false;

				//Make sure something is Selected
				//Make sure ItemQuantity true
				if (ItemQuantity > 0 && SelectedProduct?.QuantityInStock >= ItemQuantity)
					output = true;

				return output;
			}
		}

		public bool CanRemoveFromCart
		{
			get
			{
				bool output = false;

				//Make sure something is Selected
				//Make sure ItemQuantity true

				return output;
			}
		}

		public bool CanCheckOut
		{
			get
			{
				
				
				
				
				return (Cart.Count > 0) ? true : false;			
			}
		}

		public void AddToCart()
		{
			CartItemModel existingItem = Cart.FirstOrDefault(x => x.Product == SelectedProduct);
			if(existingItem != null)
			{
				existingItem.QuantityInCart += ItemQuantity;
				Cart.Remove(existingItem);
				Cart.Add(existingItem);
			}
			else
			{
				CartItemModel item = new CartItemModel
				{
					Product = SelectedProduct,
					QuantityInCart = ItemQuantity

				};
				Cart.Add(item);
			}			
			SelectedProduct.QuantityInStock -= ItemQuantity;
			ItemQuantity = 1;

			NotifyOfPropertyChange(() => SubTotal);
			NotifyOfPropertyChange(() => Tax);
			NotifyOfPropertyChange(() => Total);
			NotifyOfPropertyChange(() => CanCheckOut);

		}

		public void RemoveFromCart()
		{

			NotifyOfPropertyChange(() => SubTotal);
			NotifyOfPropertyChange(() => Tax);
			NotifyOfPropertyChange(() => Total);
			NotifyOfPropertyChange(() => CanCheckOut);
		}

		public async Task CheckOut()
		{
			// Create a Sale Model, Post it to API
			SaleModel sale = new SaleModel();
			foreach (var item in Cart)
			{
				sale.SaleDetails.Add(new SaleDetailModel{
					ProductId = item.Product.Id,
					Quantity = item.QuantityInCart
				});
			}

			await _saleEndpoint.PostSale(sale);


		}


	}
}
