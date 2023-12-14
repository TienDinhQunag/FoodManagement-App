using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class BUSMonan
    {
        private DALMonAn dalMonan;

        public BUSMonan()
        {
            dalMonan = new DALMonAn();
        }
        
        public List<MonAnDTO> GetFoodData()
        {
            return dalMonan.GetFoodData();
        }
        public MonAnDTO GetFoodDetails(int foodId)
        {
            return dalMonan.GetFoodDetails(foodId);
        }
        public void UpdateFoodDetails(MonAnDTO updatedFood)
        {
            dalMonan.UpdateFoodDetails(updatedFood);
        }
        public List<MonAnDTO> SearchFoodData(string keyword)
        {
            return dalMonan.SearchFoodData(keyword);
        }
        public void AddNewFood(MonAnDTO newFood)
        {
            dalMonan.InsertNewFood(newFood);
        }
        public List<string> GetLoaimonanOptions()
        {
            // Call the corresponding method in DALMonan to get Loaimonan options
            return dalMonan.GetLoaimonanOptions();
        }

        private DALMonAn dalMenu = new DALMonAn();

        public List<MenuDTO> GetMenuData()
        {
            // Trả về toàn bộ danh sách menu từ DAL
            return dalMenu.GetMenuData();
        }
        public void AddFoodToMenu(int menuId, int foodId, DateTime ngayCapNhat)
        {
            // Gọi phương thức AddFoodToMenu từ DALMonAn để thêm món ăn vào menu
            dalMonan.AddFoodToMenu(menuId, foodId);
        }
        public List<Tuple<int, int, string>> GetMenuItems(int menuId)
        {
            return dalMonan.GetMenuItems(menuId);
        }
        public void DeleteFoodFromMenu(int menuId, int foodId)
        {
            dalMonan.DeleteFoodFromMenu(menuId, foodId);
        }
        public List<MonAnDTO> GetAvailableFoodData()
        {
            return dalMonan.GetAvailableFoodData();
        }









    }
}
