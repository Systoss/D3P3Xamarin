using System;
using System.Collections.Generic;
using BlankApp1.Models;
using Plugin.Settings.Abstractions;

namespace BlankApp1.Util
{
	public interface ISessionStore
	{
		string AccessToken { get; set; }
		int ExpiresInSeconds { get; set; }
		string IdToken { get; set; }
		string RefreshToken { get; set; }
		string SessionId { get; set; }
		ISettings Settings { get; set; }
		DateTime TokenExpiresServer { get; set; }
		DateTime TokenIssuedServer { get; set; }
		string UserName { get; set; }
		bool IsAdmin { get; set; }
		string LastTab { get; set; }

		bool IsLoggedIn(DateTime now);
		void Logout();
	}
}