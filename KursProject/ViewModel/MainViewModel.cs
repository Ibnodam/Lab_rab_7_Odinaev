using Hook;
using MaterialDesignThemes.Wpf;
using Sample.Model;
using Sample.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Sample.ViewModel
{
    public class MainViewModel
    {
        private MainWindow mainWindow;
        public MainViewModel(MainWindow mw)
        {
            mainWindow = mw;
        }
        private RelayCommand addCommand;
        private RelayCommand boatCommand;
        private RelayCommand visitCommand;
        private RelayCommand catchCommand;
        private RelayCommand spotCommand;
        private RelayCommand mainwindCommand;
        private RelayCommand requestCommand;
        private RelayCommand addBoatCommand;
        private RelayCommand r1command;
        private RelayCommand r2command;
        private RelayCommand r3command;
        private RelayCommand r4command;
        private RelayCommand r5command;
        private RelayCommand r6command;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      mainWindow.PageContainer.Navigate(new BankPage());
                  }));
            }
        }
        
        public RelayCommand BoatCommand
        {
            get
            {
                return boatCommand ??
                  (boatCommand = new RelayCommand(obj =>
                  {
                      mainWindow.PageContainer.Navigate(new BoatWindow());
                  }));
            }
        }
        public RelayCommand VisitCommand
        {
            get
            {
                return visitCommand ??
                  (visitCommand = new RelayCommand(obj =>
                  {
                      mainWindow.PageContainer.Navigate(new VisitWindow());
                  }));
            }
        }
        public RelayCommand SpotCommand
        {
            get
            {
                return spotCommand ??
                  (spotCommand = new RelayCommand(obj =>
                  {
                      mainWindow.PageContainer.Navigate(new SpotWindow());
                  }));
            }
        }
        public RelayCommand CatchCommand
        {
            get
            {
                return catchCommand ??
                  (catchCommand = new RelayCommand(obj =>
                  {
                      mainWindow.PageContainer.Navigate(new CatchWindow());
                  }));
            }
        }
        public RelayCommand R3Command
        {
            get
            {
                return r3command ??
                  (r3command = new RelayCommand(obj =>
                  {
                      mainWindow.PageContainer.Navigate(new R3Page());
                  }));
            }
        }
        public RelayCommand R5Command
        {
            get
            {
                return r5command ??
                  (r5command = new RelayCommand(obj =>
                  {
                      mainWindow.PageContainer.Navigate(new R5Page());
                  }));
            }
        }
        public RelayCommand R6Command
        {
            get
            {
                return r6command ??
                  (r6command = new RelayCommand(obj =>
                  {
                      mainWindow.PageContainer.Navigate(new R6Page());
                  }));
            }
        }
        public RelayCommand R4Command
        {
            get
            {
                return r4command ??
                  (r4command = new RelayCommand(obj =>
                  {
                      mainWindow.PageContainer.Navigate(new R4Page());
                  }));
            }
        }
        public RelayCommand R2Command
        {
            get
            {
                return r2command ??
                  (r2command = new RelayCommand(obj =>
                  {
                      mainWindow.PageContainer.Navigate(new R2Page());
                  }));
            }
        }
        public RelayCommand R1Command
        {
            get
            {
                return r1command ??
                  (r1command = new RelayCommand(obj =>
                  {
                      mainWindow.PageContainer.Navigate(new R1Page());
                  }));
            }
        }
    }
}
