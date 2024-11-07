using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static QuanLyThuVien.Library;

namespace QuanLyThuVien
{

    //Dinh nghia class book
    public class Book
    {
        public string BookId { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public int PublishYear { get; set; }
        public string Publisher { get; set; }
        public bool Available { get; set; }
        public Book()
        {
            BookId = string.Empty;
            BookName = string.Empty;
            Author = string.Empty;
            PublishYear = 0;
            Publisher = string.Empty;
            Available = true;
        }
        public Book(string bookId, string bookName, string author, int publishYear, string publisher)
        {
            this.BookId = bookId;
            this.BookName = bookName;
            this.Author = author;
            this.PublishYear = publishYear;
            this.Publisher = publisher;
            Available = true;
        }
        public void DisplayInfo()
        {
            int frameWidth = 122; // Độ rộng của khung hình chữ nhật

            Console.WriteLine(new string('-', frameWidth)); // Đường kẻ trên cùng
            Console.WriteLine($"| {"Ma sach",-10} | {"Ten sach",-38} | {"Tac gia",-20} | {"Nha xuat ban",-20} | {"Nam xuat ban",-19} |");
            Console.WriteLine(new string('-', frameWidth));

            // Định dạng thông tin sách để in ra trong khung hình chữ nhật
            string formattedId = $"| {BookId,-10}";
            string formattedName = $"| {BookName,-38}";
            string formattedAuthor = $"| {Author,-20}";
            string formattedPublisher = $"| {Publisher,-20}";
            string formattedYear = $"|{PublishYear,-20} |";

            Console.WriteLine($"{formattedId} {formattedName} {formattedAuthor} {formattedPublisher} {formattedYear}");

            Console.WriteLine(new string('-', frameWidth)); // Đường kẻ dưới cùng
        }

    }
    //Dinh nghia class Reader
    public class Reader
    {
        public string ReaderId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }


        public Reader()
        {
            ReaderId = string.Empty;
            Name = string.Empty;
            Address = string.Empty;
            PhoneNumber = string.Empty;
        }
        public Reader(string readerId, string name, string address, string phoneNumber)
        {
            this.ReaderId = readerId;
            this.Name = name;
            this.Address = address;
            this.PhoneNumber = phoneNumber;
        }
        public void DisplayInfo()
        {
            int frameWidth = 123; // Độ rộng của khung hình chữ nhật

            Console.WriteLine(new string('-', frameWidth)); // Đường kẻ trên cùng
            Console.WriteLine($"| {"Ma doc gia",-10} | {"Ten doc gia",-35} | {"Dia chi",-40} | {"So dien thoai",-25} |");
            Console.WriteLine(new string('-', frameWidth));

            // Định dạng thông tin sách để in ra trong khung hình chữ nhật
            string formattedReaderId = $"| {ReaderId,-10}";
            string formattedName = $"| {Name,-35}";
            string formattedAddress = $"| {Address,-40}";
            string formattedPhone = $"| {PhoneNumber,-25} |";

            Console.WriteLine($"{formattedReaderId} {formattedName} {formattedAddress} {formattedPhone}");

            Console.WriteLine(new string('-', frameWidth)); // Đường kẻ dưới cùng
        }
    }
    //Dinh nghia class Library chua book va reader va chua cac thong tin muon sach va tra 
    internal class Library
    {
        private List<Book> books = new List<Book>();
        private List<Reader> readers = new List<Reader>();
        private List<Employee> employees = new List<Employee>();
        private List<BorrowingTransaction> borrowingtrans = new List<BorrowingTransaction>();
        //Doc tat ca du lieu tu file
        public void ReadDataFromFile()
        {
            books = ReadBooksFromFile("Books.txt");
            readers = ReadReadersFromFile("Readers.txt");
            employees = ReadEmployeesFromFile("Employees.txt");
            borrowingtrans = ReadBorrowingTransactionsFromFile("BorrowingTransactions.txt");
        }
        
        // Hàm ghi dữ liệu vào file
        public void SaveDataToFile()
        {
            SaveBooksToFile("Books.txt", books);
            SaveReadersToFile("Readers.txt", readers);
            SaveEmployeesToFile("Employees.txt", employees);
            SaveBorrowingTransactionsToFile("BorrowingTransactions.txt", borrowingtrans);
        }

        // Ghi thông tin sách vào file
        private void SaveBooksToFile(string fileName, List<Book> books)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    foreach (var book in books)
                    {
                        writer.WriteLine($"{book.BookId},{book.BookName},{book.Author},{book.Publisher},{book.PublishYear},{book.Available}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Loi: {ex.Message}");
            }
        }
        //ghi thong tin doc gia vao file
        private static void SaveReadersToFile(string fileName, List<Reader> readers)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    foreach (var reader in readers)
                    {
                        // Ghi từng thông tin chi tiết của độc giả vào tệp tin
                        writer.WriteLine($"{reader.ReaderId},{reader.Name},{reader.Address},{reader.PhoneNumber}");
                    }

                    Console.WriteLine("Du lieu doc gia da duoc luu thanh cong!.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Loi: {ex.Message}");
            }
        }
        //ghi thong tin nhan vien vao file
        private static void SaveEmployeesToFile(string fileName, List<Employee> employees)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    foreach (var employee in employees)
                    {
                        // Ghi từng thông tin chi tiết của nhân viên vào tệp tin
                        writer.WriteLine($"{employee.EmployeeId},{employee.EmployeeName},{employee.EmployeeAddress},{employee.EmployeePhoneNumber}");
                    }

                    Console.WriteLine("Du lieu nhan vien da duoc luu thanh cong!.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Loi: {ex.Message}");
            }
        }
       
        //Doc du lieu book
        private List<Book> ReadBooksFromFile(string fileName)
        {
            List<Book> books = new List<Book>();

            try
            {
                
                List<BorrowingTransaction> borrowingTransactions = ReadBorrowingTransactionsFromFile("BorrowingTransactions.txt");

                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 6)
                        {
                            Book book = new Book
                            {
                                BookId = parts[0],
                                BookName = parts[1],
                                Author = parts[2],
                                Publisher = parts[3],
                                PublishYear = int.Parse(parts[4]),
                                Available = bool.Parse(parts[5]) 
                            };     
                            if (!book.Available)
                            {
                                book.Available = !IsBookBorrowed(book.BookId, borrowingTransactions);
                            }

                            books.Add(book);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Loi: {ex.Message}");
            }

            return books;
        }
        //Doc du lieu file reader
        private List<Reader> ReadReadersFromFile(string fileName)
        {
            List<Reader> readers = new List<Reader>();
            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 4)
                        {
                                readers.Add(new Reader
                                {
                                    ReaderId = parts[0],
                                    Name = parts[1],
                                    Address = parts[2],
                                    PhoneNumber = parts[3]
                                });
                            }  
                        }
                    }
                }
            catch (Exception ex)
            {
                Console.WriteLine($"Loi: {ex.Message}");
            }

            return readers;
        }       
     
        //Doc du lieu tu file nhan vien
        private static List<Employee> ReadEmployeesFromFile(string fileName)
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length == 4)
                        {
                            employees.Add(new Employee
                            {
                                EmployeeId = parts[0],
                                EmployeeName = parts[1],
                                EmployeeAddress = parts[2],
                                EmployeePhoneNumber = parts[3]
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Loi: {ex.Message}");
            }

            return employees;
        }
        //ghi thong tin chi tiet muon tra sach
        private static void SaveBorrowingTransactionsToFile(string fileName, List<BorrowingTransaction> borrowingTransactions)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    foreach (var transaction in borrowingTransactions)
                    {
                        string returnDate = transaction.ReturnDate == DateTime.MinValue ? "null" : transaction.ReturnDate.ToString("yyyy-MM-dd");

                        // Ghi từng thông tin chi tiết của mượn trả vào tệp tin
                        writer.WriteLine($"{transaction.TransactionId},{transaction.BookId},{transaction.ReaderId},{transaction.BorrowDate},{transaction.DueDate},{returnDate},{transaction.EmployeeId},{transaction.EmployeeName}");
                    }

                    Console.WriteLine("Du lieu chi tiet muon tra da duoc luu thanh cong.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Loi: {ex.Message}");
            }
        }

        //doc du lieu chi tiet muon tra
        private static List<BorrowingTransaction> ReadBorrowingTransactionsFromFile(string fileName)
        {
            List<BorrowingTransaction> borrowingtrans = new List<BorrowingTransaction>();

            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');

                        // Kiểm tra xem dòng đọc được có đủ thông tin không
                        if (parts.Length == 8)
                        {
                            BorrowingTransaction transaction = new BorrowingTransaction
                            {
                                TransactionId = int.Parse(parts[0]),
                                BookId = parts[1],
                                ReaderId = parts[2],
                                BorrowDate = DateTime.Parse(parts[3]),
                                DueDate = DateTime.Parse(parts[4]),
                                ReturnDate = parts[5].ToLower() == "null" ? DateTime.MinValue : DateTime.Parse(parts[5]),
                                EmployeeId = parts[6],
                                EmployeeName = parts[7]
                            };

                            borrowingtrans.Add(transaction);
                        }
                        else
                        {
                            Console.WriteLine($"Dong khong hop le: {line}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Loi: {ex.Message}");
            }

            return borrowingtrans;
        }

        //cac ham tinh nang trong menu
        //Hàm thêm sách
        public void AddBook(Book book)
        {
            if (GetBookById(book.BookId) != null)
            {
                Console.WriteLine("Ma sach bi trung. Vui long chon ma sach khac!");
                return;
            }

            // Thêm sách vào danh sách thư viện và hiển thị thông báo
            books.Add(book);
            Console.WriteLine("Sach da duoc them thanh cong!!!");
        }

        public void RemoveBook(string bookIdToRemove)
        {
            // Tìm id sách cần xóa 
            Book bookToRemove = GetBookById(bookIdToRemove);

            if (bookToRemove != null)
            {
                // Xóa các giao dịch mượn sách của sách
                borrowingtrans.RemoveAll(t => t.BookId == bookIdToRemove);

                // Xóa sách
                books.Remove(bookToRemove);
                Console.WriteLine($"Da xoa sach '{bookToRemove.BookName}' thanh cong!");
                
            }
            else
            {
                Console.WriteLine("Khong tim thay sach voi ma sach: " + bookIdToRemove);
            }
        }
        public void EditInformationBook(string bookIdToEdit, string newTitle, string newAuthor, string newPublisher, int newYear)
        {
            // Kiểm tra xem sách có cùng ID sách cần cập nhật thông tin hay không?
            Book bookToEdit = GetBookById(bookIdToEdit);
            if (bookToEdit != null)
            {
                bookToEdit.BookName = newTitle;
                bookToEdit.Author = newAuthor;
                bookToEdit.Publisher = newPublisher;
                bookToEdit.PublishYear = newYear;
                Console.WriteLine($"Thong tin sach'{bookToEdit.BookName}' da duoc cap nhat thanh cong!");
            }
            else
            {
                Console.WriteLine("Khong tim thay sach voi ma sach : " + bookIdToEdit);
            }
        }
        
        public Book GetBookById(string bookId)
        {
            return books.FirstOrDefault(b => b.BookId.ToLower().Contains(bookId.ToLower()));
        }
        public void DisplayBooks()
        { //Hien thi sach co trong thu vien
            Console.WriteLine("Danh sach cac sach trong thu vien: ");
            Console.WriteLine("");
            foreach (var book in books)
            {
                book.DisplayInfo();
                Console.WriteLine();
            }
        }
        //tim sach dua vao id va ten doc gia
        public void SearchBook(string bookIdOrPublisherOrName)
        {
            Console.WriteLine("Chon cach tim kiem sach:");
            Console.WriteLine("[a]. Tim kiem sach theo ID");
            Console.WriteLine("[b]. Tim kiem sach theo nha xuat ban");
            Console.WriteLine("[c]. Tim kiem sach theo ten sach");
            Console.Write("Nhap lua chon: ");
            string searchOption = Console.ReadLine();

            switch (searchOption.ToLower())
            {
                case "a":
                    SearchBookById(bookIdOrPublisherOrName);
                    break;
                case "b":
                    SearchBookByPublisher(bookIdOrPublisherOrName);
                    break;
                case "c":
                    SearchBookByName(bookIdOrPublisherOrName);
                    break;
                default:
                    Console.WriteLine("Lua chon khong hop le!");
                    break;
            }
        }

        public List<Book> SearchBookByName(string bookName)
        {
            List<Book> result = books.FindAll(b => b.BookName.ToLower().Contains(bookName.ToLower()));

            if (result.Count > 0)
            {
                Console.WriteLine($"Ket qua tim kiem cac sach co ten chua: {bookName}");
                foreach (var book in result)
                {
                    book.DisplayInfo();
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine($"Khong tim thay sach voi ten chua: {bookName}");
            }

            return result;
        }
        public List<Book> SearchBookById(string bookId)
        {      //Tim thong tin sach dua vao ID
            List<Book> result = books.FindAll(b => b.BookId.ToLower().Contains(bookId.ToLower()));

            if (result.Count > 0)
            {
                Console.WriteLine($"Ket qua tim kiem sach co ma sach {bookId}: ");
                Console.WriteLine("");
                foreach (var book in result)
                {
                    book.DisplayInfo();
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine($"Khong tim thay sach voi ma sach: {bookId}");
            }

            return result;
        }
        public List<Book> SearchBookByPublisher(string publisher)
        {
            List<Book> result = books.FindAll(b => b.Publisher.ToLower().Contains(publisher.ToLower()));

            if (result.Count > 0)
            {
                Console.WriteLine($"Ket qua tim kiem cac sach co NXB la: {publisher}");
                foreach (var book in result)
                {
                    book.DisplayInfo();
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine($"Khong tim thay sach voi ten NXB la: {publisher}");
            }

            return result;
        }

        //Thong tin nhan vien

        public void AddEmployee(Employee employee)
        {
            // Kiểm tra xem mã nhân viên đã tồn tại chưa
            if (GetEmployeeById(employee.EmployeeId) != null)
            {
                Console.WriteLine("Ma nhan vien bi trung. Vui long chon ma nhan vien khac!");
                return;
            }

            // Thêm nhân viên vào danh sách và hiển thị thông báo
            employees.Add(employee);
            Console.WriteLine("Nhan vien da duoc them thanh cong!!!");
        }
        //Ham kiem tra xem ma nhan vien co ton tai khong
        public Employee GetEmployeeById(string employeeId)
        {
            return employees.FirstOrDefault(e => e.EmployeeId.ToLower().Contains(employeeId.ToLower()));
     

        }
        //Xoa nhan vien
        public void RemoveEmployee(string employeeIdToRemove)
        {
            // Tìm id nhân viên cần xóa
            Employee employeeToRemove = GetEmployeeById(employeeIdToRemove);

            if (employeeToRemove != null)
            {
                employees.Remove(employeeToRemove);
                Console.WriteLine($"Da xoa thong tin nhan vien'{employeeToRemove.EmployeeName}' thanh cong!");
            }
            else
            {
                Console.WriteLine("Khong tim thay thong tin nhan vien can xoa voi ma nhan vien: " + employeeIdToRemove);
            }
        }
        //Cap nhat 
        public void UpdateEmployee(string employeeIdToUpdate, string newEmployeeName, string newEmployeeAddress, string newEmployeePhoneNumber)
        {
            // Duyệt và tìm id cần cập nhật thông tin
            Employee employeeToUpdate = GetEmployeeById(employeeIdToUpdate);

            if (employeeToUpdate != null)
            {
                employeeToUpdate.EmployeeName = newEmployeeName;
                employeeToUpdate.EmployeeAddress = newEmployeeAddress;
                employeeToUpdate.EmployeePhoneNumber = newEmployeePhoneNumber;
                Console.WriteLine($"Thong tin nhan vien'{employeeToUpdate.EmployeeName}' da duoc cap nhat thanh cong!");
            }
            else
            {
                Console.WriteLine("Khong tim thay nhan vien voi ma nhan vien : " + employeeToUpdate);
            }
        }
        //Hien thi thong tin nhan vien
        public void DisplayEmployee()
        { //Hien thi sach co trong thu vien
            Console.WriteLine("Danh sach nhan vien trong thu vien: ");
            Console.WriteLine("");
            foreach (var employee in employees)
            {
                employee.DisplayInfo();
                Console.WriteLine();
            }
        }

        public void SearchEmployee(string employeeIdOrName)
        {
            Console.WriteLine("Chon cach tim kiem thong tin nhan vien:");
            Console.WriteLine("[a]. Tim kiem nhan vien theo ID");
            Console.WriteLine("[b]. Tim kiem nhan vien theo ten nhan vien");
            Console.Write("Nhap lua chon: ");
            string searchOption = Console.ReadLine();

            switch (searchOption.ToLower())
            {
                case "a":
                    SearchEmployeeById(employeeIdOrName);
                    break;
                case "b":
                    SearchEmployeeByName(employeeIdOrName);
                    break;
                default:
                    Console.WriteLine("Lua chon khong hop le!");
                    break;
            }
        }

        public List<Employee> SearchEmployeeByName(string employeeName)
        {
            List<Employee> result = employees.FindAll(e =>e.EmployeeName.ToLower().Contains(employeeName.ToLower()));

            if (result.Count > 0)
            {
                Console.WriteLine($"Ket qua tim kiem nhan vien co ten: {employeeName}");
                Console.WriteLine("");
                foreach (var employee in result)
                {
                    employee.DisplayInfo();
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine($"Khong tim thay nhan vien voi ten: {employeeName}");
            }

            return result;
        }

        public List<Employee> SearchEmployeeById(string employeeId)
        {
            List<Employee> result = employees.FindAll(e => e.EmployeeId.ToLower().Contains(employeeId.ToLower()));

            if (result.Count > 0)
            {
                Console.WriteLine($"Ket qua tim kiem nhan vien voi ma nhan vien {employeeId}: ");
                Console.WriteLine("");
                foreach (var employee in result)
                {
                    employee.DisplayInfo();
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine($"Khong tim thay nhan vien voi ma nhan vien: {employeeId}");
            }

            return result;
        }
        //Them doc gia vao danh sach cac doc gia
        public void AddReader(Reader reader)
        {
            // Kiểm tra xem mã độc giả đã tồn tại chưa
           
            if (GetReadersById(reader.ReaderId) != null)
            {
                Console.WriteLine("Ma doc gia bi trung. Vui long chon ma doc gia khac!");
                return;
            }

            // Thêm sách vào danh sách thư viện và hiển thị thông báo
            readers.Add(reader);
            Console.WriteLine("Da them doc gia thanh cong!!!");
        }
        public void RemoveReader(string readerIdToRemove)
        {
            // Kiểm tra xem độc giả có tồn tại không
            Reader readerToRemove = GetReadersById(readerIdToRemove);

            if (readerToRemove != null)
            {
                // Xóa các giao dịch mượn sách của độc giả
                borrowingtrans.RemoveAll(t => t.ReaderId == readerIdToRemove);

                // Xóa độc giả
                readers.Remove(readerToRemove);
                Console.WriteLine($"Da xoa doc gia '{readerToRemove.Name}' thanh cong!");
            }
            else
            {
                Console.WriteLine("Khong tim thay doc gia voi ma doc gia: " + readerIdToRemove);
            }
        }
        //Ham lay reader tu id
        public Reader GetReadersById(string readerId)
        {
            return readers.FirstOrDefault(r => r.ReaderId.ToLower().Contains(readerId.ToLower()));


        }
        //Ham hien thi danh sach doc gia
        public void DisplayReadersById(string readerId)
        {
            List<Reader> matchingReaders = readers.Where(r => r.ReaderId == readerId).ToList();

            if (matchingReaders.Count > 0)
            {
                Console.WriteLine($"Danh sach doc gia co ID {readerId}: ");
                for (int i = 0; i < matchingReaders.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. ");
                    matchingReaders[i].DisplayInfo();
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine($"Khong tim thay doc gia voi ID {readerId}");
            }
        }
        public void EditInformationReader(string readerIdToEdit, string newReaderName, string newReaderAddress, string newReaderPhoneNumber)
        {
            // Duyệt danh sách độc giả có cùng ID cần cập nhật
            Reader readerToEdit = GetReadersById(readerIdToEdit);

            if (readerToEdit != null)
            {
                readerToEdit.Name = newReaderName;
                readerToEdit.Address = newReaderAddress;
                readerToEdit.PhoneNumber = newReaderPhoneNumber;
                Console.WriteLine($"Thong tin doc gia '{readerToEdit.Name}' da duoc cap nhat thanh cong!");
            }
            else
            {
                Console.WriteLine("Khong tim thay doc gia voi ma doc gia : " + readerIdToEdit);
            }
        }
        //Ham hien thi danh sach cac doc gia
        public void DisplayReaders()
        {

            Console.WriteLine("Danh sach cac doc gia: ");
            foreach (var reader in readers)
            {
                reader.DisplayInfo();
                Console.WriteLine();
            }
        }

        public void SearchReader(string readerIdOrName)
        {
            Console.WriteLine("Chon cach tim kiem doc gia:");
            Console.WriteLine("[a]. Tim kiem doc gia theo ID");
            Console.WriteLine("[b]. Tim kiem doc gia theo ten doc gia");
            Console.Write("Nhap lua chon: ");
            string searchOption = Console.ReadLine();

            switch (searchOption.ToLower())
            {
                case "a":
                    SearchReaderById(readerIdOrName);
                    break;
                case "b":
                    SearchReaderByName(readerIdOrName);
                    break;
                default:
                    Console.WriteLine("Lua chon khong hop le!");
                    break;
            }
        }

        public List<Reader> SearchReaderByName(string readerName)
        {
            List<Reader> result = readers.FindAll(r => r.Name.ToLower().Contains(readerName.ToLower()));

            if (result.Count > 0)
            {
                Console.WriteLine($"Ket qua tim kiem doc gia co ten: {readerName}");
                Console.WriteLine("");
                foreach (var reader in result)
                {
                    reader.DisplayInfo();
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine($"Khong tim thay doc gia voi ten: {readerName}");
            }

            return result;
        }

        public List<Reader> SearchReaderById(string readerId)
        {
            List<Reader> result = readers.FindAll(r => r.ReaderId.ToLower().Contains(readerId.ToLower()));

            if (result.Count > 0)
            {
                Console.WriteLine($"Ket qua tim kiem doc gia voi ma doc gia {readerId}: ");
                Console.WriteLine("");
                foreach (var reader in result)
                {
                    reader.DisplayInfo();
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("");
                Console.WriteLine($"Khong tim thay doc gia voi ma doc gia: {readerId}");
            }

            return result;
        }
        // Cập nhật hàm BorrowBook
        public void BorrowBook(string bookId, string readerId, string returnDateInput, string employeeInfo)
        {
            // Kiểm tra xem sách có tồn tại không dựa trên bookId
            Book bookToBorrow = books.FirstOrDefault(b => b.BookId.Equals(bookId, StringComparison.OrdinalIgnoreCase));

            if (bookToBorrow == null)
            {
                Console.WriteLine($"Khong tim thay sach co ma sach: {bookId}");
                return;
            }

            // Kiểm tra xem độc giả có tồn tại không
            Reader readerBorrowing = GetReadersById(readerId);

            if (readerBorrowing == null)
            {
                Console.WriteLine($"Khong tim thay doc gia voi ma doc gia: {readerId}");
                return;
            }

            // Kiểm tra xem thông tin nhân viên có phải là mã nhân viên hay tên nhân viên
            Employee employeeLibrary = employees.FirstOrDefault(e => e.EmployeeId.ToLower().Contains(employeeInfo.ToLower()) || e.EmployeeName.ToLower().Contains(employeeInfo.ToLower()));
            if (employeeLibrary == null)
            {
                Console.WriteLine($"Khong tim thay nhan vien voi ma nhan vien hoac ten nhan vien: {employeeInfo}");
                return;
            }

            // Kiểm tra xem sách có sẵn để mượn không
            if (bookToBorrow.Available)
            {
                
                if (IsBookBorrowed(bookToBorrow.BookId, borrowingtrans))
                {
                    Console.WriteLine($"Doc gia '{readerBorrowing.Name}' da muon sach '{bookToBorrow.BookName}'. Khong the muon them!");
                    return;
                }

                // Chuyển đổi ngày nhập vào thành kiểu DateTime theo thứ tự ngày/tháng/năm
                if (DateTime.TryParseExact(returnDateInput, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dueDate))
                {
                    // Kiểm tra xem độc giả đã tồn tại trước khi thực hiện giao dịch mượn sách
                    if (borrowingtrans.Any(t => t.ReaderId == readerBorrowing.ReaderId && t.ReturnDate == DateTime.MinValue))
                    {
                        Console.WriteLine($"Doc gia '{readerBorrowing.Name}' da muon sach khac. Khong the muon them!");
                        return;
                    }

                    // Tạo giao dịch mượn sách
                    int transactionId = GenerateTransactionId();
                    DateTime borrowDate = DateTime.Now;

                    BorrowingTransaction borrowingTransaction = new BorrowingTransaction(transactionId, bookToBorrow.BookId, readerBorrowing.ReaderId, borrowDate, dueDate, DateTime.MinValue, employeeLibrary.EmployeeId, employeeLibrary.EmployeeName);
                    borrowingtrans.Add(borrowingTransaction);

                    // Đánh dấu sách đã được mượn
                    bookToBorrow.Available = false;

                    Console.WriteLine($"Muon sach thanh cong. Ma phieu muon sach: {transactionId}");
                }
                else
                {
                    Console.WriteLine("Ngay tra sach nhap khong hop le. Vui long nhap theo dinh dang dd/MM/yyyy.");
                }
            }
            else
            {
                Console.WriteLine($"Sach '{bookToBorrow.BookName}' da duoc muon boi doc gia khac!");
                return;
            }
        }
      
        private bool IsBookBorrowed(string bookId, List<BorrowingTransaction> borrowingTransactions)
        {
            return borrowingTransactions.Any(t => t.BookId == bookId && t.ReturnDate == DateTime.MinValue);
        }  
        private int GenerateTransactionId()
        {
            // Logic để sinh mã phiếu mượn, ghi nhận việc giao dịch
            return borrowingtrans.Count + 1;
        }
        //Hàm trả sách
        public void ReturnBook(string bookId, string readerId)
        {
            // Kiểm tra xem sách có tồn tại không
            Book bookToReturn = books.FirstOrDefault(b => b.BookId.Equals(bookId, StringComparison.OrdinalIgnoreCase));

            if (bookToReturn == null)
            {
                Console.WriteLine($"Khong tim thay sach co ma sach: {bookId}");
                return;
            }

            // Kiểm tra xem sách đã được mượn chưa
            if (bookToReturn.Available)
            {
                Console.WriteLine($"Sach '{bookToReturn.BookName}' chua duoc muon. Khong the tra.");
                return;
            }

            // Kiểm tra xem độc giả có tồn tại không
            Reader readerReturning = GetReadersById(readerId);

            if (readerReturning == null)
            {
                Console.WriteLine($"Khong tim thay doc gia voi ma doc gia: {readerId}");
                return;
            }

            // Kiểm tra xem độc giả đã mượn sách này chưa
            BorrowingTransaction borrowingTransaction = borrowingtrans.FirstOrDefault(t => t.BookId == bookToReturn.BookId && t.ReaderId == readerReturning.ReaderId && t.ReturnDate == DateTime.MinValue);

            if (borrowingTransaction == null)
            {
                Console.WriteLine($"Doc gia '{readerReturning.Name}' chua muon sach '{bookToReturn.BookName}'. Khong the tra.");
                return;
            }

            // Cập nhật thông tin trả sách
            borrowingTransaction.ReturnDate = DateTime.Now.Date;
            bookToReturn.Available = true;

            // Kiểm tra trả sách có trễ hạn hay không
            if (DateTime.Now.Date > borrowingTransaction.DueDate.Date)
            {
                // Sách đã trễ hạn, thực hiện các biện pháp phạt
                Console.WriteLine($"Tra sach tre han! Ma phieu muon: {borrowingTransaction.TransactionId}");
            }
            else
            {
                // Sách trả đúng hạn
                Console.WriteLine($"Tra sach thanh cong. Ma phieu muon: {borrowingTransaction.TransactionId}");
            }
        }
        public void ViewBorrowingTransaction()
        {
            int frameWidth = 155;
            Console.WriteLine("Danh sach thong tin cac giao dich: \n");
            Console.WriteLine(new string('-', frameWidth)); // Đường kẻ trên cùng
            Console.WriteLine($"| {"Ma phieu muon",-9} | {"Doc gia",-17} | {"Sach",-35} | {"Ngay muon",-13} | {"Ngay tra du kien",-14} |  {"Ngay tra",-15} |  {"Tinh trang",-23} |");
            Console.WriteLine(new string('-', frameWidth));

            foreach (var transaction in borrowingtrans)
            {
                Book borrowedBook = GetBookById(transaction.BookId);
                Reader borrowingReader = readers.FirstOrDefault(r => r.ReaderId == transaction.ReaderId);

                string formattedId = $"| {transaction.TransactionId,-13}";
                string formattedReader = $"| {borrowingReader.Name,-17}";
                string formattedBook = $"| {borrowedBook.BookName,-35}";
                string formattedBorrowDate = $"| {transaction.BorrowDate.ToString("dd/MM/yyyy"),-13}";
                string formattedDueDate = $"| {transaction.DueDate.ToString("dd/MM/yyyy"),-16} |";
                string formattedReturnDate = transaction.ReturnDate != DateTime.MinValue
                    ? $" {transaction.ReturnDate.ToString("dd/MM/yyyy"),-15}"
                    : "";

                string status;
                if (transaction.ReturnDate == DateTime.MinValue)
                {
                    DateTime currentDate = DateTime.Now.Date; // Lưu giữ ngày hiện tại
                    if (currentDate > transaction.DueDate.Date)
                    {
                        status = "                | Da qua han tra sach!     |";
                    }
                    else
                    {
                        status = "                | Chua tra sach!           |";
                    }
                }
                else
                {
                    status = $"| Da tra sach dung han!    |";
                }

                Console.WriteLine($"{formattedId} {formattedReader} {formattedBook} {formattedBorrowDate} {formattedDueDate} {formattedReturnDate} {status}");
            }

            if (borrowingtrans.Count == 0)
            {
                Console.WriteLine("| Khong co giao dich muon sach nao.                                                                                                                                    |");
            }

            Console.WriteLine(new string('-', frameWidth)); // Đường kẻ dưới cùng
            Console.WriteLine("(===Nhan phim Enter de quay ve lua chon o menu===) ");
            Console.ReadLine();
        }

        //Dinh nghia class Nhan vien
        public class Employee
        {
            public string EmployeeId { get; set; }
            public string EmployeeName { get; set; }
            public string EmployeeAddress { get; set; }
            public string EmployeePhoneNumber { get; set; }
            public Employee()
            {
                EmployeeId = string.Empty;
                EmployeeName = string.Empty;
                EmployeeAddress = string.Empty;
                EmployeePhoneNumber = string.Empty;

            }
            public Employee(string employeeId, string employeeName, string employeeAddress, string employeePhoneNumber)
            {
                this.EmployeeId = employeeId;
                this.EmployeeName = employeeName;
                this.EmployeeAddress = employeeAddress;
                this.EmployeePhoneNumber = employeePhoneNumber;
            }
            public void DisplayInfo()
            {
                int frameWidth = 115; // Độ rộng của khung hình chữ nhật

                Console.WriteLine(new string('-', frameWidth)); // Đường kẻ trên cùng
                Console.WriteLine($"| {"Ma nhan vien",-10} | {"Ten nhan vien",-30} | {"Dia chi",-35} | {"So dien thoai",-25} |");
                Console.WriteLine(new string('-', frameWidth));

                // Định dạng thông tin sách để in ra trong khung hình chữ nhật
                string formattedEmployeeId = $"| {EmployeeId,-12}";
                string formattedName = $"| {EmployeeName,-30}";
                string formattedAddress = $"| {EmployeeAddress,-35}";
                string formattedPhone = $"| {EmployeePhoneNumber,-25} |";

                Console.WriteLine($"{formattedEmployeeId} {formattedName} {formattedAddress} {formattedPhone}");

                Console.WriteLine(new string('-', frameWidth)); // Đường kẻ dưới cùng
            }
        }
        //Dinh nghia class quan ly giao dich giua viec muon va tra sach
        public class BorrowingTransaction
        {
            public int TransactionId { get; set; }
            public string BookId { get; set; }
            public string ReaderId { get; set; }
            public DateTime BorrowDate { get; set; }
            public DateTime DueDate { get; set; }
            public DateTime ReturnDate { get; set; }
            public string EmployeeId { get; set; }
            public string EmployeeName { get; set; }

            public BorrowingTransaction()
            {
                TransactionId = 0;
                BookId = string.Empty;
                ReaderId = string.Empty;
                BorrowDate = DateTime.Now;
                DueDate = DateTime.Now;
                ReturnDate = DateTime.Now;
                EmployeeId = string.Empty;
                EmployeeName = string.Empty;
            }
            public BorrowingTransaction(int transactionId, string bookId, string readerId, DateTime borrowDate, DateTime dueDate, DateTime returnDate, string employeeId, string employeeName)
            {
                this.TransactionId = transactionId;
                this.BookId = bookId;
                this.ReaderId = readerId;
                this.BorrowDate = borrowDate;
                this.DueDate = dueDate;
                this.ReturnDate = returnDate;
                this.EmployeeId = employeeId;
                this.EmployeeName = employeeName;
            }

        }
        class Program
        {
            static void Main(string[] args)
            {
                Library library = new Library();
                // Đọc dữ liệu từ file khi chương trình khởi động
                library.ReadDataFromFile();

                while (true)
                {
                    Console.Clear();
                    Console.WriteLine("---------MENU Quan Ly Thu Vien---------");
                    Console.WriteLine("");
                    Console.WriteLine("Cac tinh nang cua phan mem quan ly:");
                    Console.WriteLine("");
                    Console.WriteLine("1.Them sach");
                    Console.WriteLine("2.Xoa sach");
                    Console.WriteLine("3.Cap nhat thong tin sach");
                    Console.WriteLine("4.Tim kiem sach");
                    Console.WriteLine("5.Hien thi thong tin sach");
                    Console.WriteLine("6.Lap phieu muon sach");
                    Console.WriteLine("7.Tra sach");
                    Console.WriteLine("8.Dang ky tai khoan doc gia");
                    Console.WriteLine("9.Xoa thong tin doc gia");
                    Console.WriteLine("10.Cap nhat thong tin doc gia");
                    Console.WriteLine("11.Tim kiem thong tin cua doc gia");
                    Console.WriteLine("12.Hien thi thong tin doc gia");
                    Console.WriteLine("13.Xem chi tiet muon tra sach");
                    Console.WriteLine("14.Cap nhat thong tin nhan vien thu vien");
                    Console.WriteLine("");
                    Console.WriteLine("");
                    Console.WriteLine($"So luong sach trong thu vien: {library.books.Count}");
                    Console.WriteLine($"So luong doc gia: {library.readers.Count}");
                    Console.WriteLine($"So luong nhan vien: {library.employees.Count}");
                    Console.WriteLine("");
                    Console.WriteLine("-------Nhap vao lua chon----------------");
                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            Console.Write("Nhap ma sach: ");
                            string bookId = Console.ReadLine();
                            Console.Write("Nhap ten sach: ");
                            string title = Console.ReadLine();
                            Console.Write("Nhap ten tac gia: ");
                            string author = Console.ReadLine();
                            Console.Write("Nhap ten nha xuat ban: ");
                            string publisher = Console.ReadLine();
                            Console.Write("Nhap nam xuat ban: ");
                            int publishYear = int.Parse(Console.ReadLine());
                            library.AddBook(new Book { BookId = bookId, BookName = title, Author = author, Publisher = publisher, PublishYear = publishYear });
                            Console.WriteLine("(===Nhan phim Enter de quay ve lua chon o menu===) ");
                            Console.ReadLine();
                            break;
                        case "2":
                            library.DisplayBooks();
                            Console.Write("Nhap ID sach can xoa: ");
                            string bookIdToRemove = Console.ReadLine();
                            library.RemoveBook(bookIdToRemove);
                            Console.WriteLine("(===Nhan phim Enter de quay ve lua chon o menu===) ");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case "3":
                            library.DisplayBooks();
                            Console.Write("Nhap ID sach can sua: ");
                            string bookIdToEdit = Console.ReadLine();
                            Console.Write("Nhap ten sach moi: ");
                            string newTitle = Console.ReadLine();
                            Console.Write("Nhap ten tac gia moi: ");
                            string newAuthor = Console.ReadLine();
                            Console.Write("Nhap ten nha xuat ban moi: ");
                            string newPublisher = Console.ReadLine();
                            Console.Write("Nhap nam xuat ban moi: ");
                            int newYear = int.Parse(Console.ReadLine());
                            library.EditInformationBook(bookIdToEdit, newTitle, newAuthor, newPublisher, newYear);
                            Console.WriteLine("(===Nhan phim Enter de quay ve lua chon o menu===) ");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case "4":
                            library.DisplayBooks();
                            Console.Write("Nhap ID, nha xuat ban hoac ten sach can tim: ");
                            string bookIdOrPublisherOrName = Console.ReadLine();
                            library.SearchBook(bookIdOrPublisherOrName);
                            Console.WriteLine("(===Nhan phim Enter de quay ve lua chon o menu===) ");
                            Console.ReadLine();
                            break;
                        case "5": //ham hien thi sach
                            library.DisplayBooks();
                            Console.WriteLine("(===Nhan phim Enter de quay ve lua chon o menu===) ");
                            Console.ReadLine();
                            break;
                        case "6": // Lập phiếu mượn sách
                            library.DisplayBooks();
                            Console.WriteLine("");
                            Console.Write("Nhap ma sach can muon: ");
                            string bookToBorrow = Console.ReadLine();
                            Console.Write("Nhap ma doc gia muon sach: ");
                            string readerIdToBorrow = Console.ReadLine();
                            Console.Write("Nhap ngay hen tra sach (dd/mm/yyyy): ");
                            string returnDateInput = Console.ReadLine();
                            Console.Write("Nhap ma hoac ten nhan vien thuc hien viec lap phieu: ");
                            string employeeInfo = Console.ReadLine();
                            library.BorrowBook(bookToBorrow, readerIdToBorrow, returnDateInput, employeeInfo);
                            Console.WriteLine("(===Nhan phim Enter de quay ve lua chon o menu===) ");
                            Console.ReadLine();
                            break;
                        case "7": // Trả sách
                            library.DisplayBooks();
                            Console.Write("Nhap ma sach can tra: ");
                            string bookToReturn = Console.ReadLine();
                            Console.Write("Nhap ma doc gia tra sach: ");
                            string readerIdToReturn = Console.ReadLine();
                            library.ReturnBook(bookToReturn, readerIdToReturn);
                            Console.WriteLine("(===Nhan phim Enter de quay ve lua chon o menu===) ");
                            Console.ReadLine();
                            break;
                        case "8": // Ham them doc gia
                            Console.Write("Nhap ma doc gia: ");
                            string readerId = Console.ReadLine();
                            Console.Write("Nhap ten doc gia: ");
                            string readerName = Console.ReadLine();
                            Console.Write("Nhap dia chi: ");
                            string readerAddress = Console.ReadLine();
                            Console.Write("Nhap so dien thoai: ");
                            string readerPhoneNumber = Console.ReadLine();
                            library.AddReader(new Reader { ReaderId = readerId, Name = readerName, Address = readerAddress, PhoneNumber = readerPhoneNumber });
                            Console.WriteLine("(===Nhan phim Enter de quay ve lua chon o menu===) ");
                            Console.ReadLine();
                            break;
                        case "9":
                            library.DisplayReaders();
                            Console.Write("Nhap ID doc gia can xoa: ");
                            string readerIdToRemove = Console.ReadLine();
                            library.RemoveReader(readerIdToRemove);
                            Console.WriteLine("(===Nhan phim Enter de quay ve lua chon o menu===) ");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case "10":
                            library.DisplayReaders();
                            Console.Write("Nhap ID doc gia can sua: ");
                            string readerIdToEdit = Console.ReadLine();
                            Console.Write("Nhap ten doc gia moi: ");
                            string newReaderName = Console.ReadLine();
                            Console.Write("Nhap dia chi moi: ");
                            string newReaderAddress = Console.ReadLine();
                            Console.Write("Nhap so dien thoai moi: ");
                            string newReaderPhoneNumber = Console.ReadLine();
                            library.EditInformationReader(readerIdToEdit, newReaderName, newReaderAddress, newReaderPhoneNumber);
                            Console.WriteLine("(===Nhan phim Enter de quay ve lua chon o menu===) ");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        case "11": // Ham tim kiem thong tin doc gia
                            library.DisplayReaders();
                            Console.Write("Nhap ID hoac ten doc gia can tim: ");
                            string readerIdOrName = Console.ReadLine();
                            library.SearchReader(readerIdOrName);
                            Console.WriteLine("(===Nhan phim Enter de quay ve lua chon o menu===) ");
                            Console.ReadLine();
                            break;
                        case "12": // ham hien thi thong tin doc gia
                            library.DisplayReaders();
                            Console.WriteLine("(===Nhan phim Enter de quay ve lua chon o menu===) ");
                            Console.ReadLine();
                            break;
                        case "13": // Xem thông tin mượn sách
                            library.ViewBorrowingTransaction();
                            break;
                        case "14"://Cac ham chuc nang ve thong tin nhan vien ca lam 
                            Console.WriteLine("Chon chuc nang cua Nhan Vien Thu Vien:");
                            Console.WriteLine("");
                            Console.WriteLine("[a]. Nhap thong tin nhan vien");
                            Console.WriteLine("[b]. Xoa thong tin nhan vien");
                            Console.WriteLine("[c]. Sua thong tin nhan vien");
                            Console.WriteLine("[d]. Hien thi thong tin nhan vien");
                            Console.WriteLine("[e].Tim kiem thong tin nhan vien");
                            Console.Write("Nhap lua chon cua ban: ");
                            string employeeOption = Console.ReadLine();
                            switch (employeeOption)
                            {
                                case "a":
                                    // Thêm nhân viên
                                    Console.Write("Nhap ma nhan vien: ");
                                    string newEmployeeId = Console.ReadLine();
                                    Console.Write("Nhap ten nhan vien: ");
                                    string newEmployeeName = Console.ReadLine();
                                    Console.Write("Nhap dia chi: ");
                                    string newEmployeeAddress = Console.ReadLine();
                                    Console.Write("Nhap so dien thoai: ");
                                    string newEmployeePhoneNumber = Console.ReadLine();
                                    library.AddEmployee(new Employee(newEmployeeId, newEmployeeName, newEmployeeAddress, newEmployeePhoneNumber));
                                    Console.WriteLine("(===Nhan phim Enter de quay ve lua chon o menu===) ");
                                    Console.ReadLine();
                                    break;
                                case "b":
                                    // Xóa nhân viên
                                    library.DisplayEmployee();
                                    Console.Write("Nhap ma nhan vien can xoa: ");
                                    string employeeIdToRemove = Console.ReadLine();
                                    library.RemoveEmployee(employeeIdToRemove);
                                    Console.WriteLine("(===Nhan phim Enter de quay ve lua chon o menu===) ");
                                    Console.ReadLine();
                                    Console.Clear();
                                    break;
                                case "c":
                                    // Cập nhật thông tin nhân viên
                                    library.DisplayEmployee();
                                    Console.Write("Nhap ma nhan vien can sua thong tin: ");
                                    string employeeIdToUpdate = Console.ReadLine();
                                    Console.Write("Nhap ten nhan vien moi: ");
                                    string newEmployeeNameToUpdate = Console.ReadLine();
                                    Console.Write("Nhap dia chi moi: ");
                                    string newEmployeeAddressToUpdate = Console.ReadLine();
                                    Console.Write("Nhap so dien thoai moi: ");
                                    string newEmployeePhoneNumberToUpdate = Console.ReadLine();
                                    library.UpdateEmployee(employeeIdToUpdate, newEmployeeNameToUpdate, newEmployeeAddressToUpdate, newEmployeePhoneNumberToUpdate);
                                    Console.WriteLine("(===Nhan phim Enter de quay ve lua chon o menu===) ");
                                    Console.ReadLine();
                                    Console.Clear();
                                    break;
                                case "d":
                                    // Hiển thị thông tin nhân viên
                                    library.DisplayEmployee();
                                    Console.WriteLine("(===Nhan phim Enter de quay ve lua chon o menu===) ");
                                    Console.ReadLine();
                                    Console.Clear();
                                    break;
                                case "e":
                                    //Tim kiem thong tin nhan vien
                                    library.DisplayEmployee();
                                    Console.Write("Nhap ID hoac ten nhan vien can tim: ");
                                    string employeeIdOrName = Console.ReadLine();
                                    library.SearchEmployee(employeeIdOrName);
                                    Console.WriteLine("(===Nhan phim Enter de quay ve lua chon o menu===) ");
                                    Console.ReadLine();
                                    break;
                                default:
                                    Console.WriteLine("Lua chon khong hop le.");
                                    Console.WriteLine("(===Nhan phim Enter de quay ve lua chon o menu===) ");
                                    Console.ReadLine();
                                    break;
                            }
                            break;
                        default:
                            Console.WriteLine("Lua chon cua ban khong co trong menu!Loi!");
                            Console.WriteLine("(===Nhan phim Enter de quay ve lua chon o menu===) ");
                            Console.ReadLine();
                            break;
                    }
                    // Lưu trữ dữ liệu vào file trước khi thoát chương trình
                    library.SaveDataToFile();

                }

            }
        }
    }
}