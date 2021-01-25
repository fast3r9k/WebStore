using System.ComponentModel.DataAnnotations;

namespace WebStore.Domain.ViewModels
{
    public class EmployeesViewModel
    {
        [Required]
        public int Id { get; set; }
        /// <summary>Имя</summary>
        [Required(AllowEmptyStrings = false,
                  ErrorMessage = "First name is required field")]
        [StringLength(maximumLength: 20, MinimumLength = 2, ErrorMessage = "First name must have at least 2 and no more than 20 chars")]
        public string FirstName { get; set; }
        /// <summary>Фамилия</summary>
        [Required(AllowEmptyStrings = false,
          ErrorMessage = "Last name is required field")]
        public string LastName { get; set; }
        /// <summary>Отчество</summary>
        public string Patronymic { get; set; }
        /// <summary>Возраст</summary>
        [Required(AllowEmptyStrings = false,
          ErrorMessage = "Age is required field")]
        public int Age { get; set; }
    }
}
