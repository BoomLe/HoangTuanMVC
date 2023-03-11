using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models
{
    public class ContactModel
    {
        [Key]
        public int Id{set;get;}

        [Required]
        [Column(TypeName = "nvarchar")]
        [StringLength(255)]
        [DisplayName("Họ và Tên")]
        public string? FullName{set;get;}

        [StringLength(100)]
        [Required]
        [DisplayName("Địa chỉ Email")]
        [EmailAddress(ErrorMessage ="{0} không hợp lệ !")]
        public string? Email {set;get;}

        [DisplayName("Ngày Tháng Năm")]
        public DateTime DateSend{set;get;}
        [DisplayName("Nội dung")]
        public string? message {set;get;}
        [StringLength(50)]
        [DisplayName("Số điện thoại")]
        [Phone(ErrorMessage ="{0} không đúng !")]
        public string? Phone{set;get;}
    }
}