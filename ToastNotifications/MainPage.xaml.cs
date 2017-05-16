using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Notifications;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ToastNotifications
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.  The Parameter
        /// property is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            UpdateList();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var template = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);   

            for (int i = 0; i < 10; i++)
			{
			    DateTime d = DateTime.Now.AddHours(1).AddDays(i);
                ToastNotificationManager.CreateToastNotifier().AddToSchedule(new ScheduledToastNotification(template, new DateTimeOffset(d)));
			}
            UpdateList();
        }

        private void UpdateList()
        {
            List<Item> items = new List<Item>();
            foreach (var n in ToastNotificationManager.CreateToastNotifier().GetScheduledToastNotifications())
            {
                items.Add(new Item { DeliveryTime = n.DeliveryTime });
            }
            List.ItemsSource = items;
        }
    }

    public class Item
    {
        public DateTimeOffset DeliveryTime { get; set; }
    }
}
