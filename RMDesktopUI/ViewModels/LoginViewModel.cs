﻿using Caliburn.Micro;
using RMDesktopUI.EventModels;
using RMDesktopUI.Helpers;
using RMDesktopUI.Library.Api;
using System;
using System.Threading.Tasks;

namespace RMDesktopUI.ViewModels
{
	public class LoginViewModel : Screen
	{
		private string _userName = "helloworld@gmail.com";
		private string _password = "Hello@1";
		private IAPIHelper _apiHelper;
		private IEventAggregator _events;

		public LoginViewModel(IAPIHelper apiHelper, IEventAggregator events)
		{
			_apiHelper = apiHelper;
			_events = events;
		}
		public string UserName
		{
			get { return _userName; }
			set
			{
				_userName = value;
				NotifyOfPropertyChange(() => UserName);
				NotifyOfPropertyChange(() => CanLogIn);
			}
		}

		public string Password
		{
			get { return _password; }
			set
			{
				_password = value;
				NotifyOfPropertyChange(() => Password);
				NotifyOfPropertyChange(() => CanLogIn);
				
			}
		}

		
		public bool IsErrorVisible
		{
		
			get
			{
				bool output = false;
				
				if (ErrorMessage?.Length > 0)
					output = true;

				return output;
			}
			set { }
		}

		private string  _errorMessage;	

		public string  ErrorMessage
		{
			get { return _errorMessage; }
			set
			{ 
				_errorMessage = value;
				NotifyOfPropertyChange(() => ErrorMessage);
				NotifyOfPropertyChange(() => IsErrorVisible);
			}
		}


		public bool CanLogIn
		{
			get
			{
				bool output = false;
				if (UserName?.Length > 0 && Password?.Length > 0)
				{
					output = true;
				}
				return output;
			}
		}	
		
		public async Task LogIn()
		{
			try
			{
				ErrorMessage = "";
				var result = await _apiHelper.Authenticate(UserName, Password);

				await _apiHelper.GetLoggedInUserInfo(result.Access_Token);

				_events.PublishOnUIThread(new LogOnEvent());
			}
			catch(Exception ex)
			{
				ErrorMessage = ex.Message;
			}
		}

	}
}
