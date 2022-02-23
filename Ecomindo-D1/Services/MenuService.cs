using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ecomindo_D1.Model;
using System.Linq;

namespace Ecomindo_D1.bll
{
    public class MenuService
    {
        public MenuService()
        {

        }
        public List<Menu> getAll()
        {
            return null;
        }
        public bool insert(string nama, int harga)
        {
            try
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Menu getOne(int id)
        {
            try
            {
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool deleteOne(int id)
        {
            try
            {
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

}
