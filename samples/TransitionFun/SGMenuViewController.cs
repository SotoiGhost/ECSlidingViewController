using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreAnimation;
using ECSlidingViewControllerSDK;

namespace TransitionFun {

	public class SGMenuViewController : UITableViewController {
	
		#region Properties

		UINavigationController TransitionNC { get; set; }

		string[] menu_items;
		string[] MenuItems {
			get {
				if (menu_items != null) return menu_items;

				menu_items = new string [] { "Transitions", "Settings" };
				return menu_items;
			}
			set { menu_items = value; }	
		}

		#endregion

		public SGMenuViewController ()
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			TransitionNC = (UINavigationController)ECSlidingViewControllerExtension.SlidingViewController (this).TopViewController;

			RegisterNotifications ();
			TableView.Source = new TableViewSource (MenuItems);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
			View.EndEditing (true);
		}

		#region Notifications Methods

		void RegisterNotifications ()
		{
			NSNotificationCenter.DefaultCenter.AddObserver (SGGeneral.ResetZoomTransitionsNotification, ResetZoomTransitions);
		}

		void UnregisterNotifications ()
		{
			NSNotificationCenter.DefaultCenter.RemoveObserver (this, (NSString) SGGeneral.ResetZoomTransitionsNotification);
		}

		void ResetZoomTransitions (NSNotification notif)
		{
			string menuItem = (NSString) notif.Object;

			// This undoes the Zoom Transition's scale because it affects the other transitions.
			// You normally wouldn't need to do anything like this, but we're changing transitions
			// dynamically so everything needs to start in a consistent state.
			ECSlidingViewControllerExtension.SlidingViewController (this).TopViewController.View.Layer.Transform = CATransform3D.MakeScale (1, 1, 1);

			if (menuItem.Equals ("Transitions")) {
				ECSlidingViewControllerExtension.SlidingViewController (this).TopViewController = TransitionNC;
			} else {
				// TODO: Create an instance of SGSettingsNavigationController
			}

			ECSlidingViewControllerExtension.SlidingViewController (this).ResetTopView (true);
		}

		#endregion

	}

	class TableViewSource : UITableViewSource {

		string [] menu_items;

		public TableViewSource (string [] items)
		{
			menu_items = items;
		}

		#region UITableViewDataSource

		public override int RowsInSection (UITableView tableview, int section)
		{
			return menu_items.Length;
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			string cellID = "MenuCellID";

			UITableViewCell cell = tableView.DequeueReusableCell (cellID);

			if (cell == null)
				cell = new UITableViewCell (UITableViewCellStyle.Default, cellID);

			cell.TextLabel.Text = menu_items [indexPath.Row];
			cell.BackgroundColor = UIColor.Clear;

			return cell;
		}

		#endregion

		#region UITableViewDelegate

		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			string menuItem = menu_items [indexPath.Row];
			NSNotificationCenter.DefaultCenter.PostNotificationName (SGGeneral.ResetZoomTransitionsNotification, (NSString) menuItem);
		}

		#endregion

	}
}

