using Hook.Model;
using System;
using Hook.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Security;

namespace Hook.View
{
    /// <summary>
    /// Логика взаимодействия для AutorWindow.xaml
    /// </summary>
    public partial class AutorWindow : Window
    {
        public AutorWindow()
        {
            InitializeComponent();
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            SecureString pasword = pas.SecurePassword;

            if (Log.Text != "" && pas.Password!=null)
            {
                using (HookContext db = new HookContext())
                {
                    User u1 = new User { Login = Log.Text, Password = pasword.ToString() };
                    db.Users.Add(u1);
                    await db.SaveChangesAsync();

                }

                
                MainWindow mw = new MainWindow();   
                mw.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите логин и пароль.");
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        { SecureString pasword= pas.SecurePassword;
            if (!string.IsNullOrEmpty(Log.Text) && !string.IsNullOrEmpty( pasword.ToString()))
            {
                using (HookContext db = new HookContext())
                {
                    // Важно! Используйте параметризованный запрос для предотвращения SQL-инъекций
                    string login = Log.Text;
                    string password =pasword.ToString(); // Возможно, нужно хешировать пароль для безопасности


                    var user = await db.Users.FirstOrDefaultAsync(u => u.Login == login);

                    if (user != null)
                    {
                        // Проверка пароля (важно: Хешируйте пароли!)
                        if (user.Password == password) // !!! НЕЛЬЗЯ сравнивать пароли напрямую!!!
                        {
                            MainWindow mw = new MainWindow();
                            mw.Show();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Неверный пароль.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пользователя с таким логином не существует.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите логин и пароль.");
            }

        }
    }
}
