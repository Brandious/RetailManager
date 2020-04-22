using Caliburn.Micro;
using System.ComponentModel;

namespace RMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {
		private BindingList<string> _products;

		public BindingList<string> Products
		{
			get { return _products; }
			set 
			{ 
				_products = value;
				NotifyOfPropertyChange(() => Products);
			}
		}

		private int _itemQuantity;

		public int ItemQuantity
		{
			get { return _itemQuantity; }
			set 
			{
				_itemQuantity = value;
				NotifyOfPropertyChange(() => ItemQuantity);
			}
		}

		private BindingList<string> _cart;

		public BindingList<string> Cart
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
				//Add Calculation
				return "$ 0.00";
			}

		}


		public string Tax
		{
			get
			{
				//Add Calculation
				return "$ 0.00";
			}

		}

		public string Total
		{
			get
			{
				//Add Calculation
				return "$ 0.00";
			}

		}

		public bool CanAddToCart
		{
			get
			{
				bool output = false;

				//Make sure something is Selected
				//Make sure ItemQuantity true

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
				bool output = false;

				//Make sure something is Selected
				//Make sure ItemQuantity true

				return output;
			}
		}

		public void AddToCart()
		{

		}

		public void RemoveFromCart()
		{

		}

		public void CheckOut()
		{

		}


	}
}
