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
            AnimalsApi.AddAnimal(new Monkey(AnimalsApi.GetAnimalsOfType(new Monkey()).Count()));
            AnimalsApi.AddAnimal(new Lion(AnimalsApi.GetAnimalsOfType(new Lion()).Count()));
            AnimalsApi.AddAnimal(new Lion(AnimalsApi.GetAnimalsOfType(new Lion()).Count()));
            AnimalsApi.AddAnimal(new Lion(AnimalsApi.GetAnimalsOfType(new Lion()).Count()));
            var animals = AnimalsApi.GetAllAliveAnimals(null);
            var animals2 = AnimalsApi.GetAllAliveAnimals(new Monkey());

            Label.Content = animals.Count();
        }
    }
}