using System.Linq;
using System.Windows;
using Zoo.API.Animals;
using Zoo.Models.Animals;

namespace Zoo
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AnimalsApi.InitAnimals();
        }

        private void Label_Loaded(object sender, RoutedEventArgs e)
        {
            AnimalsApi.AddAnimal(new Lion(AnimalsApi.GetAnimalsOfType(new Lion()).Count()));
            AnimalsApi.AddAnimal(new Lion(AnimalsApi.GetAnimalsOfType(new Lion()).Count()));
            AnimalsApi.AddAnimal(new Lion(AnimalsApi.GetAnimalsOfType(new Lion()).Count()));
            AnimalsApi.AddAnimal(new Lion(AnimalsApi.GetAnimalsOfType(new Lion()).Count()));
            AnimalsApi.AddAnimal(new Lion(AnimalsApi.GetAnimalsOfType(new Lion()).Count()));
            var animals = AnimalsApi.GetAnimals().Result;
            Label.Content = animals.Count();
        }
    }
}