using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web.UI;
using System.Web.UI.WebControls;
using Vertica.Utilities_v4;
using Vertica.Utilities_v4.Extensions.StringExt;
using Vertica.Utilities_v4.Resources;

namespace Vertica.Utilities_v4.Extensions.ControlExt
{
	public static class ControlExtensions
	{
		public static T FindControl<T>(this Control parentControl, string targetControlID)
		where T : Control
		{
			Guard.AgainstNullArgument("parentControl", parentControl);
			Guard.AgainstArgument("targetControlID", targetControlID.IsEmpty());
			Control control = parentControl.FindControl(targetControlID);
			if (control == null)
			{
				string controlExtensionsNotFoundTemplate = Exceptions.ControlExtensions_NotFoundTemplate;
				string[] strArrays = new string[] { targetControlID, parentControl.ID };
				ExceptionHelper.Throw<ArgumentOutOfRangeException>(controlExtensionsNotFoundTemplate, strArrays);
			}
			return (T)control;
		}

		public static T LoadControl<T>(this TemplateControl entry)
		where T : UserControl
		{
			return (T)entry.LoadControl(ControlExtensions.userControlFileName<T>());
		}

		public static T LoadControl<T>(this TemplateControl entry, Uri userControlsPath)
		where T : UserControl
		{
			return (T)entry.LoadControl(string.Concat(userControlsPath.ToString().IfNotThere().Append("/"), ControlExtensions.userControlFileName<T>()));
		}

		public static bool SelectValue(this ListControl list, string value)
		{
			if (list == null)
			{
				throw new ArgumentNullException("list");
			}
			if (list.Items.Count > 0)
			{
				ListItem listItem = list.Items.FindByValue(value);
				if (listItem != null)
				{
					list.ClearSelection();
					listItem.Selected = true;
					return true;
				}
			}
			return false;
		}

		public static bool TryFindControl<T>(this Control parentControl, string targetControlID, out T control)
		where T : Control
		{
			control = default(T);
			try
			{
				control = parentControl.FindControl<T>(targetControlID);
				return control != null;
			}
			catch (ArgumentException argumentException)
			{
			}
			catch (InvalidCastException invalidCastException)
			{
			}
			return false;
		}

		private static string userControlFileName<T>()
		where T : UserControl
		{
			return string.Concat(typeof(T).Name, ".ascx");
		}
	}
}