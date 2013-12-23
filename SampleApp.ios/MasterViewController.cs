using System;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using QuickCross;
using SampleApp.Shared;
using SampleApp.Shared.ViewModels;

namespace SampleApp.ios
{
	public partial class MasterViewController : TableViewControllerBase
    {
		private SampleItemListViewModel ViewModel { get { return SampleAppApplication.Instance.SampleItemListViewModel; } }

        public MasterViewController(IntPtr handle) : base(handle)
        {
            Title = NSBundle.MainBundle.LocalizedString("Master", "Master");
			
            if (UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Pad)
            {
                ContentSizeForViewInPopover = new SizeF(320f, 600f);
                ClearsSelectionOnViewWillAppear = false;
            }
        }

		public DetailViewController DetailViewController { get; set; }

		void AddNewItem(object sender, EventArgs args)
        {
			ViewModel.AddItemCommand.Execute(null);
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            NavigationItem.LeftBarButtonItem = EditButtonItem;
			NavigationItem.RightBarButtonItem = new UIBarButtonItem(UIBarButtonSystemItem.Add, AddNewItem);
			InitializeBindings(View, ViewModel);
        }
		/*
        class DataSource : UITableViewSource
        {
            static readonly NSString CellIdentifier = new NSString("Cell");
            readonly List<object> objects = new List<object>();
            readonly MasterViewController controller;

            public DataSource(MasterViewController controller)
            {
                this.controller = controller;
            }

            public IList<object> Objects
            {
                get { return objects; }
            }
            // Customize the number of sections in the table view.
            public override int NumberOfSections(UITableView tableView)
            {
                return 1;
            }

            public override int RowsInSection(UITableView tableview, int section)
            {
                return objects.Count;
            }
            // Customize the appearance of table view cells.
            public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
            {
                var cell = (UITableViewCell)tableView.DequeueReusableCell(CellIdentifier, indexPath);

                cell.TextLabel.Text = objects[indexPath.Row].ToString();

                return cell;
            }

            public override bool CanEditRow(UITableView tableView, NSIndexPath indexPath)
            {
                // Return false if you do not want the specified item to be editable.
                return true;
            }

            public override void CommitEditingStyle(UITableView tableView, UITableViewCellEditingStyle editingStyle, NSIndexPath indexPath)
            {
                if (editingStyle == UITableViewCellEditingStyle.Delete)
                {
                    // Delete the row from the data source.
                    objects.RemoveAt(indexPath.Row);
                    controller.TableView.DeleteRows(new NSIndexPath[] { indexPath }, UITableViewRowAnimation.Fade);
                }
                else if (editingStyle == UITableViewCellEditingStyle.Insert)
                {
                    // Create a new instance of the appropriate class, insert it into the array, and add a new row to the table view.
                }
            } */

		protected override object GetCommandParameter(string commandName)
		{
			if (commandName == "ViewItemCommand")
			{
				var indexPath = TableView.IndexPathForSelectedRow;
				if (indexPath != null) return ViewModel.Items[indexPath.Row];
			}
			return base.GetCommandParameter(commandName);
		}
    }
}