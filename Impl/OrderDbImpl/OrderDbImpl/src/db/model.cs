using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMovie.Db.Order.Model
{
    public class OrderModel : IOrderModel
    {
        public OrderModel(long id, long userId, double payAmount, DateTime time,
            long filmId, long cinemaId, long scheduleId, string cinemaSeat, string status)
        {
            Id = id;
            UserId = userId;
            PayAmount = payAmount;
            Time = time;
            FilmId = filmId;
            CinemaId = cinemaId;
            ScheduleId = scheduleId;
            CinemaSeat = cinemaSeat;
            Status = status;

        }
        public long Id { get; set; }
        public long UserId { get; set; }
        public double PayAmount { get; set; }
        public DateTime Time { get; set; }
        public long FilmId { get; set; }
        public long CinemaId { get; set; }
        public long ScheduleId { get; set; }
        public string CinemaSeat { get; set; }
        public string Status { get; set; }
        public long AlipayOrderId { get; set; }
    }
}
