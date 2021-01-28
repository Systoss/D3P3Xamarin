using System;
using System.Collections.Generic;
using System.Text;

namespace BlankApp1.Util
{
	public enum CognitoEvent
	{
		/// <summary>
		/// The sign in screen should be shown.
		/// </summary>
		DoSignin,
		/// <summary>
		/// The user has successfully authenticated
		/// </summary>
		Authenticated,
		/// <summary>
		/// The password is invalid
		/// </summary>
		BadPassword,
		/// <summary>
		/// The user was not found.
		/// </summary>
		UserNotFound,
		/// <summary>
		/// The default password needs to be changed.
		/// </summary>
		PasswordChangedRequired,
		/// <summary>
		/// The default password has been successfully updated
		/// </summary>
		PasswordUpdated,
		/// <summary>
		/// The default password update has failed.
		/// </summary>
		PasswordUpdateFailed,
		/// <summary>
		/// The password change failed the requirements check
		/// </summary>
		PasswordRequirementsFailed,
	}
}
