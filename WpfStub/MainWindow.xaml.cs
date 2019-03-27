using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;

namespace WpfStub
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			_timer = new Timer {Interval = 1000};
			_timer.Elapsed += (sender, eventArgs) =>
			{
				Dispatcher.Invoke(() =>
				{
					ListBoxMain.Items.Clear();
					foreach (var item in GetData())
					{
						ListBoxMain.Items.Add("" + item);
					}
				});
			};

			_timer.Start();

		}

		private List<string> GetData()
		{
			var list= new List<string>();
			var db = new db_botEntities();
			var applications = db.Applications.Where(x => x.State == 1).ToList();
		
			foreach (var item in applications)
			{
				list.Add($"{item.NumbPhone} {item.Time} {item.PaymentMethod}");
			}
			
			return list;
		}


		private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{

		}

		private readonly Timer _timer;
		


		private void Start_OnClick(object sender, RoutedEventArgs e)
		{
			_timer.Start();
		}

		private void Stop_OnClick(object sender, RoutedEventArgs e)
		{
			_timer.Stop();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
