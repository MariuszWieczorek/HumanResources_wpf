using HumanResources.Models.Domains;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace HumanResources.Models.Configurations
{
    public class DepartmentConfiguration : EntityTypeConfiguration<Department>
    {
        public DepartmentConfiguration()
        {
            ToTable("dbo.Departments");
            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            
            Property(x => x.Name)
                .HasMaxLength(20)
                .IsRequired();
        }

    }
}
