﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
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
using Zoo.API.Animals;
using Zoo.Constants.Animals;
using Zoo.Models.Animals;
using Path = System.IO.Path;

namespace Zoo
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

        private void Label_Loaded(object sender, RoutedEventArgs e)
        {
            AnimalsApi.InitAnimals();
            AnimalsApi.AddAnimal(new Lion());
            List<BaseAnimal> animals = AnimalsApi.GetAnimals();
        }
    }
}
