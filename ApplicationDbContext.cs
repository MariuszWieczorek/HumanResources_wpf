using HumanResources.Models.Configurations;
using HumanResources.Models.Domains;
using HumanResources.Properties;
using System;
using System.Data.Entity;
using System.Linq;

namespace HumanResources
{
    public class ApplicationDbContext : DbContext
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

        // do bazowego konstruktora mo¿emy przekazaæ konkretny ConnectionString
        // lub jego nazwê "name=ApplicationDbContext" 

        private static string _connectionString = DbHelper.ConnectionStringBuilder(ServerAddress, ServerName, DataBase, User, Password);

        public ApplicationDbContext()
            : base(_connectionString)
        {
        }

        public ApplicationDbContext(string connectionString)
            : base(connectionString)
        {
        }


        // wskazujemy klasy domenowe
        // Entity Framework wie, ¿e ma stworzyæ 3 tabele jak poni¿ej
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        // musimy przes³oniæ metodê OnModelCreating
        // aby wskazaæ, ¿e mamy pliki konfiguracyjne
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
            modelBuilder.Configurations.Add(new DepartmentConfiguration());
        }


    }


}