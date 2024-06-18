using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Enums;
using BusinessObjects;
using Microsoft.AspNetCore.Http;

namespace DTOs
{
    public class AccountDTO
    {
        public int AccountId { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime CreatedDate { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }

        public byte[] Avatar { get; set; }

        public AccountRole Role { get; set; }

        public ObjectStatus ObjectStatus { get; set; }
    }

    public class CreateAccountDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [StringLength(10, ErrorMessage = "Phone length must at 10")]
        public string PhoneNumber { get; set; }

        public DateTime CreatedDate { get; set; }

        [StringLength(50, ErrorMessage = "Fullname length must under 50")]
        public string FullName { get; set; }

        [StringLength(50, ErrorMessage = "Username length must under 50")]
        public string UserName { get; set; }

        public AccountRole Role { get; set; }

        public ObjectStatus ObjectStatus = ObjectStatus.ACTIVE;
    }

    public class UpdateAccountDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email is invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [StringLength(10, ErrorMessage = "Phone length must at 10")]
        public string PhoneNumber { get; set; }

        public DateTime CreatedDate { get; set; }

        [StringLength(50, ErrorMessage = "Fullname length must under 50")]
        public string FullName { get; set; }

        [StringLength(50, ErrorMessage = "Username length must under 50")]
        public string UserName { get; set; }

        public AccountRole Role { get; set; }

        public ObjectStatus ObjectStatus { get; set; }
    }
}
