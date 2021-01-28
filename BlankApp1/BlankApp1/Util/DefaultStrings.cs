using System;
using System.Collections.Generic;
using System.Text;

namespace BlankApp1.Util
{
	public static class DefaultStrings
	{
		public static string SignInTitle { get; set; } = "Connection";
		public static string BadPassMessage { get; set; } = "Il y a eu un problème avec l'ID utilisateur ou le mot de passe.";
		public static string UserNotFoundMessage { get; set; } = "Impossible de trouver un utilisateur avec cet ID utilisateur";
		public static string PassUpdateFailedTitle { get; set; } = "Échec de la mise à jour du mot de passe";
		public static string PassUpdateFailedMessage { get; set; } = "Impossible de mettre à jour le mot de passe";
		public static string OkButton { get; set; } = "OK";
		public static string ValidationCreationTitle { get; set; } = "Création";
		public static string ValidationCreationOK { get; set; } = "Création réussi";
		public static string ValidationCreationKO { get; set; } = "Création non réussi";
		public static string RenseignerInformationTitle { get; set; } = "Informations manquantes";
		public static string RenseignerInformation { get; set; } = "Veuillez renseigner toute les informations";
		public static string SelectionSpace { get; set; } = "Veuillez selectionner un espace";

	}
}
