using System.Windows;
using MarvelPortable;

namespace MarvelPortablePlayground
{
    public partial class MainPage
    {
        private readonly IMarvelClient _client;

        // Constructor
        public MainPage()
        {
            InitializeComponent();
            _client = new MarvelClient("ec29a219e0defb6f73a726f9aa1413e0", "c1c321f48ea81fb194a5772803d00f561d4c9d5d");
        }

        private async void DoSomethingButton_OnClick(object sender, RoutedEventArgs e)
        {
            var response = await _client.GetStoriesForCharacterAsync(1009718);
        }
    }
}