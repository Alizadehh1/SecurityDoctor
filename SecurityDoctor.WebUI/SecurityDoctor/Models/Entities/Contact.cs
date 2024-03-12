using SecurityDoctor.WebUI.AppCode.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace SecurityDoctor.WebUI.Models.Entities
{
    public class Contact : BaseEntity
    {
        [Required(ErrorMessage = "Comment boş ola bilməz")]
        public string Comment { get; set; }
        [Required(ErrorMessage = "Ad boş ola bilməz")]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage = "Email düzgün formada deyil")]
        [Required(ErrorMessage = "Email boş ola bilməz")]
        public string Email { get; set; }
        public int? AnsweredById { get; set; }
        public DateTime? AnswerDate { get; set; }
        public string AnswerMessage { get; set; }
    }
}
