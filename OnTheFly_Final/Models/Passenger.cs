using System;
using System.ComponentModel.DataAnnotations;

namespace OnTheFly_Final.Models
{
    public class Passenger
    {
        [Required,StringLength(14, ErrorMessage = "CPF inválido!")]
        public String Cpf { get; set; }
        [Required(ErrorMessage = "O campo Nome é obrigatório!"), StringLength(30,ErrorMessage = "Nome inválido!")]
        public String Name { get; set; }
        [Required(ErrorMessage = "O campo Gênero é obrigatório!")]
        public char Gender { get; set; }
        [Required,StringLength(14, ErrorMessage = "Telefone inválido!")]
        public String Phone { get; set; }
        [Required(ErrorMessage = "O campo Data de Nascimento é obrigatório!")]
        public DateTime DtBirth { get; set; }
        public DateTime DtRegister { get; set; }
        public bool Status { get; set; }
    }
}
