using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ecomindo_D1.dal.Model;
using System.Linq;

namespace Ecomindo_D1.bll
{
    public class MenuService
    {
        private List<Menu> listMenu = new List<Menu>
            {
                new Menu
                {
                    idMenu=1,
                    namaMenu="Bakso",
                    hargaMenu=15000
                },
                new Menu
                {
                    idMenu=2,
                    namaMenu = "Rendang",
                    hargaMenu=20000
                }
            };
        public MenuService()
        {

        }
        public List<Menu> getAll()
        {
            var result = this.listMenu;
            return result;
        }
        public bool insert(int id, string nama, int harga)
        {
            try
            {
                this.listMenu.Add(new Menu { idMenu = id, namaMenu = nama, hargaMenu = harga });
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
                var result = this.listMenu.Where(x => x.idMenu == id).FirstOrDefault();
                if (result != default(Menu)) return result;
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
                var data = this.listMenu.Where(x => x.idMenu == id).FirstOrDefault();
                this.listMenu.Remove(data);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

}
