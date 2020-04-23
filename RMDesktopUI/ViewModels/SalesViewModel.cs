﻿using Caliburn.Micro;
using RMDesktopUI.Library.Api;
using RMDesktopUI.Library.Models;
using System.ComponentModel;
using System.Threading.Tasks;

namespace RMDesktopUI.ViewModels
{
    public class SalesViewModel : Screen
    {

		private BindingList<ProductModel> _products;
		private IProductEndpoint _productEndpoint;

		public SalesViewModel(IProductEndpoint productEndpoint)
		{
			_productEndpoint = productEndpoint;
			
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

		private BindingList<ProductModel> _cart;

		public BindingList<ProductModel> Cart
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
