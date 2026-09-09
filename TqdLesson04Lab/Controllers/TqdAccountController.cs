using Microsoft.AspNetCore.Mvc;
using TqdLesson04Lab.Models;

namespace TqdLesson04Lab.Controllers
{
    [Route("/TqdAccount", Name = "account")]
    public class TqdAccountController : Controller
    {
        private readonly List<TqdAccount> tqdAccounts = new()
        {
            new TqdAccount
            {
                Id = 1,
                Name = "Nguyễn Văn An",
                Email = "an.nguyen@example.com",
                Phone = "0901234567",
                Avatar = "/images/3.png",
                Address = "123 Đường Lê Lợi, Quận 1, TP. Hồ Chí Minh",
                Bio = "Kỹ sư phần mềm đam mê công nghệ và du lịch.",
                Gender = 1,
                Birthday = new DateTime(1995, 5, 15)
            },
            new TqdAccount
            {
                Id = 2,
                Name = "Trần Thị Bích",
                Email = "bich.tran@example.com",
                Phone = "0912345678",
                Avatar = "/images/4.png",
                Address = "45 Đường Cầu Giấy, Quận Cầu Giấy, Hà Nội",
                Bio = "Chuyên viên Marketing sáng tạo và yêu thích nghệ thuật.",
                Gender = 0,
                Birthday = new DateTime(1998, 8, 22)
            },
            new TqdAccount
            {
                Id = 3,
                Name = "Lê Hoàng Nam",
                Email = "nam.le@example.com",
                Phone = "0923456789",
                Avatar = "/images/1.jfif",
                Address = "78 Đường Hải Phòng, Quận Hải Châu, Đà Nẵng",
                Bio = "UI/UX Designer tự do, thích nhiếp ảnh và cà phê.",
                Gender = 1,
                Birthday = new DateTime(1992, 11, 30)
            },
            new TqdAccount
            {
                Id = 4,
                Name = "Phạm Minh Châu",
                Email = "chau.pham@example.com",
                Phone = "0934567890",
                Avatar = "/images/2.jfif",
                Address = "12 Đường Nguyễn Văn Linh, Quận Ninh Kiều, Cần Thơ",
                Bio = "Quản lý dự án, quan tâm đến khởi nghiệp và công nghệ.",
                Gender = 0,
                Birthday = new DateTime(1990, 3, 10)
            },
            new TqdAccount
            {
                Id = 5,
                Name = "Hoàng Quốc Dũng",
                Email = "dung.hoang@example.com",
                Phone = "0945678901",
                Avatar = "/images/3.png",
                Address = "56 Đường Quang Trung, TP. Nha Trang, Khánh Hòa",
                Bio = "Chuyên viên phân tích dữ liệu, thích chơi bóng rổ.",
                Gender = 1,
                Birthday = new DateTime(1997, 12, 5)
            }
        };
      
        public IActionResult TqdIndex()
        {
            ViewBag.TqdAccounts = tqdAccounts;
            return View();
        }

        [Route("ho-so-cua-toi",Name ="tqdprofile")]
        public IActionResult TqdProfile(int? id)
        {
            TqdAccount tqdAccount = new TqdAccount
            {
                Id = 5,
                Name = "Hoàng Quốc Dũng",
                Email = "dung.hoang@example.com",
                Phone = "0945678901",
                Avatar = "/images/3.png",
                Address = "56 Đường Quang Trung, TP. Nha Trang, Khánh Hòa",
                Bio = "Chuyên viên phân tích dữ liệu, thích chơi bóng rổ.",
                Gender = 1,
                Birthday = new DateTime(1997, 12, 5)
            };
            if (id != null) 
                tqdAccount = tqdAccounts.FirstOrDefault(x => x.Id == id);

            ViewBag.TqdAccount = tqdAccount;
            return View();
        }
    }
}
