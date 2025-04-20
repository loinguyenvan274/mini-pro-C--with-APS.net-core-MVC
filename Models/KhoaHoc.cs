
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web_1.Models
{
    public class KhoaHoc
    {
        [Key]
        public int MaKhoaHoc { get; set; }

        [Required(ErrorMessage = "Tên khóa học là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên khóa học không được vượt quá 100 ký tự.")]
        public string _tenKhoaHoc { get; set; }

        [Required(ErrorMessage = "Tên giảng viên là bắt buộc.")]
        [StringLength(100, ErrorMessage = "Tên giảng viên không được vượt quá 100 ký tự.")]
        public string _tenGiangVien { get; set; }

        [Required(ErrorMessage = "Thời gian khai giảng là bắt buộc.")]
        public DateTime _thoiGianKhaiGiang { get; set; }

        [Required(ErrorMessage = "Học phí là bắt buộc.")]
        [Range(0, double.MaxValue, ErrorMessage = "Học phí phải là số dương.")]
        public decimal _hocPhi { get; set; }

        [Required(ErrorMessage = "Số lượng học viên tối đa là bắt buộc.")]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng học viên tối đa phải lớn hơn hoặc bằng 1.")]
        public int _soLuongHocVienToiDa { get; set; }

        [NotMapped]
        public int _soHocVienHienTai
        {
            get
            {
                return _hocViens?.Count ?? 0;
            }
        }


        public ICollection<HocVien> _hocViens { get; set; }

        public override bool Equals(object? obj)
        {
            if (obj is KhoaHoc otherKhoaHoc)
            {
                return MaKhoaHoc == otherKhoaHoc.MaKhoaHoc;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return MaKhoaHoc.GetHashCode();
        }
    }
}