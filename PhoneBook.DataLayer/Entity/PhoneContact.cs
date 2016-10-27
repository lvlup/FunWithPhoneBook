using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.DataLayer.Entity
{
    public class PhoneContact
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("Фамилия")]
        [MaxLength(20)]
        [Required(ErrorMessage = "Введите фамилию")]
        public string Surname { get; set; }

        [DisplayName("Имя")]
        [MaxLength(20)]
        [Required(ErrorMessage = "Введите имя")]
        public string Name { get; set; }


        [DisplayName("Номер телефона")]
        [Required(ErrorMessage = "Введите номер телефона")]
        [Range(0, int.MaxValue, ErrorMessage = "Номер телефона не может быть отрицательным")]
        public int PhoneNumber { get; set; }
    }
}
