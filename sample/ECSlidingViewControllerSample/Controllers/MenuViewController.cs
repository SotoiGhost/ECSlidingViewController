using System;
using System.CodeDom.Compiler;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using ECSlidingViewController1X;

namespace ECSlidingViewControllerSample
{
	partial class MenuViewController : UIViewController
	{

		#region Constructors

		public MenuViewController () : base ()
		{
		}

		public MenuViewController (IntPtr handle) : base (handle)
		{
		}

		#endregion

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			tblMenu.Source = new TableViewSource (this);

			SlidingViewExtension.SlidingViewController (this).AnchorRightRevealAmount = 280f;
			SlidingViewExtension.SlidingViewController (this).UnderLeftWidthLayout = ECViewWidthLayout.FullWidth;
		}
	}

	class TableViewSource : UITableViewSource
	{
		static string cellID = "MenuItemCell";

		UIViewController ownerVC;
		string[] menuItems;

		public TableViewSource (UIViewController owner)
		{
			ownerVC = owner;
			menuItems = new [] { "First", "Second" };
		}

		#region UITableViewDataSource

		public override int NumberOfSections (UITableView tableView)
		{
			return 1;
		}

		public override int RowsInSection (UITableView tableView, int section)
		{
			return menuItems.Length;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			UITableViewCell cell = tableView.DequeueReusableCell (cellID);

			if (cell == null) {
				cell = new UITableViewCell (UITableViewCellStyle.Default, cellID);
				cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
			}

			cell.TextLabel.Text = menuItems [indexPath.Row];

			return cell;
		}

		#endregion

		#region UITableViewDelegate

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			string viewID = menuItems [indexPath.Row] + "TopViewControllerID";

			UIViewController newTopVC = ownerVC.Storyboard.InstantiateViewController (viewID) as UIViewController;
			SlidingViewExtension.SlidingViewController (ownerVC).TopViewController = newTopVC;
			SlidingViewExtension.SlidingViewController (ownerVC).ResetTopView ();
		}

		#endregion
	}

}
