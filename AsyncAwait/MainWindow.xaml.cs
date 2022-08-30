using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AsyncAwait
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        void DoIndependentWork(int Count)
        {
            resultsTextBox.Text += $"[{Count}]Working . . . . . . .\r\n";
        }


        private async void StartButton_Click(object sender, RoutedEventArgs e)
        {
            Task<int> getStringTask = AccessTheWebAsync();

            DoIndependentWork(3);

            int contentLength = await getStringTask;
            DoIndependentWork(4);
            resultsTextBox.Text +=
                $"\r\nLength of the downloaded string: {contentLength}.\r\n\r\n";

        }

        // Three things to note in the signature:  
        //  - The method has an async modifier.   
        //  - The return type is Task or Task<T>. (See "Return Types" section.)  
        //    Here, it is Task<int> because the return statement returns an integer.  
        //  - The method name ends in "Async."  
        async Task<int> AccessTheWebAsync()
        {
            // You need to add a reference to System.Net.Http to declare client.  
            using (HttpClient client = new HttpClient())
            {
                // GetStringAsync returns a Task<string>. That means that when you await the  
                // task you'll get a string (urlContents).  

                DoIndependentWork(1);

                string urlContents = await client.GetStringAsync("https://docs.microsoft.com");

                // You can do work here that doesn't rely on the string from GetStringAsync.  
                DoIndependentWork(2);

                // The return statement specifies an integer result.  
                // Any methods that are awaiting AccessTheWebAsync retrieve the length value.  
                return urlContents.Length;
            }
        }


        private async void StartButton2_Click(object sender, RoutedEventArgs e)
        {
            int contentLength = await AccessTheWebAsync2();

            DoIndependentWork(4);
            resultsTextBox.Text +=
                $"\r\nLength of the downloaded string: {contentLength}.\r\n\r\n";
        }

        async Task<int> AccessTheWebAsync2()
        {
            // You need to add a reference to System.Net.Http to declare client.  
            using (HttpClient client = new HttpClient())
            {
                DoIndependentWork(1);

                Task<string> getStringTask = client.GetStringAsync("https://docs.microsoft.com");

                DoIndependentWork(2);

                string urlContents = await getStringTask;

                DoIndependentWork(3);

                return urlContents.Length;
            }
        }

        private async void StartButton3_Click(object sender, RoutedEventArgs e)
        {
            Task<int> getStringTask = AccessTheWebAsync3();

            DoIndependentWork(4);

            int contentLength = await getStringTask;
            DoIndependentWork(5);
            resultsTextBox.Text +=
                $"\r\nLength of the downloaded string: {contentLength}.\r\n\r\n";
        }

        async Task<int> AccessTheWebAsync3()
        {
            // You need to add a reference to System.Net.Http to declare client.  
            using (HttpClient client = new HttpClient())
            {
                DoIndependentWork(1);

                Task<string> getStringTask = client.GetStringAsync("https://docs.microsoft.com");

                DoIndependentWork(2);

                string urlContents = await getStringTask;

                DoIndependentWork(3);

                return urlContents.Length;
            }
        }


        private async void StartButton4_Click(object sender, RoutedEventArgs e)
        {
            Task<int> getStringTask = AccessTheWebAsync();

            DoIndependentWork(1000); 

            int Data = await getStringTask;

            resultsTextBox.Text +=
             $"\r\nLength of the downloaded string: {Data}.\r\n\r\n";
        }

        private void StartButton5_Click(object sender, RoutedEventArgs e)
        {
            int Data = AccessTheWebAsync5();

            DoIndependentWork(1000);

            resultsTextBox.Text +=
             $"\r\nLength of the downloaded string: {Data}.\r\n\r\n";
        }

        int AccessTheWebAsync5()
        {
            using (HttpClient client = new HttpClient())
            {
                DoIndependentWork(1);

                string urlContents = client.GetStringAsync("https://docs.microsoft.com").Result;

                DoIndependentWork(2);

                return urlContents.Length;
            }
        }

        private async void StartButton6_Click(object sender, RoutedEventArgs e)
        {
            Task<int> getStringTask = AccessTheWebAsync6();

            DoIndependentWork(4);

            int contentLength = await getStringTask;

            DoIndependentWork(1000);

            resultsTextBox.Text +=
             $"\r\nLength of the downloaded string: {contentLength}.\r\n\r\n";
        }

        async Task<int> AccessTheWebAsync6()
        {
            using (HttpClient client = new HttpClient())
            {
                DoIndependentWork(1);

                var Data = Task.Run(()=> client.GetStringAsync("https://docs.microsoft.com"));

                DoIndependentWork(2);

                string urlContents = await Data;

                DoIndependentWork(3);

                return urlContents.Length;
            }
        }

        private async void StartButton7_Click(object sender, RoutedEventArgs e)
        {
            int contentLength = AccessTheWebAsync7();

            DoIndependentWork(1000);

            resultsTextBox.Text +=
             $"\r\nLength of the downloaded string: {contentLength}.\r\n\r\n";
        }

        int AccessTheWebAsync7()
        {
            using (HttpClient client = new HttpClient())
            {
                DoIndependentWork(1);

                string urlContents = Task.Run(() => client.GetStringAsync("https://docs.microsoft.com")).Result;

                DoIndependentWork(2);

                return urlContents.Length;
            }
        }
    }
}
