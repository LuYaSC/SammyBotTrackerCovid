namespace TC.Core.JwtAuthServer.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public interface IBase<TypeKey>
    {
        TypeKey Id { get; set; }
    }

    public interface ILogicalDelete
    {
        bool IsDeleted { get; set; }
    }

    public class Base<TypeKey> : IBase<TypeKey>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual TypeKey Id { get; set; }
    }

    public class BaseLogicalDelete<TypeKey> : Base<TypeKey>, ILogicalDelete
    {
        public bool IsDeleted { get; set; }
    }

    public class BaseBackOfficeTrace<Typekey> : BaseLogicalDelete<Typekey>, IBackOfficeAuditable
    {
        [Required]
        [MaxLength(6)]
        public string UserCreation { get; set; }

        [MaxLength(6)]
        public string UserModification { get; set; }

        [Required]
        public DateTime DateCreation { get; set; }

        public DateTime? DateModification { get; set; }
    }

    public class BaseBackOfficeOnlyTrace<TypeKey> : Base<TypeKey>, IBackOfficeAuditable
    {
        [Required]
        [MaxLength(6)]
        public string UserCreation { get; set; }

        [MaxLength(6)]
        public string UserModification { get; set; }

        [Required]
        public DateTime DateCreation { get; set; }

        public DateTime? DateModification { get; set; }
    }

    public class Base : Base<int> { }

    public class BaseBackOfficeTrace : BaseBackOfficeTrace<int> { }
}