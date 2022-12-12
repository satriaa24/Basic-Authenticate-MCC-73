using System;
using System.ComponentModel.Design;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security.Cryptography.X509Certificates;

namespace Basic_Authenticate
{
    class Program
    {
        static int[] ID = { };
        static string[] FirstName = { };
        static string[] LastName = { };
        static string[] UserName = { };
        static string[] Password = { };
        static int id = ID.Length;

        public static void Main(string[] args)
        {
            tampil();
            Console.ReadKey();
        }

        static void tampil()
        {
            Console.Clear();
            Console.WriteLine("==BASIC AUTHENTICATION==");
            Console.WriteLine();
            Console.WriteLine("1. Create User ");
            Console.WriteLine("-------------------");
            Console.WriteLine("2. Show User");
            Console.WriteLine("-------------------");
            Console.WriteLine("3. Search User");
            Console.WriteLine("-------------------");
            Console.WriteLine("4. Login User");
            Console.WriteLine("-------------------");
            Console.WriteLine("5. Exit");
            Console.WriteLine();
            Console.Write("Input : ");
            

            switch (Convert.ToInt32(Console.ReadLine()))
            {
                case 1:
                    Create();
                    break;
                case 2:
                    Show();
                    break;
                case 3:
                    search();
                    break;
                case 4:
                    login();
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Input Not Valid");
                    Console.ReadKey();
                    break;
            }

            //method create
            static void Create()
            {
                Console.Clear();
                Console.Write("First Name: ");
                string firstname = Console.ReadLine();
                Console.Write("Last Name: ");
                string lastName = Console.ReadLine();
                Console.Write("Password: ");
                string password = Console.ReadLine();
                CekPassword(password); //Fungsi untuk cek password
                User(firstname, lastName, password); //FUNGSI UNTUK Create User
                Console.WriteLine();
                Console.WriteLine("Data user berhasil dibuat");
                Console.ReadKey();
                tampil();
            }

            //USER
            static void User(string firstName, string lastName, string password)
            {
                id = id + 1;
                ID = ID.Append(id).ToArray();
                FirstName = FirstName.Append(firstName).ToArray();
                LastName = LastName.Append(lastName).ToArray();
                UserName = UserName.Append(firstName.Substring(0, 2) + lastName.Substring(0, 2)).ToArray();
                Password = Password.Append(password).ToArray();
            }

            //CEK PASSWORD
            static string CekPassword(string password)
            {
                while (true)
                {
                    bool flag = true;
                    if ((password.Length > 7) && (Enumerable.Any<char>((IEnumerable<char>)password, new Func<char, bool>(char.IsUpper)) && (Enumerable.Any<char>((IEnumerable<char>)password, new Func<char, bool>(char.IsLower)) && Enumerable.Any<char>((IEnumerable<char>)password, new Func<char, bool>(char.IsNumber)))))
                    {
                        flag = false;
                    }
                    else
                    {
                        Console.WriteLine("\nPassword must have at least 8 characters\n with at least one Capital letter, at least one lower case letter and at least one number.");
                        Console.Write("Password: ");
                        password = Console.ReadLine();
                        flag = true;
                    }
                    if (!flag)
                    {
                        return password;
                    }
                }
            }

            //method show user
            static void Show()
            {
                Console.Clear();
                Console.WriteLine("==SHOW USER==");
                for (int i = 0; i < ID.Length; i++)
                {
                    Console.WriteLine("========================");
                    Console.WriteLine($"ID	: " + (i + 1));
                    Console.WriteLine("Name\t: " + FirstName[i] + " " + LastName[i]);
                    Console.WriteLine("Username: " + UserName[i]);
                    Console.WriteLine("Password: " + Password[i]);
                    Console.WriteLine("========================");

                }
                Console.WriteLine("Menu");
                Console.WriteLine("1. Edit User");
                Console.WriteLine("2. Delete User");
                Console.WriteLine("3. Back");
                Console.Write("Input : ");

                //MENU 1-2-3
                switch (Convert.ToInt32(Console.ReadLine()))
                {
                    case 1:
                        bool flag = false;
                        while (true)
                        {
                            Console.Write("Id Yang Ingin Diubah : ");
                            int edit_id = Convert.ToInt32(Console.ReadLine());

                            //decision untuk untuk cek id
                            if (edit_id > ID.Length)
                            {
                                Console.WriteLine("User Tidak Ditemukan!!!");
                                flag = true;
                            }
                            else
                            {
                                Console.Write("First Name : ");
                                FirstName[edit_id - 1] = Console.ReadLine();
                                Console.Write("Last Name : ");
                                LastName[edit_id - 1] = Console.ReadLine();
                                Console.Write("Password : ");
                                Password[edit_id - 1] = Console.ReadLine();
                                Console.Write("Data User berhasil di ubah");
                                flag = false;
                            }
                            if (!flag)
                            {
                                Console.ReadKey();
                                Show();
                                return;
                            }
                        }

                    case 2:
                        Console.Write("Id Yang Ingin Dihapus : "); //DELETE
                        int delete_id = Convert.ToInt32(Console.ReadLine());
                        if (delete_id > ID.Length)
                        {
                            Console.WriteLine("Data User Tidak Ditemukan");
                            Console.ReadKey();
                            Show();
                        }

                        //pengecekan id yg mau dihapus
                        int numToRemove = delete_id;
                        int numIndex = Array.IndexOf(ID, numToRemove);
                        ID = ID.Where((val, index) => index != numIndex).ToArray();
                        //numIndex = Array.IndexOf(FirstName, numToRemove);
                        FirstName = FirstName.Where((val, index) => index != numIndex).ToArray();
                        //numIndex = Array.IndexOf(LastName, numToRemove);
                        LastName = LastName.Where((val, index) => index != numIndex).ToArray();
                        //numIndex = Array.IndexOf(Password, numToRemove);
                        Password = Password.Where((val, index) => index != numIndex).ToArray();

                        Console.WriteLine("Data User Berhasil Dihapus");
                        Console.ReadKey();
                        Show();
                        return;

                    case 3:
                        tampil();
                        return;
                }
                Console.WriteLine("ERROR : Input Not Valid");
                Console.ReadKey();
                Show();
            }

            // method search
            static void search()
            {
                Console.Clear();
                Console.WriteLine("==== Cari Akun ====");
                Console.Write("Masukan ID : ");
                var name = int.Parse(Console.ReadLine());
                
                //tinggal buat searchnya
                bool contains = false;
                for (int i = 0; i < ID.Length; i++)
                {
                    if (name == ID[i])
                    {
                        contains = true;
                        Console.WriteLine(" ===================== ");
                        Console.WriteLine("Tampilkan Data: ");
                        Console.WriteLine("Nomor id     : " + id);
                        Console.WriteLine("Nama         : " + FirstName[i] + " " + LastName[i]);
                        Console.WriteLine("UserName     : " + UserName[i]);
                        Console.WriteLine("Password     : " + Password[i]);
                        Console.WriteLine(" ===================== ");
                        Console.WriteLine("Silahkan Tekan Enter untuk kembali");
                        Console.ReadKey();
                        tampil();
                    }
                    else
                    {
                        Console.WriteLine("Tidak Ada Data Yang Dapat Ditampilkan");
                        Console.ReadKey();
                        tampil();
                    }
                }
                Console.ReadKey();
                tampil();
            }

            //method login
            static void login()
            {
                Console.Clear();
                string coba_username;
                string coba_password;
                int index_username;
                int index_password;

                Console.WriteLine(" == LOGIN == ");
                Console.Write("Masukan username anda : ");
                string input_username = Console.ReadLine();
                Console.Write("Masukan password anda : ");
                string input_password = Console.ReadLine();

                coba_username = Array.Find(UserName, n => n.Contains(input_username)); //cari 
                index_username = Array.IndexOf(UserName, coba_username); //cek index username

                coba_password = Array.Find(Password, n => n.Contains(input_password));
                index_password = Array.IndexOf(Password, coba_password); //cek index password

                if (index_username == index_password)
                {
                    Console.WriteLine("Login Telah Berhasil");
                }
                else
                {
                    Console.WriteLine("Login Anda Gagal");
                }
            }
        }
    }
}