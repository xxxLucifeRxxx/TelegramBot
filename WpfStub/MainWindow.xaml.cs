using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using GMap.NET;

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
					var list = GetData();
					foreach (var item in list)
					{
						ListBoxMain.Items.Add(item);
					}

					var last = list.LastOrDefault();
					if (last?.LatFrom != null && last.LonFrom.HasValue)
						mapView.Position = new PointLatLng(last.LatFrom.Value, last.LonFrom.Value);

					if (last?.LatTo != null && last.LonTo.HasValue)
						mapView2.Position = new PointLatLng(last.LatTo.Value, last.LonTo.Value);
				});
			};

			_timer.Start();

		}

		private List<MyClass> GetData()
		{
			var list= new List<MyClass>();
			var db = new db_botEntities();
			var applications = db.Applications.Where(x => x.State == 1).ToList();

			foreach (var item in applications)
			{
				list.Add(new MyClass()
				{
					Phone = item.NumbPhone,
					Time = item.Time,
					Payment = item.PaymentMethod,
					LonFrom = item.LongitudeFrom,
					LatFrom = item.LatitudeFrom,
					LonTo = item.LongitudeTo,
					LatTo = item.LatitudeTo
				});
			}

		

			return list;
		}

		class MyClass
		{
			public string Phone { get; set; }
			public DateTime? Time { get; set; }
			public int? Payment { get; set; }

			public string Pay => ((Payment ?? 0 ) == 1)? "Наличные":( ((Payment ?? 0) == 2)? "Мобильный банк":"");
			public float? LonFrom { get; set; }
			public float? LatFrom

			{ get; set; }

			public float? LatTo { get; set; }
			public float? LonTo 
			{ get; set; }

			public override string ToString()
			{
				return $"{Phone:11} {Time?.ToShortTimeString()} {Pay}";
			}
		}


		private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			var list = (ListBox) sender;
			var myClass = (MyClass) list.SelectedValue;
			if (myClass?.LatFrom != null && myClass.LonFrom.HasValue)
				mapView.Position = new PointLatLng(myClass.LatFrom.Value, myClass.LonFrom.Value);

			if (myClass?.LatTo != null && myClass.LonTo.HasValue)
				mapView2.Position = new PointLatLng(myClass.LatTo.Value, myClass.LonTo.Value);

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


		private void MapView_OnLoaded(object sender, RoutedEventArgs e)
		{
			throw new System.NotImplementedException();
		}

		private void FrameworkElement_OnLoaded(object sender, RoutedEventArgs e)
		{
			GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
			// choose your provider here
			mapView.MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;
			mapView.MinZoom = 2;
			mapView.MaxZoom = 18;
			// whole world zoom
			mapView.Zoom = 18;
			// lets the map use the mousewheel to zoom
			mapView.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
			// lets the user drag the map
			mapView.CanDragMap = true;
			mapView.Position = new PointLatLng(42.161904, -26.023755);
			// lets the user drag the map with the left mouse button
			mapView.DragButton = MouseButton.Left;



			GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
			// choose your provider here
			mapView2.MapProvider = GMap.NET.MapProviders.OpenStreetMapProvider.Instance;
			mapView2.MinZoom = 2;
			mapView2.MaxZoom = 18;
			// whole world zoom
			mapView2.Zoom = 18;
			// lets the map use the mousewheel to zoom
			mapView2.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
			// lets the user drag the map
			mapView2.CanDragMap = true;
			mapView2.Position = new PointLatLng(42.161904, -26.023755);
			// lets the user drag the map with the left mouse button
			mapView2.DragButton = MouseButton.Left;
		}
	}
}
