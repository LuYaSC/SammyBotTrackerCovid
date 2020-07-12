namespace SBTC.Functions.Patients.Data
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public interface IBase<TypeKey>
    {
        TypeKey Id { get; set; }
    }

    public class Base : Base<int> { }

    public class Base<TypeKey> : IBase<TypeKey>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual TypeKey Id { get; set; }
    }

    public class BaseLogicalDelete : BaseLogicalDelete<int> { }

    public class BaseLogicalDelete<TypeKey> : Base<TypeKey>, ILogicalDelete
    {
        public bool IsDeleted { get; set; }
    }
}