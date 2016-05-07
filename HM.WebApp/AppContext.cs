using HM.DataModels;
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

        #region User


        public int GetLoginUserId()
        {

            return 0;
        }

        public int GetHotelId(int userId)
        {
            var hotelId = DataService.Instance.GetObject<User>(ApiUtils.USER, ApiUtils.GETBYID, userId).HotelId;
            return hotelId;
        }

        public User GetLoginUser()
        {
            return this.GetUserById(this.GetLoginUserId());
        }

        public User GetUserById(int id)
        {
            return DataService.Instance.GetObject<User>(ApiUtils.USER, ApiUtils.GETBYID, id);
        }

        #endregion

        #region Reporting - Charting

        /// <summary>
        /// Hàm lấy 5 khách hàng mới nhất của một khách sạn
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        public IEnumerable<Customer> Top5NewestCustomers(int hotelId)
        {
            var customers = DataService.Instance
                            .GetObjects<IEnumerable<Customer>>(ApiUtils.CUSTOMER, ApiUtils.GETALL)
                            .Where(a => a.HotelId == hotelId)
                            .OrderByDescending(a => a.CreatedOn)
                            .Take(5);
            return customers;
        }

        /// <summary>
        /// Hàm thống kê 5 khách hàng vừa trả phòng gần nhất
        /// </summary>
        /// <param name="hotelId"></param>
        /// <returns></returns>
        public IEnumerable<Customer> Top5CusomerCheckedOut(int hotelId)
        {
            var payments = DataService.Instance
                            .GetObjects<IEnumerable<Payment>>(ApiUtils.PAYMENT, ApiUtils.GETALL)
                            .Where(a => a.HotelId == hotelId)
                            .OrderByDescending(a => a.CheckOutDate)
                            .Select(a => a.CustomerId)
                            .Take(5);

            var customers = new List<Customer>();

            payments.ToList().ForEach(a =>
                customers.Add(DataService.Instance.GetObject<Customer>(ApiUtils.CUSTOMER, ApiUtils.GETBYID, a))
            );

            return customers;
        }

        //TODO: Định nghĩa hàm thống kê số phòng có mật độ sử dụng cao nhất, bao gồm tên phòng, số lần được sử dụng trong ngày, trong tuần, trong tháng
        public Dictionary<Room, double> Top5MostUseRoom(int hotelId)
        {
            //Double: tỷ lệ sử dụng phòng (tỷ lệ %)
            return null;
        }

        //TODO: Định nghĩa hàm thống kê doanh thu theo loại phòng
        public Dictionary<RoomType, double> RevenueByRoomType(int hotelId, DateTime fromDate, DateTime toDate)
        {
            var payments = DataService.Instance
                            .GetObjects<IEnumerable<Payment>>(ApiUtils.PAYMENT, ApiUtils.GETALL)
                            .Where(a => a.HotelId == hotelId && a.CheckOutDate >= fromDate && a.CheckOutDate <= toDate);

            return null;
        }
        
        public RoomType GetRoomTypeFromPayment(int paymentId)
        {
            var payment = DataService.Instance.GetObject<Payment>(ApiUtils.PAYMENT, ApiUtils.GETBYID, paymentId);
            var order = DataService.Instance.GetObject<Order>(ApiUtils.ORDER, ApiUtils.GETBYID, payment?.OrderId ?? 0);
            var room = DataService.Instance.GetObject<Room>(ApiUtils.ROOM, ApiUtils.GETBYID, order?.RoomId ?? 0);
            var roomtype = DataService.Instance.GetObject<RoomType>(ApiUtils.ROOMTYPE, ApiUtils.GETBYID, room?.RoomTypeId ?? 0);

            return roomtype;
        }

        //TODO: định nghĩa hàm thống kê doanh thu theo ngày
        public Dictionary<DateTime, double> RevenueByDay(int hotelId)
        {
            //double: doanh thu
            return null;
        }

        //TODO: định nghĩa hàm tính doanh thu theo tuần
        public Dictionary<int, double> RevenueByWeek(int hotelId)
        {
            //double: doanh thu
            return null;
        }

        //TODO: định nghĩa hàm tính doanh thu theo tháng
        public Dictionary<int, double> RevenueByMonth(int hotelId)
        {
            //double: doanh thu
            return null;
        }

        //TODO: định nghĩa hàm thống kê các dịch vụ được khách sử dụng nhiều nhất trong ngày, tuần tháng
        public Dictionary<ExtraService, double> ExtraServiceDensityByDay(int hotelId)
        {
            return null;
        }

        //TODO: định nghĩa hàm thống kê các dịch vụ được khách sử dụng nhiều nhất trong ngày, tuần tháng
        public Dictionary<ExtraService, double> ExtraServiceDensityByWeek(int hotelId)
        {
            return null;
        }

        //TODO: định nghĩa hàm thống kê các dịch vụ được khách sử dụng nhiều nhất trong ngày, tuần tháng
        public Dictionary<ExtraService, double> ExtraServiceDensityByMonth(int hotelId)
        {
            return null;
        }

        //TODO: định nghĩa hàm thống kê mật độ sử dụng phòng theo ngày
        public Dictionary<Room, double> RoomUseDensityByDay(int hotelId)
        {
            return null;
        }

        //TODO: định nghĩa hàm thống kê mật độ sử dụng phòng theo tuần
        public Dictionary<Room, double> RoomUseDensityByWeek(int hotelId)
        {
            return null;
        }

        //TODO: định nghĩa hàm thống kê mật độ sử dụng phòng theo tháng
        public Dictionary<Room, double> RoomUseDensityByMonth(int hotelId)
        {
            return null;
        }

        #endregion

        #region Role

        public enum Role
        {
            Administrator = 0,  //Admin của ứng dụng
            HotelManager = 1,   //Quản lý khách sạn
            Cashier = 2,        //Thu ngân
            Staff = 3           //Nhân viên
        }

        //TODO: định nghĩa hàm kiểm tra quyền, nếu quyền là administrator

        #endregion
    }
}