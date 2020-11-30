using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Zoo.API.Animals;
using Zoo.Helper.Animals;
using Zoo.Models.Animals;

namespace Zoo
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int animalsDied;
        private int animalsCreated;
        public MainWindow()
        {
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(0.1)
            };
            timer.Tick += Tick;
            timer.Start();
        }

        private async void Tick(object sender, EventArgs e)
        {
            Random rnd = new Random();

            await AnimalsApi.InitAnimals();

            List<BaseAnimal> deadAnimals = await AnimalsApi.GetDeadAnimals(null);
            animalsDied += deadAnimals.Count();
            AnimalsDied.Text = animalsDied.ToString();
            await AnimalsApi.RemoveDeadAnimals(deadAnimals);
            List<BaseAnimal> animals = await AnimalsApi.GetAliveAnimals(null);
            await AnimalsApi.AnimalEnergyUser(animals);



            int rndNumber = rnd.Next(0, 99999);
            int animalTypeNumber = rnd.Next(0, 3);
            switch (animalTypeNumber)
            {
                case 0:
                    await AnimalsApi.AddAnimal(new Monkey(rndNumber));
                    break;
                case 1:
                    await AnimalsApi.AddAnimal(new Lion(rndNumber));
                    break;
                case 2:
                    await AnimalsApi.AddAnimal(new Elephant(rndNumber));
                    break;
                default:
                    break;
            }
            animalsCreated += 1;
            AnimalsCreated.Text = animalsCreated.ToString();

            AnimalCount.Text = animals.Count().ToString();

            int monkeyCount = (await AnimalsApi.GetAnimalsOfType(new Monkey())).Count();
            MonkeyCount.Text = monkeyCount.ToString();
            int lionCount = (await AnimalsApi.GetAnimalsOfType(new Lion())).Count();
            LionCount.Text = lionCount.ToString();
            int elephantCount = (await AnimalsApi.GetAnimalsOfType(new Elephant())).Count();
            ElephantCount.Text = elephantCount.ToString();
        }
    }
}