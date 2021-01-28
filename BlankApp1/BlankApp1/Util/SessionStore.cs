using System;
using System.Collections.Generic;
using System.Text;
using BlankApp1.Models;
using Newtonsoft.Json;
using Plugin.Settings.Abstractions;

namespace BlankApp1.Util
{
	public class SessionStore : ISessionStore
	{
		public ISettings Settings { get; set; }

		public bool IsLoggedIn(DateTime now)
		{
			return !string.IsNullOrEmpty(IdToken) && !string.IsNullOrEmpty(RefreshToken) && TokenExpiresServer > now;
		}

		public void Logout()
		{
			IdToken = AccessToken = RefreshToken = String.Empty;
			TokenIssuedServer = TokenExpiresServer = default(DateTime);
			ExpiresInSeconds = 0;
			LastTab = string.Empty;
		}

		public String UserName
		{
			get { return Settings.GetValueOrDefault(nameof(UserName), String.Empty); }
			set { Settings.AddOrUpdateValue(nameof(UserName), value); }
		}

		public String IdToken
		{
			get { return Settings.GetValueOrDefault(nameof(IdToken), String.Empty); }
			set { Settings.AddOrUpdateValue(nameof(IdToken), value); }
		}

		public String AccessToken
		{
			get { return Settings.GetValueOrDefault(nameof(AccessToken), String.Empty); }
			set { Settings.AddOrUpdateValue(nameof(AccessToken), value); }
		}

		public String RefreshToken
		{
			get { return Settings.GetValueOrDefault(nameof(RefreshToken), String.Empty); }
			set { Settings.AddOrUpdateValue(nameof(RefreshToken), value); }
		}

		public String SessionId
		{
			get { return Settings.GetValueOrDefault(nameof(SessionId), String.Empty); }
			set { Settings.AddOrUpdateValue(nameof(SessionId), value); }
		}

		public DateTime TokenIssuedServer
		{
			get { return Settings.GetValueOrDefault(nameof(TokenIssuedServer), default(DateTime)); }
			set { Settings.AddOrUpdateValue(nameof(TokenIssuedServer), value); }
		}

		public DateTime TokenExpiresServer
		{
			get { return Settings.GetValueOrDefault(nameof(TokenExpiresServer), default(DateTime)); }
			set { Settings.AddOrUpdateValue(nameof(TokenExpiresServer), value); }
		}

		public int ExpiresInSeconds
		{
			get { return Settings.GetValueOrDefault(nameof(ExpiresInSeconds), 0); }
			set { Settings.AddOrUpdateValue(nameof(ExpiresInSeconds), value); }
		}

		public bool IsAdmin
		{
			get { return Settings.GetValueOrDefault(nameof(IsAdmin), default(bool)); }
			set { Settings.AddOrUpdateValue(nameof(IsAdmin), value); }
		}

		public string LastTab
		{
			get { return Settings.GetValueOrDefault(nameof(LastTab), string.Empty); }
			set { Settings.AddOrUpdateValue(nameof(LastTab), value); }
		}
	}
}
