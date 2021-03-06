//using HumanResources.Models.Configurations;
//using HumanResources.Models.Domains;
using HumanResources.Properties;
using HumanResources.Views;
using HumanResources;
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;


namespace Diary
{
    public class ApplicationBbContext : DbContext
    {

        public static string ServerAddress   
        {
            get
            {
                return Settings.Default.ServerAddress;
            }
            set
            {
                Settings.Default.ServerAddress = value;
            }
        }

        public static string ServerName
        {
            get
            {
                return Settings.Default.ServerName;
            }
            set
            {
                Settings.Default.ServerName = value;
            }
        }

        public static string DataBase
        {
            get
            {
                return Settings.Default.Database;
            }
            set
            {
                Settings.Default.Database = value;
            }
        }


        public static string User
        {
            get
            {
                return Settings.Default.User;
            }
            set
            {
                Settings.Default.User = value;
            }
        }

        public static string Password
        {
            get
            {
                return Settings.Default.Password;
            }
            set
            {
                Settings.Default.Password = value;
            }
        }


        private static string ConnectionString
        {
            get
            {
                return $"Server={ServerAddress};Database={DataBase};Uid={User};Pwd={Password};";
            }
        }

        // do bazowego konstruktora mo�emy przekaza� konkretny ConnectionString
        // lub jego nazw� "name=ApplicationBbContext" 

        private static string _connectionString = DbHelper.ConnectionStringBuilder(ServerAddress, ServerName, DataBase, User, Password);
     
        public ApplicationBbContext() 
            : base(_connectionString)
        {
        }

        public ApplicationBbContext(string connectionString)
            : base(connectionString)
        {
        }

        /*
        // wskazujemy klasy domenowe
        // Entity Framework wie, �e ma stworzy� 3 tabele jak poni�ej
        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Rating> Ratings { get; set; }

        // musimy przes�oni� metod� OnModelCreating
        // aby wskaza�, �e mamy pliki konfiguracyjne
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new StudentConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
            modelBuilder.Configurations.Add(new RatingConfiguration());
        }
        */
        // private static string recoveryConnectionString2 = "Server=127.0.0.1;Database=Diary;Uid=user1;Pwd=alamakota;";

    }
}