using Bibliotekaa;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Bibliotekaa
{
    public partial class MainWindow : Window
    {
        private static List<User> users = new List<User>()
        {
            new User(2,"Иосиф","Сталин"),
            new User(4,"Пиво", "Нефильтрованное"),
            new User(5,"Шойгу", "Где боеприпасы?"),
        };
        private static List<Book> books = new List<Book>()
        {
            new Book("Гарри Потный", 228, new DateOnly(2023,7,28),123),
            new Book("Для умных", 246, new DateOnly(2000,10,12),372),
            new Book("PlayBoy", 134, new DateOnly(2023,4,13),245),
        };
        public MainWindow()
        {
            InitializeComponent();

            ReadersList.ItemsSource = users;
            BooksList.ItemsSource = books;
            ReadersAddList.ItemsSource = users;
            BooksAddList.ItemsSource = books;
        }
        private void BooksList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Book? books = BooksList.SelectedItem as Book;
            if (books != null)
            {
                authorText.Text = Convert.ToString(books.Author);
                vendorCodeText.Text = Convert.ToString(books.VendorCode);
                yearText.Text = Convert.ToString(books.Year);
                amountText.Text = Convert.ToString(books.Amount);
            }
        }
        private void ReaderList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            User? selectedUser = ReadersList.SelectedItem as User;
            if (selectedUser != null)
            {
                idText.Text = Convert.ToString(selectedUser.Id);
                nameText.Text = Convert.ToString(selectedUser.Name);
                surnameText.Text = Convert.ToString(selectedUser.Surname);
                foreach (var item in selectedUser.Books)
                {
                    BooksText.Text = Convert.ToString(selectedUser.Books.Count);
                }
            }
        }
        private void AddBookToUser(object sender, RoutedEventArgs e)
        {
            User? selectedUser = ReadersAddList.SelectedItem as User;
            Book? selectedBook = BooksAddList.SelectedItem as Book;

            if (selectedUser == null || selectedBook == null)
            {
                MessageBox.Show("Не выбран пользователь или книжка");
                return;
            }
            if (selectedBook.Amount > 0)
            {
                selectedBook.Amount--;
                selectedUser.Books.Add(selectedBook);
                ReadersAddList.Items.Refresh();
                BooksAddList.Items.Refresh();
                ReadersList.Items.Refresh();
                BooksList.Items.Refresh();
                ReadersAddList.SelectedItem = null;
                BooksAddList.SelectedItem = null;
                MessageBox.Show("Книга выдана пользователю");
            }
            else MessageBox.Show("Кончилась книжка(");
        }
    }
}
