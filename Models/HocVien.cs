using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_1.Models
{
    public class HocVien
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int _id { get; set; }

        [Required]
        [StringLength(100)]
        public string _ten { get; set; }

        [DataType(DataType.Date)]
        public DateTime? _ngaySinh { get; set; }

        [Required]
        [StringLength(50)]
        public string _email { get; set; }

        [Required]
        [StringLength(15)]
        public string _sdt { get; set; }

        [Required]
        public Account _account { get; set; }

        private ICollection<KhoaHoc> _khoaHocs = new List<KhoaHoc>();

        public ICollection<KhoaHoc> KhoaHocs
        {
            get
            {
                
                return _khoaHocs ?? new List<KhoaHoc>();
            }
            set
            {
                // ví dụ: tránh gán null
                _khoaHocs = value ?? new List<KhoaHoc>();
            }
        }

        public HocVien()
        {
        }

        public HocVien(string ten, string email, string sdt, Account account)
        {
            _ten = ten;
            _email = email;
            _sdt = sdt;
            _account = account;
        }
    }
}