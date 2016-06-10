using HM.DataModels;
using HM.WebApp.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HM.WebApp
{
    public class AppContext
    {
        public static AppContext Instance = new AppContext();

        public int GetLoggedUserId()
        {
            var id = HttpContext.Current.User.Identity.GetUserId();
            return int.Parse(id ?? "-1");
        }

        public User GetLoggedUser()
        {
            return DataContext.Instance.GetUser(this.GetLoggedUserId());
        }

        public int GetLoggedHotelId()
        {
            var user = DataContext.Instance.GetUser(this.GetLoggedUserId());
            return user != null ? user.HotelId : -1;
        }

        public Hotel GetLoggedHotel()
        {
            return DataContext.Instance.GetHotel(this.GetLoggedHotelId());
        }

        public bool IsAdministrator()
        {
            var isAdmin = this.GetLoggedUser()?.RoleId == 1;
            return isAdmin;
        }

        public double GetDayRemains()
        {
            var createdDate = this.GetLoggedHotel()?.CreatedOn ?? DateTime.Today;

            var days = Math.Round(90 - (DateTime.Today - createdDate).TotalDays);
            return days;
        }


        #region Reporting - Charting

        /// <summary>
        /// Hàm lấy 5 khách hàng mới nhất của một khách sạn
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> Get5NewestCustomers()
        {
            var customers = DataContext.Instance.GetCustomers()?
                            .OrderByDescending(c => c.CreatedOn)?
                            .Take(5);
            return customers;
        }

        /// <summary>
        /// Hàm thống kê 5 khách hàng vừa trả phòng gần nhất
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> Get5CusomerCheckedOutRecent()
        {
            var payments = DataContext.Instance.GetPayments()?
                            .OrderByDescending(p => p.CheckOutDate)?
                            .Select(p => p.CustomerId)?
                            .Take(5);

            var customers = new List<Customer>();
            payments?.ToList()?.ForEach(customerId =>
                customers.Add(DataContext.Instance.GetCustomer(customerId))
            );

            return customers;
        }

        /// <summary>
        /// Hàm thống kê mật độ sử dụng phòng (phòng nào được sử dụng nhiều nhất)
        /// </summary>
        /// <param name="fromDate">từ ngày</param>
        /// <param name="toDate">đến ngày</param>
        /// <returns></returns>
        public Dictionary<Room, double> GetRoomUseDensity(DateTime fromDate, DateTime toDate)
        {
            //Double: tỷ lệ sử dụng phòng (tỷ lệ %)
            var orders = DataContext.Instance.GetOrders()?
                            .Where(a => a.CheckInDate >= fromDate && a.CheckInDate <= toDate)?
                            .GroupBy(a => a.RoomId)?
                            .Select(a => a);

            var ordCount = orders?.Count() ?? 0.0;
            var rooms = new Dictionary<Room, double>();
            foreach (var o in orders)
            {
                var ordByRoomCount = 0.0;
                foreach (var item in o)
                {
                    ordByRoomCount++;
                }

                rooms.Add(DataContext.Instance.GetRoom(o.Key), ordByRoomCount / ordCount);
            }

            return rooms;
        }

        /// <summary>
        /// Hàm thống kê mật độ sử dụng phòng trong ngày
        /// </summary>
        /// <returns></returns>
        public Dictionary<Room, double> GetRoomUseDensityByDay()
        {
            return this.GetRoomUseDensity(DateTime.Today, DateTime.Today.AddDays(1));
        }

        /// <summary>
        /// Hàm thống kê mật độ sử dụng phòng trong 7 ngày gần nhất
        /// </summary>
        /// <returns></returns>
        public Dictionary<Room, double> GetRoomUseDensityByWeek()
        {
            return this.GetRoomUseDensity(DateTime.Today.AddDays(-7), DateTime.Today.AddDays(1));
        }

        /// <summary>
        /// Hàm thống kê mật độ sử dụng phòng trong 1 tháng gần nhất
        /// </summary>
        /// <returns></returns>
        public Dictionary<Room, double> GetRoomUseDensityByMonth()
        {
            return this.GetRoomUseDensity(DateTime.Today.AddMonths(-1), DateTime.Today.AddDays(1));
        }

        /// <summary>
        /// Hàm thống kê doanh thu theo loại phòng
        /// </summary>
        /// <param name="fromDate">từ ngày</param>
        /// <param name="toDate">đến ngày</param>
        /// <returns></returns>
        public Dictionary<RoomType, double> GetRevenueByRoomType(DateTime fromDate, DateTime toDate)
        {
            var group = DataContext.Instance.GetPayments()?
                            .Where(a => a.CheckOutDate >= fromDate && a.CheckOutDate <= toDate)?
                            .Select(a => new PaymentModel(a))?
                            .GroupBy(a => a.RoomType)?
                            .Select(a => a);

            var rev = new Dictionary<RoomType, double>();
            foreach (var g in group)
            {
                var total = 0.0;
                foreach (var item in g)
                    total += item.Total;
                rev.Add(g.Key, total);
            }

            return rev;
        }

        /// <summary>
        /// Hàm lấy loại phòng theo thanh toán
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        public RoomType GetRoomTypeFromPayment(int paymentId)
        {
            var payment = DataContext.Instance.GetPayment(paymentId);

            var orderId = payment != null ? payment.OrderId : 0;
            var order = DataContext.Instance.GetOrder(orderId);

            var roomId = order != null ? order.RoomId : 0;
            var room = DataContext.Instance.GetRoom(roomId);

            var roomtypeId = room != null ? room.RoomTypeId : 0;
            var roomtype = DataContext.Instance.GetRoomType(roomtypeId);

            return roomtype;
        }

        /// <summary>
        /// Hàm thống kê doanh thu trong ngày
        /// </summary>
        /// <returns></returns>
        public double GetRevenueByDay()
        {
            var payments = DataContext.Instance.GetPayments()?
                            .Where(a => a.CheckOutDate >= DateTime.Today && a.CheckOutDate <= DateTime.Today.AddDays(1))?
                            .Select(a => a);

            var rev = 0.0;
            if (payments != null)
            {
                foreach (var item in payments)
                    rev += item.Total;
            }

            return rev;
        }

        /// <summary>
        /// Hàm tính doanh thu 7 ngày gần nhất
        /// </summary>
        /// <returns></returns>
        public double GetRevenueByWeek()
        {
            var payments = DataContext.Instance.GetPayments()?
                            .Where(a => a.CheckOutDate >= DateTime.Today.AddDays(-7) && a.CheckOutDate <= DateTime.Today.AddDays(1))?
                            .Select(a => a);

            var rev = 0.0;
            if (payments != null)
            {
                foreach (var item in payments)
                    rev += item.Total;
            }

            return rev;
        }

        /// <summary>
        ///Hàm tính doanh thu theo 1 tháng gần nhất        
        /// </summary>
        /// <returns></returns>
        public double GetRevenueByMonth()
        {
            var payments = DataContext.Instance.GetPayments()?
                            .Where(a => a.CheckOutDate >= DateTime.Today.AddMonths(-1) && a.CheckOutDate <= DateTime.Today.AddDays(1))?
                            .Select(a => a);

            var rev = 0.0;
            if (payments != null)
            {
                foreach (var item in payments)
                    rev += item.Total;
            }

            return rev;
        }

        public double GetRevenueByTime(DateTime from, DateTime to)
        {
            var payments = DataContext.Instance.GetPayments()?
                            .Where(a => a.CheckOutDate >= from && a.CheckOutDate <= to)?
                            .Select(a => a);

            var rev = 0.0;
            if (payments != null)
            {
                foreach (var item in payments)
                    rev += item.Total;
            }

            return rev;
        }

        public Dictionary<int, double> GetMonthlyRevenue(int year)
        {
            var result = new Dictionary<int, double>();
            try
            {
                for (int i = 1; i <= 12; i++)
                {
                    var rev = GetRevenueByTime(new DateTime(year, i, 1), new DateTime(year, i, DateTime.DaysInMonth(year, i)));
                    result.Add(i, rev);
                }

                //var month1 = GetRevenueByTime(new DateTime(year, 1, 1), new DateTime(year, 1, 31));
                //result.Add(1, month1);

                //var month2 = GetRevenueByTime(new DateTime(year, 2, 1), new DateTime(year, 2, 28));
                //result.Add(2, month2);

                //var month3 = GetRevenueByTime(new DateTime(year, 3, 1), new DateTime(year, 3, 31));
                //result.Add(3, month3);

                //var month4 = GetRevenueByTime(new DateTime(year, 4, 1), new DateTime(year, 4, 30));
                //result.Add(4, month4);

                //var month5 = GetRevenueByTime(new DateTime(year, 5, 1), new DateTime(year, 5, 31));
                //result.Add(5, month1);

                //var month6 = GetRevenueByTime(new DateTime(year, 6, 1), new DateTime(year, 6, 30));
                //result.Add(6, month1);

                //var month7 = GetRevenueByTime(new DateTime(year, 7, 1), new DateTime(year, 7, 31));
                //result.Add(7, month1);

                //var month8 = GetRevenueByTime(new DateTime(year, 8, 1), new DateTime(year, 8, 31));
                //result.Add(8, month1);

                //var month9 = GetRevenueByTime(new DateTime(year, 9, 1), new DateTime(year, 9, 30));
                //result.Add(9, month1);

                //var month10 = GetRevenueByTime(new DateTime(year, 10, 1), new DateTime(year, 10, 31));
                //result.Add(10, month1);

                //var month11 = GetRevenueByTime(new DateTime(year, 11, 1), new DateTime(year, 11, 30));
                //result.Add(11, month1);

                //var month12 = GetRevenueByTime(new DateTime(year, 12, 1), new DateTime(year, 12, 31));
                //result.Add(12, month1);

            }
            catch (Exception)
            {

            }

            return result;
        }

        public double GetRevenueByYear()
        {
            var payments = DataContext.Instance.GetPayments()?
                            .Where(a => a.CheckOutDate >= DateTime.Today.AddYears(-1) && a.CheckOutDate <= DateTime.Today.AddDays(1))?
                            .Select(a => a);

            var rev = 0.0;
            if (payments != null)
            {
                foreach (var item in payments)
                    rev += item.Total;
            }

            return rev;
        }

        //public Dictionary<ExtraService, double> ExtraServiceDensity(DateTime fromDate, DateTime toDate)
        //{
        //    var 
        //    return null;
        //}

        //public Dictionary<ExtraService, double> ExtraServiceDensityByDay()
        //{
        //    return null;
        //}

        //public Dictionary<ExtraService, double> ExtraServiceDensityByWeek()
        //{
        //    return null;
        //}

        //public Dictionary<ExtraService, double> ExtraServiceDensityByMonth()
        //{
        //    return null;
        //}

        #endregion

        #region Role

        //public enum Role
        //{
        //    Administrator = 0,  //Admin của ứng dụng
        //    HotelManager = 1,   //Quản lý khách sạn
        //    Cashier = 2,        //Thu ngân
        //    Staff = 3           //Nhân viên
        //}

        ////TODO: định nghĩa hàm kiểm tra quyền, nếu quyền là administrator

        #endregion

        public PayModel Calculate(int roomId, int customerId, int ordId)
        {
            var topay = new PayModel(roomId, customerId, ordId);
            return topay;
        }

    }
}