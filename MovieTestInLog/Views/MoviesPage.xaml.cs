﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MovieTestInLog.Models;
using MovieTestInLog.Views;
using MovieTestInLog.ViewModels;

namespace MovieTestInLog.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MoviesPage 
    {
        private MoviesViewModel ViewModel => BindingContext as MoviesViewModel;

        public MoviesPage()
        {
            InitializeComponent();
           BindingContext = ViewModel;
        }

    }
}